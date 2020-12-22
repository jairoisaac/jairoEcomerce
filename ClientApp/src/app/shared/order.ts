import * as _ from "lodash";
import { forEach } from "lodash";
//NOTE: Put the count here make a property for this class
export class Order {
  orderId: number;
  orderDate: Date;
  orderNumber: string;
  items: Array<OrderItem> = new Array<OrderItem>();
  get subtotal(): number {
    return _.sum(_.map(this.items, i => i.unitPrice * i.quantity ));
  }
  get totalItems(): number {
    return _.sum(_.map(this.items, i => i.quantity)); 
  }
}
export class OrderItem {
  id: number;
  quantity: number;
  unitPrice: number;
  productId: number;
  productName: string;
  productCategory: string;
}


