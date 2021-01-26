import { isNull } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { IProduct } from '../product/product';
import { DataService } from '../shared/data.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  // To get the path of the picture
  public response: { dbPath: '' };
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

  constructor(private data: DataService) { }

  ngOnInit() {
  }

  public uploadFinished = (event) => {
    //This one is the one who links upload and products
    this.response = event;
    if (this.response.dbPath.length > 0)
      this.noPicture = false;
  }

  // to upload the picture of the product
  public createImgPath = () => {
    let imageString = this.response.dbPath;
    if (imageString.length <= 0)
      this.noPicture = true;
    this.product.imageUrl = imageString.replace("img\\", "");
    console.log("HTML img src=createImgPath: this.response.dbPath " + this.response.dbPath);
    return 'https://localhost:44367/' + this.response.dbPath;
  }

  onSubmit(form: NgForm) {
    if (this.product.imageUrl == null)
      this.noPicture = true;
      
   console.log('in onSubmit: ', form.valid);
   //this.data.postProductsForm(this.product).subscribe(result => console.log('success', result),
   //  error => console.log('error ', error));
   this.data.postProductsForm(this.product).subscribe(result => console.log('success', result),
     error => console.log('error ', error));
 }
  onBlur(field: NgModel) {
    console.log('in onBlur', field.valid)
  }
}
