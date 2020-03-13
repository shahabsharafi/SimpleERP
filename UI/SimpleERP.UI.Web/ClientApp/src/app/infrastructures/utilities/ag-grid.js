"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AgGridUtility = /** @class */ (function () {
    function AgGridUtility() {
    }
    AgGridUtility.getBooleanFilterParams = function () {
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
        };
    };
    ;
    return AgGridUtility;
}());
exports.AgGridUtility = AgGridUtility;
//# sourceMappingURL=ag-grid.js.map