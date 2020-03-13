using SimpleERP.Libraries.Infrastructure.QueryBuilder;
using SimpleERP.Libraries.Infrastructure.QueryBuilder.Extensions;
using SimpleERP.Libraries.Infrastructure.QueryBuilder.Infrastructure.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace SimpleERP.Libraries.Infrastructure.QueryHandler
{
    public class AgGridQueryHandler : IQueryHandler
    {
        public bool HasIds(IQueryCollection args)
        {
            return args.Any(o => o.Key.Contains("ids"));
        }

        public bool HasFilter(IQueryCollection args)
        {
            return args.Any(o => o.Key.Contains("filterModel"));
        }

        public string[] GetIds(IQueryCollection args)
        {
            if (args.Any(o => o.Key.Contains("ids")))
            {
                List<string> idList = new List<string>();
                var ids = args.Where(o => o.Key.Contains("ids"));
                foreach (var id in ids)
                {
                    if (id.Value.Any())
                    {
                        idList.Add(id.Value.ToString());
                    }
                }
                return idList.ToArray();
            }
            return null;
        }

        public IQueryable ApplyQuery(IQueryable query, IQueryCollection args)
        {            
            List<IFilterDescriptor> filterByItems = GetFilterData(args);
            query = query.Where(filterByItems);
            return query;
        }

        public Dictionary<string, object> Execute(IQueryable query, IQueryCollection args)
        {
            query = ApplyQuery(query, args);

            int startRow;
            int endRow;

            int.TryParse(args["startRow"], out startRow);
            int.TryParse(args["endRow"], out endRow);

            List<SortDescriptor> orderByItems = GetSortData(args);

            int total = query.Count();
            query = query.Sort(orderByItems);//note : paging "always" requires sorted data. otherwise you get an exception!
            if (startRow != 0)
                query = query.Skip(startRow);
            if (endRow != 0)
                query = query.Take(endRow - startRow);

            var output = new Dictionary<string, object>();
            output.Add("rows", query);
            output.Add("rowCount", total);

            return output;
        }

        private List<IFilterDescriptor> GetFilterData(IQueryCollection args)
        {
            IEnumerable<IFilterDescriptor> filters = new List<IFilterDescriptor>();
            var filterKeys = args.Where(o => o.Key.Contains("filterModel"))
                .GroupBy(o => o.Key.Substring(o.Key.IndexOf("[") + 1, o.Key.IndexOf("]") - o.Key.IndexOf("[") - 1))
                .Select(o => o.Key).ToArray();
            foreach (var key in filterKeys)
            {
                string filterType = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][filterType]")).Value.ToString();
                string type = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][type]")).Value.ToString();
                var conditions = args.Where(o => o.Key.Contains($"filterModel[{key}][condition")).GroupBy(o => o.Key.Substring(o.Key.IndexOf("[condition"), 12)).ToArray();
                if (conditions.Any())
                {
                    string oprt = args.First(o => o.Key.Contains($"filterModel[{key}][operator]")).Value.ToString();                
                    var compositeFilter = new CompositeFilterDescriptor();
                    IEnumerable<IFilterDescriptor> filterSubItems = new FilterDescriptorCollection();
                    var maxConditionCount = Math.Min(conditions.Count(), 9);
                    for (var i = 0; i < maxConditionCount; i++)
                    {
                        string condetionFilterType = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][condition{i + 1}][filterType]")).Value.ToString();
                        string conditionType = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][condition{i + 1}][type]")).Value.ToString();
                        string conditionFilter = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][condition{i + 1}][filter]")).Value.ToString();
                        string conditionFilterTo = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][condition{i + 1}][filterTo]")).Value.ToString();

                        //toto should be correct
                        filterSubItems = AddFilterDescriptor(filterSubItems, key, condetionFilterType, conditionType, conditionFilter, conditionFilterTo);
                    }
                    var collection = new FilterDescriptorCollection();
                    collection.AddRange(filterSubItems.ToList());
                    compositeFilter.FilterDescriptors = collection;
                    compositeFilter.LogicalOperator = (oprt == "AND" ? FilterCompositionLogicalOperator.And : FilterCompositionLogicalOperator.Or);
                    filters = filters.Concat(new List<IFilterDescriptor>() { compositeFilter });
                }
                else
                {
                    string filter = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][filter]")).Value.ToString();
                    string filterTo = args.FirstOrDefault(o => o.Key.Contains($"filterModel[{key}][filterTo]")).Value.ToString();
                    filters = AddFilterDescriptor(filters, key, filterType, type, filter, filterTo);
                }
            }
            return filters.ToList();
        }

        static IEnumerable<IFilterDescriptor> AddToRangeFilters(IEnumerable<IFilterDescriptor> filters, string key, FilterOperator op1, object val1, FilterOperator op2, object val2)
        {            
            var compositeFilter = new CompositeFilterDescriptor();
            var filterSubItems = new FilterDescriptorCollection();
            filterSubItems.Add(new FilterDescriptor() { Member = key, Operator = op1, Value = val1 });
            filterSubItems.Add(new FilterDescriptor() { Member = key, Operator = op2, Value = val2 });
            compositeFilter.FilterDescriptors = filterSubItems;
            compositeFilter.LogicalOperator = FilterCompositionLogicalOperator.And;
            return filters.Concat(new List<IFilterDescriptor>() { compositeFilter });
        }

        private static IEnumerable<IFilterDescriptor> AddFilterDescriptor(IEnumerable<IFilterDescriptor> filters, string key, string filterType, string type, string filter, string filterTo)
        {
            IEnumerable<IFilterDescriptor> _AddToFilters(FilterOperator op, object val) =>
                filters.Concat(new List<IFilterDescriptor>() { new FilterDescriptor() { Member = key, Operator = op, Value = val } });
            IEnumerable<IFilterDescriptor> _AddToRangeFilters(FilterOperator op1, object val1, FilterOperator op2, object val2) => 
                AddToRangeFilters(filters, key, op1, val1, op2, val2);

            bool ignore = false;
            if (filterType == "text" && type == "none")
            {
                ignore = true;
            }

            if (!ignore)
            {
                if (string.IsNullOrEmpty(type))
                {
                    if (filterType == "string")
                        filters = _AddToFilters(FilterOperator.Contains, filter);
                    else if (filterType == "boolean")
                        filters = _AddToFilters(FilterOperator.IsEqualTo, filter);
                    else if (filterType == "number")
                        filters = _AddToFilters(FilterOperator.IsEqualTo, filter);
                    else
                        filters = _AddToFilters(FilterOperator.IsEqualTo, filter);
                }

                switch (type)
                {
                    case "contains":
                        filters = _AddToFilters(FilterOperator.Contains, filter);
                        break;
                    case "notContains":
                        filters = _AddToFilters(FilterOperator.DoesNotContain, filter);
                        break;
                    case "equals":
                        filters = _AddToFilters(FilterOperator.IsEqualTo, filter);
                        break;
                    case "notEqual":
                        filters = _AddToFilters(FilterOperator.IsNotEqualTo, filter);
                        break;
                    case "startsWith":
                        filters = _AddToFilters(FilterOperator.StartsWith, filter);
                        break;
                    case "endsWith":
                        filters = _AddToFilters(FilterOperator.EndsWith, filter);
                        break;
                    case "isTrue":
                        filters = _AddToFilters(FilterOperator.IsEqualTo, true);
                        break;
                    case "isFalse":
                        filters = _AddToFilters(FilterOperator.IsEqualTo, false);
                        break;
                    case "lessThan":
                        filters = _AddToFilters(FilterOperator.IsLessThan, filter);
                        break;
                    case "lessThanOrEqual":
                        filters = _AddToFilters(FilterOperator.IsLessThanOrEqualTo, filter);
                        break;
                    case "greaterThan":
                        filters = _AddToFilters(FilterOperator.IsGreaterThan, filter);
                        break;
                    case "greaterThanOrEqual":
                        _AddToFilters(FilterOperator.IsGreaterThanOrEqualTo, filter);
                        break;
                    case "inRange":
                        filters = _AddToRangeFilters(FilterOperator.IsGreaterThanOrEqualTo, filter, FilterOperator.IsLessThanOrEqualTo, filterTo);
                        break;
                    default:
                        throw new Exception($"Ag-Grid Operator '{type}' is not implemented yet!");
                }               
            }
            return filters;
        }

        private static List<SortDescriptor> GetSortData(IQueryCollection args)
        {
            List<SortDescriptor> orderByItems = new List<SortDescriptor>();
            var s = args.Where(o => o.Key.Contains("sortModel"));
            int c = s.Count() / 2;
            for (var i = 0; i < c; i++)
            {
                string colId = args.FirstOrDefault(o => o.Key.Contains($"sortModel[{i}][colId]")).Value.ToString();
                string sort = args.First(o => o.Key.Contains($"sortModel[{i}][sort]")).Value.ToString();
                if (!string.IsNullOrEmpty(colId) && !string.IsNullOrEmpty(sort))
                {
                    orderByItems.Add(new SortDescriptor()
                    {
                        Member = string.Concat(colId.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(word => word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower())),
                        SortDirection = sort.ToUpper() == "ASC" ? System.ComponentModel.ListSortDirection.Ascending : System.ComponentModel.ListSortDirection.Descending
                    });
                }
            }
            return orderByItems;
        }
    }
}
