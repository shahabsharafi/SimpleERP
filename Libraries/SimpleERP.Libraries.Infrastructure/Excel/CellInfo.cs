using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.Excel
{
    /// <summary>
    /// The information of a cell in a sheet.
    /// </summary>
    public class CellInfo
    {
        /// <summary>
        /// Constructor of CellInfo.
        /// </summary>
        /// <param name="name">The name of the cell</param>
        /// <param name="displayName">The display name of the cell.</param>
        /// <param name="value">The value of the cell.</param>
        public CellInfo (string name, string displayName, string value)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Value = value;
        }
        /// <summary>
        /// The name of the cell.
        /// For example, formula uses Name property.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The display name of the cell. Which caption is shown to the user.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The value of the cell.
        /// </summary>
        public string Value { get; set; }
    }
}
