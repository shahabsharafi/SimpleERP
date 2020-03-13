using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.Excel
{
    /// <summary>
    /// Represents the helper interface for excel utilities.
    /// Excel operations should at least these operations and properties.
    /// </summary>
    public interface IExcelHelper
    {

        /// <summary>
        /// Initializes the path for creating/loading excel file. 
        /// If the path is not specified before calling Save and Load methods, an error exception will be raised.
        /// </summary>
        /// <param name="path"></param>
        void Init(string path = null);

        /// <summary>
        /// Loads a sheet of excel file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheetName">The name of the sheet.</param>
        /// <param name="eof">A <c>Func</c> to find end of the file.</param>
        /// <returns></returns>
        List<T> Load<T>(string sheetName, Func<List<CellInfo>, bool> eof) where T : new();

        /// <summary>
        /// Create a sheet with a list of values. The list contains a T types(for example int, string fields) to be added to the sheet.
        /// </summary>
        /// <typeparam name="T">A Type which includes fields.</typeparam>
        /// <param name="sheetName">The name of the sheet.</param>
        /// <param name="list">The values in the list are inserted into the given sheet.</param>
        /// <exception cref="System.Exception">Throws when the path string for creating or loading the excel file is not specified in Init Method. To avoid this exception, before using any methods, calls the Init method with the suitable path parameter.</exception>
        void CreateExcel<T>(string sheetName, IList<T> list);

        /// <summary>
        /// Save created excel.
        /// </summary>
        void Save();

        /// <summary>
        /// Get byte arrays of the excel file.
        /// </summary>
        byte[] GetAsByteArray();
    }
}
