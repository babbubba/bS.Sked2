"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var BaseService = /** @class */ (function () {
    function BaseService() {
        this.httpOptions = {
            headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
        };
    }
    return BaseService;
}());
exports.BaseService = BaseService;
//# sourceMappingURL=baseService.js.map