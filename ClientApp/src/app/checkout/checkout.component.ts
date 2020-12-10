import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from '../shared/data.service';

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class CheckoutComponent {

  constructor(public data: DataService, public router: Router) {

  }
  
  errorMessage: string = "";

  onCheckout() {
    this.data.checkout()
      .subscribe(success => {
        if (success) {
          this.router.navigate([""]);
        }
      }, err => this.errorMessage = "Failed to save order");
  }
}
