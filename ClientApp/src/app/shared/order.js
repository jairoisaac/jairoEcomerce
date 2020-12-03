"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.OrderItem = exports.Order = void 0;
var _ = require("lodash");
var Order = /** @class */ (function () {
    function Order() {
        this.items = new Array();
    }
    Object.defineProperty(Order.prototype, "subtotal", {
        get: function () {
            return _.sum(_.map(this.items, function (i) { return i.unitPrice * i.quantity; }));
        },
        enumerable: false,
        configurable: true
    });
    return Order;
}());
exports.Order = Order;
var OrderItem = /** @class */ (function () {
    function OrderItem() {
    }
    return OrderItem;
}());
exports.OrderItem = OrderItem;
//# sourceMappingURL=order.js.map