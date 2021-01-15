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
    this.response = event;
  }

  // to upload the picture of the product
  public createImgPath = () => {
    //console.log(this.response.dbPath);
    return 'https://localhost:44367/' + this.response.dbPath;
  }

 onSubmit(form: NgForm) {
   console.log('in onSubmit: ', form.valid);
   this.data.postProductsForm(this.product).subscribe(result => console.log('success', result),
     error => console.log('error ', error));
 }
  onBlur(field: NgModel) {
    console.log('in onBlur', field.valid)
  }
}
