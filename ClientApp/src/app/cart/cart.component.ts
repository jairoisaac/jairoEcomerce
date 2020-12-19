import { Component, OnInit } from '@angular/core';
import { findIndex } from 'rxjs/operators';
import { DataService } from '../shared/data.service';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  constructor(public data: DataService) { }

  ngOnInit() {
  }
  public itemToDecrese(item) {
    if (item.quantity > 1) {
      item.quantity = item.quantity - 1
      //alert("Id " + item.productId + " Quantity " + item.quantity);
    } else if (item.quantity === 1)
    {

      // delete that index
      var myIndex = this.data.order.items.findIndex(i => i.productId == item.productId);
      this.data.order.items.splice(myIndex, 1);
      //alert(" Index " + myIndex);
    }
  }
  public itemToIncrease(item) {
    item.quantity = item.quantity + 1;
  }
  public ItemToDelete(item) {
    var myIndex = this.data.order.items.findIndex(i => i.productId == item.productId);
    this.data.order.items.splice(myIndex, 1);
  }
} 
