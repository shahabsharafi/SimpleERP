"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var IMessageModel = /** @class */ (function () {
    function IMessageModel() {
    }
    return IMessageModel;
}());
exports.IMessageModel = IMessageModel;
var MessageModel = /** @class */ (function () {
    function MessageModel(sender, reciver, title, body) {
        this.sender = sender;
        this.reciver = reciver;
        this.title = title;
        this.body = body;
    }
    return MessageModel;
}());
exports.MessageModel = MessageModel;
//# sourceMappingURL=message.model.js.map