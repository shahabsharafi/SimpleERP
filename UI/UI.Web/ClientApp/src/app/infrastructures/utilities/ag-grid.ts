export class AgGridUtility {
  public static getBooleanFilterParams() {
    return {
      filterOptions: [
        {
          displayKey: "none",
          displayName: "-",          
          test: function (filterValue, cellValue) {
            return cellValue;
          },
          hideFilterInput: true
        },
        {
          displayKey: "isTrue",
          displayName: "Yes",
          test: function (filterValue, cellValue) {
            return cellValue != null && cellValue === true;
          },
          hideFilterInput: true
        },
        {
          displayKey: "isFalse",
          displayName: "No",
          test: function (filterValue, cellValue) {
            return cellValue != null && cellValue === false;
          },
          hideFilterInput: true
        }
      ],
      suppressAndOrCondition: true
    }
  };
}
