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
  public itemToDelete(event,o) {
  
    alert("This is the number " + o.productId);
  }
}
