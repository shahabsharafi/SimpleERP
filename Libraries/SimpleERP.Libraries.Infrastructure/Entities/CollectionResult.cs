using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleERP.Libraries.Infrastructure.Entities
{ 
    public interface ICollectionResult<TData>
    {
        IEnumerable<TData> Rows { get; }
        long RowCount { get; }
    }

    public class CollectionResult<TData>: ICollectionResult<TData>
    {
        private IEnumerable<TData> _rows;
        private long _rowCount;

        public CollectionResult(IEnumerable<TData> rows, long rowCount)
        {
            this._rows = rows;
            this._rowCount = rowCount;
        }

        public IEnumerable<TData> Rows => this._rows;

        public long RowCount => this._rowCount;
    }
}
