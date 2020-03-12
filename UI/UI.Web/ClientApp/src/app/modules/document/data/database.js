"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var dexie_1 = require("dexie");
var ContractManagementDatabase = /** @class */ (function (_super) {
    __extends(ContractManagementDatabase, _super);
    //...other tables goes here...
    function ContractManagementDatabase() {
        var _this = _super.call(this, "ContractManagementDatabase") || this;
        _this.version(1).stores({
            contracts: '++id, first, last',
        });
        // The following line is needed if your typescript
        // is compiled using babel instead of tsc:
        _this.contracts = _this.table("contracts");
        return _this;
    }
    return ContractManagementDatabase;
}(dexie_1.default));
//# sourceMappingURL=database.js.map