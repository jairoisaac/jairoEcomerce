import { isNull } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { IProduct } from '../shared/product';
import { DataService } from '../shared/data.service';
import { ActivatedRoute, Router } from '@angular/router';

//import { error } from 'console';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  // To get the path of the picture
  public response: { dbPath: '' };
  // To handle the validation for having a picture of the product.
  public noPicture = false;
  errorMessage = "No Picture";

  originalproduct: IProduct = {
    category: null,
    name: null,
    price: null,
    imageUrl: null,
    id: null
  }

  product: IProduct = { ...this.originalproduct };
 
  constructor(private data: DataService, private route: ActivatedRoute,
    private router: Router)
  {
  }

  ngOnInit() {
    if (this.data.productId != null) 
      this.getAProduct(this.data.productId);
  }

  public uploadFinished = (event) => {
    //This one is the one who links upload and products
    this.response = event;
    if (this.response.dbPath.length > 0) {
      this.noPicture = false;
      let imageString = this.response.dbPath;
      this.product.imageUrl = imageString.replace("img\\", "");
    }
  }


  onSubmit(form: NgForm) {
    if (this.product.imageUrl == null)
      this.noPicture = true;

    console.log('in onSubmit: ', form.valid);
    //this.data.postProductsForm(this.product).subscribe(result => console.log('success', result),
    //  error => console.log('error ', error));
    if (this.data.productId) 
      this.updateProduct(this.product);
    else {
      this.data.postProductsForm(this.product).subscribe({
        next: result => console.log('success', result),
        error: error => console.log('error ', error)
      });
      this.router.navigate(['/shop']);
      this.data.product = this.product;
    }

  }
  onBlur(field: NgModel) {
    console.log('in onBlur', field.valid)
  }
  //getAProduct(id)
  getAProduct(id) {
    this.data.getProduct(id).subscribe({
      next: product => this.product = product,
      error: err => this.errorMessage = err
    });
  }
  updateProduct(product: IProduct) {
    this.data.product = product;
    this.data.putProduct(product).subscribe();
    this.router.navigate(['/shop']);
  }
}
