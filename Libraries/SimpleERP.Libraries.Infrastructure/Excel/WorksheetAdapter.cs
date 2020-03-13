using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.Excel
{
    /// <summary>
    /// Represents worksheet interface.
    /// The interface is introduced for creating adapter design pattern for worksheet of any third-party excel packages.
    /// </summary>
    public interface IWorksheetAdaptee
    {          
        /// <summary>
        /// Gets the value of the specified cell.
        /// </summary>
        /// <param name="row">The row number of the worksheet.</param>
        /// <param name="col">The column number of the worksheet.</param>
        /// <returns>Returns the value of the specified row and column from the worksheet.</returns>
        object GetCellValue(int row, int col);
        /// <summary>
        /// Sets the value of the specified cell.
        /// </summary>
        /// <param name="row">The row number of the worksheet.</param>
        /// <param name="col">The column number of the worksheet.</param>
        /// <param name="value">The value to set in the specified row and column.</param>
        void SetCellValue(int row, int col, object value);

        /// <summary>
        /// Checks the worksheet is empty or not.
        /// </summary>
        /// <returns>Returns the status of the worksheet.</returns>
        bool IsEmpty();
    }

    /// <summary>
    /// Represents the worksheet of excel and provides the functionality to access and manipulate it.
    /// Be careful, This class is a adapter class to wrap third-party excel packages.
    /// </summary>
    public class WorksheetAdapter: IWorksheetAdaptee
    {
        ExcelWorksheet _worksheet;
        /// <summary>
        /// Constructs an excel worksheet.
        /// </summary>
        /// <param name="worksheet">The third-party excel worksheet</param>
        public WorksheetAdapter(ExcelWorksheet worksheet)
        {
            this._worksheet = worksheet;
        }

        /// <summary>
        /// Gets the value of the specified cell.
        /// </summary>
        /// <param name="row">The row number of the worksheet.</param>
        /// <param name="col">The column number of the worksheet.</param>
        /// <returns>Returns the value of the specified row and column from the worksheet.</returns>
        public object GetCellValue(int row, int col)
        {
            if (this._worksheet == null)
                throw new Exception("worksheet_not_fond");
            return this._worksheet.Cells[row, col].Value;
        }

        /// <summary>
        /// Sets the value of the specified cell.
        /// </summary>
        /// <param name="row">The row number of the worksheet.</param>
        /// <param name="col">The column number of the worksheet.</param>
        /// <param name="value">The value to set in the specified row and column.</param>
        public void SetCellValue(int row, int col, object value)
        {
            if (this._worksheet == null)
                throw new Exception("worksheet_not_fond");
            this._worksheet.Cells[row, col].Value = value;
        }


        /// <summary>
        /// Checks the worksheet is empty or not.
        /// </summary>
        /// <returns>Returns the status of the worksheet.</returns>
        public bool IsEmpty()
        {
            return this._worksheet == null;
        }
    }
}
