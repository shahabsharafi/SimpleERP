using SimpleERP.Libraries.Infrastructure.Commons;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.Excel
{
    /// <summary>
    /// Handles excel operations. This class is a adapter class. It means the class wraps ExcelPackage functionality.
    /// So, the class is testable.
    /// Before creating or loading excel file, call Init method to set the excel file's path.
    /// </summary>
    public class ExcelHelper : IExcelHelper
    {

        // Working with xlsx 2010 and 2007 packages.

        // The adapter class for Excel.
        IExcelAdaptee _excelAdapter;
        // The path of excel file is keep here.
        FileInfo fileInfo;

        IResourceManager _resourceManager;


        /// <summary>
        /// Constructs an excel file based on existing or create new one.
        /// </summary>
        /// <param name="resourceManager">This manager used for translate coplumn headers.</param>
        public ExcelHelper(IResourceManager resourceManager)
        {
            this._resourceManager = resourceManager;
        }



        /// <summary>
        /// Gets the path of excel file.
        /// </summary>
        public string ExcelPath
        {
            get
            {
                if (fileInfo != null)
                    return this.fileInfo.ToString();
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Initializes the path for creating/loading excel file. 
        /// If the path is not specified before calling Save and Load methods, an error exception will be raised.
        /// </summary>
        /// <param name="path"></param>
        public void Init(string path = null)
        {
            if (path == null)
            {
                this._excelAdapter = new ExcelAdapter(new ExcelPackage());
            }
            else
            {
                fileInfo = new FileInfo(path);
                this._excelAdapter = new ExcelAdapter(new ExcelPackage(fileInfo));
            }
        }


        /// <summary>
        /// Create a sheet with a list of values. The list contains a T types(for example int, string fields) to be added to the sheet.
        /// </summary>
        /// <typeparam name="T">A Type which includes fields.</typeparam>
        /// <param name="sheetName">The name of the sheet.</param>
        /// <param name="list">The values in the list are inserted into the given sheet.</param>
        /// <exception cref="System.Exception">Throws when the path string for creating or loading the excel file is not specified in Init Method. To avoid this exception, before using any methods, calls the Init method with the suitable path parameter.</exception>
        public void CreateExcel<T>(string sheetName, IList<T> list)
        {
            if (this._excelAdapter == null)
                throw new Exception("The path is not specified. Please call Init method.");
            Dictionary<string, string> properties = this.GetProperties<T>();
            var propertyList = properties.ToList();
            WorksheetAdapter worksheet = this._excelAdapter.GetWorksheet(sheetName);
            if (worksheet == null)
                worksheet = this._excelAdapter.CreateWorkSheet(sheetName);
            for (int i = 0; i < propertyList.Count; i++)
            {
                worksheet.SetCellValue(1, i + 1, propertyList[i].Key);
            }
            for (int i = 0; i < list.Count; i++)
            {
                T row = list[i];
                for (int j = 0; j < propertyList.Count; j++)
                {
                    string fieldName = propertyList[j].Value;
                    worksheet.SetCellValue(i + 2, j + 1, GetValue<T>(row, fieldName));
                }
            }
        }

        /// <summary>
        /// Save created excel.
        /// </summary>
        public void Save()
        {
            this._excelAdapter.Save();
        }

        /// <summary>
        /// Get byte arrays of the excel file.
        /// </summary>
        public byte[] GetAsByteArray()
        {
            return this._excelAdapter.GetAsByteArray();
        }

        /// <summary>
        /// Loads an excel file with given a sheet name and an action to reckon the end of reading file.
        /// </summary>
        /// <typeparam name="T">A Type which includes fields.</typeparam>
        /// <param name="sheetName">The name of the sheet.</param>
        /// <param name="eof">An action to control the end of reading file.(cell)</param>
        /// <returns>Returns the loaded records in T type.</returns>
        /// <exception cref="System.Exception">Throws when the path string for creating or loading the excel file is not specified in Init Method. To avoid this exception, before using any methods, calls the Init method with the suitable path parameter.</exception>
        public List<T> Load<T>(string sheetName, Func<List<CellInfo>, bool> eof) where T : new()
        {
            if (this._excelAdapter == null)
                throw new Exception("The path is not specified. Please call Init method.");
            Dictionary<string, string> properties = this.GetProperties<T>();
            if (this._excelAdapter.GetWorksheet(sheetName) == null)
                throw new Exception("client_excel_format_is_wrong");
            var worksheet = this._excelAdapter.GetWorksheet(sheetName); 
            List<string> fields = GetField(worksheet);

            // if given Type T is not suitable with sheet's fields, so rise an exception.
            if (properties.Any(p => !fields.Any(f => f == p.Key)))
            {
                throw new Exception("client_excel_format_is_wrong");
            }
            List<T> output = new List<T>();

            //TODO (shahab sharafi) should be compelete
            //ValidationDictionary validationDictionary = new ValidationDictionary();
            // start after field header (start from second row).
            int j = 2;
            
            while (true)
            {
                // start getting cell's information from second row until eof action terminate it.
                // all of cell that is belong to a record gatherd into dic variable.
                List<CellInfo> dic = GetRecord(worksheet, fields, properties, j);
                if (eof(dic))
                    break;
                T obj = new T();
                foreach (var item in dic)
                {
                    //TODO (shahab sharafi) should be compelete
                    //try
                    //{
                        SetValue(obj, item.Name, item.Value);
                    //}
                    //catch
                    //{
                        //  validationDictionary.Add($"row: {j}, field: {item.DisplayName} is not valid");
                    //}
                }
                output.Add(obj);
                j++;                            
            }
            //TODO (shahab sharafi) should be compelete
            //if (!validationDictionary.IsValid)
            //    throw new CustomException(validationDictionary);
            return output;
        }
        /// <summary>
        /// Provides a dictionary that contains field with its display name. Display name is used to row headers.
        /// </summary>
        /// <typeparam name="T">A custom <c>Type</c></typeparam>
        /// <returns>Returns the properties of the T.</returns>
        private Dictionary<string, string> GetProperties<T>()
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string displayName = this.GetDisplayName(propertyInfo);
                if (!string.IsNullOrEmpty(displayName))
                {
                    properties.Add(displayName, propertyInfo.Name);
                }
            }
            return properties;
        }
        /// <summary>
        /// Gets a list of cell's information from the given sheet and specified row.
        /// </summary>
        /// <param name="worksheet">The name of the sheet.</param>
        /// <param name="fields">The list of the fields.</param>
        /// <param name="properties">The list of the properties which keeps a record.</param>
        /// <param name="j">Row</param>
        /// <returns>Returns a list of CellInfo.</returns>
        private static List<CellInfo> GetRecord(IWorksheetAdaptee worksheet, List<string> fields, Dictionary<string, string> properties, int j)
        {
            List<CellInfo> rec = new List<CellInfo>();
            for (int i = 0; i < fields.Count; i++)
            {
                string field = fields[i];
                string value = worksheet.GetCellValue(j, i + 1)?.ToString();

                string fieldName;
                if (properties.TryGetValue(field, out fieldName))
                {
                    //SetValue(obj, fieldName, value);
                    rec.Add(new CellInfo(name:fieldName, field, value));
                }
            }
            return rec;
        }
        /// <summary>
        /// Provides the display name of an excel property. The output value is gathered from the attribute
        /// in which assigned to the property before. 
        /// Reads the display name attribute of the property. if it doesn't have the ExcelPropertyAttribute the null value is returned.
        /// </summary>
        /// <param name="prop">The PropertyInfo that inherited from ExcelPropertyAttribute attribute.</param>
        /// <returns>Returns the display name gained from the attribute. If the property doesn't have the ExcelPropertyAttribute the null value is returned.</returns>
        /// <exception cref="System.NullReferenceException">Thrown when the property is not as ExcelPropertyAttribute. Provides the property as ExcelPropertyAttribute.</exception>
        private string GetDisplayName(PropertyInfo prop)
        {
            string displayName = null;
            // get all custom attribute, here ExcelPropertyAttribute is a custom attribute.
            object[] attrs = prop.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                ExcelPropertyAttribute excelPropertyAttribute = attr as ExcelPropertyAttribute;
                if (excelPropertyAttribute != null)
                {
                    displayName = this._resourceManager.GetValue(excelPropertyAttribute.MethodeName);
                }
                else
                    throw new NullReferenceException();
            }

            return displayName;
        }
        /// <summary>
        /// Sets a value to the specified field(a property of the T type).
        /// </summary>
        /// <typeparam name="T">The type of T.</typeparam>
        /// <param name="obj">The object that its field get the new value.</param>
        /// <param name="field">The name of T field.</param>
        /// <param name="value">The value of T field.</param>
        private static void SetValue<T>(T obj, string field, string value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(field, BindingFlags.Public | BindingFlags.Instance);
            if (null != propertyInfo && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
            }
        }
        /// <summary>
        /// Get a value from the specified field(a property of the T type).
        /// </summary>
        /// <typeparam name="T">T type of T</typeparam>
        /// <param name="obj">The object that value of its files is requested.</param>
        /// <param name="field">The name of T field.</param>
        /// <returns>Returns the value of the T field.</returns>
        private static string GetValue<T>(T obj, string field)
        {
            string output = null;
            PropertyInfo propertyInfo = obj.GetType().GetProperty(field, BindingFlags.Public | BindingFlags.Instance);
            if (null != propertyInfo && propertyInfo.CanRead)
            {
                output = propertyInfo.GetValue(obj)?.ToString();
            }
            return output;
        }

        /// <summary>
        /// Get the fields of the given sheet. The first rows of the sheet are fields.
        /// </summary>
        /// <param name="worksheet">A sheet of the excel file.</param>
        /// <returns>A list of the name fields.</returns>
        private static List<string> GetField(IWorksheetAdaptee worksheet)
        {
            List<String> fields = new List<string>();
            int index = 1;
            while (!string.IsNullOrEmpty(worksheet.GetCellValue(1, index)?.ToString()))
            {
                var cell = worksheet.GetCellValue(1, index)?.ToString();
                fields.Add(cell);
                index++;
            }

            return fields;
        }
    }
}
