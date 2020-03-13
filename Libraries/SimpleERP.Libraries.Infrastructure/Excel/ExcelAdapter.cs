using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.Excel
{
    /// <summary>
    /// Represents the adapter design pattern for an excel helper class.
    /// Each adapter class should have an Adaptee class to represents its functionality.
    /// These functionality are used when we want to have unit test with mocking.
    /// Mock uses the interface to create a fake object.
    /// </summary>
    public interface IExcelAdaptee
    {
        //void Load(string path);
        void Save();
        byte[] GetAsByteArray();

        WorksheetAdapter GetWorksheet(string worksheetName);
        WorksheetAdapter CreateWorkSheet(string worksheetName);
    }

    /// <summary>
    /// Provides a wrapper class for excel operations. The class provides the primitive the needs of excel functionality such as saving/ loading and etc.
    /// This adapter hides the excel third-party packages and provides the common and utility functionality in simple manner and can be used in creating fake object such as mocking in unit tests.
    /// </summary>
    public class ExcelAdapter : IExcelAdaptee
    {
        ExcelPackage _excelPackage;
        /// <summary>
        /// Creates an Excel file to read/write it.
        /// </summary>
        /// <param name="excelPackage">The third-party excel package is passed as constructor argument.</param>
        public ExcelAdapter(ExcelPackage excelPackage)
        {
            this._excelPackage = excelPackage;   
        }

       /// <summary>
       /// Gets the specified worksheet.
       /// </summary>
       /// <param name="worksheetName">The name of the worksheet.</param>
       /// <returns>Returns the requested worksheet.</returns>
        public WorksheetAdapter GetWorksheet(string worksheetName)
        {
            var excelWorksheet = this._excelPackage.Workbook.Worksheets.FirstOrDefault(o => o.Name == worksheetName);
            if (excelWorksheet == null)
                return null;
            WorksheetAdapter workSheet = new WorksheetAdapter(excelWorksheet);
            return workSheet;
        }
        /// <summary>
        /// Creates a worksheet on excel file. 
        /// </summary>
        /// <param name="worksheetName">The name of the worksheet.</param>
        /// <returns>Returns the created worksheet.</returns>
        public WorksheetAdapter CreateWorkSheet(string worksheetName)
        {
            var excelWorksheet = this._excelPackage.Workbook.Worksheets.Add(worksheetName);
            WorksheetAdapter workSheet = new WorksheetAdapter(excelWorksheet);
            return workSheet;
        }

        /// <summary>
        /// Saves all of changes to the excel file.
        /// </summary>
        public void Save()
        {
            this._excelPackage.Save();
        }

        /// <summary>
        /// Get byte arrays of the excel file.
        /// </summary>
        public byte[] GetAsByteArray()
        {
            return this._excelPackage.GetAsByteArray();
        }
    }
}
