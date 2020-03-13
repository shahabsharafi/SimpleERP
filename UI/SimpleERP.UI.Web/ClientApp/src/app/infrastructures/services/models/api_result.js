"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var IApiResult = /** @class */ (function () {
    function IApiResult() {
    }
    return IApiResult;
}());
exports.IApiResult = IApiResult;
var ApiResult = /** @class */ (function () {
    function ApiResult(isSuccess, statusCode, message) {
        this.isSuccess = isSuccess;
        this.statusCode = statusCode;
        this.message = message;
    }
    return ApiResult;
}());
exports.ApiResult = ApiResult;
var IApiDataResult = /** @class */ (function () {
    function IApiDataResult() {
    }
    return IApiDataResult;
}());
exports.IApiDataResult = IApiDataResult;
var ApiDataResult = /** @class */ (function () {
    function ApiDataResult(isSuccess, statusCode, message, data) {
        this.isSuccess = isSuccess;
        this.statusCode = statusCode;
        this.message = message;
        this.data = data;
    }
    return ApiDataResult;
}());
exports.ApiDataResult = ApiDataResult;
//# sourceMappingURL=api_result.js.map