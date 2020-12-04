import { Component } from "@angular/core";
import { DataService } from '../shared/data.service';

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class CheckoutComponent {

  constructor(public data: DataService) {
  }
  
  errorMessage: string = "";

  onCheckout() {
    // TODO
    alert("Doing checkout");
  }
}
