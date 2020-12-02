export class Order {
  orderId: number;
  orderDate: Date;
  orderNumber: string;
  items: Array<OrderItem> = new Array<OrderItem>();
}
export class OrderItem {
  id: number;
  quantity: number;
  unitPrice: number;
  productId: number;
  productCategory: string;
  productName: string;
}


