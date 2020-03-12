"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var QueryString = /** @class */ (function () {
    function QueryString() {
    }
    QueryString.serialize = function (obj, prefix) {
        if (prefix === void 0) { prefix = null; }
        var str = [], p;
        for (p in obj) {
            if (obj.hasOwnProperty(p)) {
                var k = prefix ? prefix + "[" + p + "]" : p, v = obj[p];
                var r = (v !== null && typeof v === "object")
                    ? QueryString.serialize(v, k)
                    : encodeURIComponent(k) + "=" + encodeURIComponent(v);
                if (r)
                    str.push(r);
            }
        }
        return str.join("&");
    };
    return QueryString;
}());
exports.QueryString = QueryString;
//# sourceMappingURL=utility.js.map