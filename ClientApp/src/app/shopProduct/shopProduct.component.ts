import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/product';
import { DataService } from '../shared/data.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-shopProduct',
  templateUrl: './shopProduct.component.html',
  styleUrls: ["shopProduct.component.css"]
})
export class shopProductComponent implements OnInit {
  constructor(public data: DataService, private route: ActivatedRoute,
    private router: Router) { }


  errorMessage: string;
  product: IProduct[] = [];
 
  //ngOnInit(): void {
  //  this.data.getProducts().subscribe({
  //    next: product => this.product = product,
  //    error: err => this.errorMessage = err
  //  });
  //}
  ngOnInit(): void {
    //
    this.data.getProducts().subscribe({
      next: product => this.product = product,
      error: err => this.errorMessage = err,
      complete: () => {
        if ((this.data.productId != null) && (this.data.productId == this.data.product.id)) {
          // This is a case of edition or API PUT
          var prodIndx = this.product.findIndex(p => p.id == this.data.product.id);
          this.product[prodIndx] = this.data.product;
          //this.data.product = null;
        } else if (this.data.product != null) {
          //this is the case of an addition
          this.product.push(this.data.product);
        }
      }
    });
    console.log("PRODUCT ID "+this.data.product.id)
  }


  //ngAfterContentChecked(): void {
  //  if (this.data.productId == this.data.product.id)
  //    var prodIndx = this.product.findIndex(p => p.id == this.data.product.id)
  //  this.product[prodIndx] = this.data.product;
  //}

  AddToOrder(product: IProduct) {
    this.data.AddToOrder(product);
  }
  addProduct() {
    this.data.productId = null;
    this.router.navigate(['/products']);
  }
  EditProduct(id) {
    // put id on data service
    this.data.productId = id;
    // enroute it to product.
    this.router.navigate(['/products']);
  }

  //deleteAProduct(id)
  deleteProduct(id) {
    //this.data.product = this.product[this.product.findIndex(p => p.id = id)];
    this.data.deleteProduct(id).subscribe(() => {
      //var prodIndx = this.product.findIndex(p => p.id == this.data.product.id);
      this.product.splice(this.product.findIndex(p => p.id == id), 1);
      //this.product.splice(prodIndx, 1);
    });
  }
}
