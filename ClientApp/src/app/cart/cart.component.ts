import { Component, OnInit } from '@angular/core';
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
  public itemToDelete(item) {
  
    alert("This is the number " + item.productId);
  }
}
