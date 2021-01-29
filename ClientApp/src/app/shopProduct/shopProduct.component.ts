import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/product';
import { DataService } from '../shared/data.service';

@Component({
  selector: 'app-shopProduct',
  templateUrl: './shopProduct.component.html',
  styleUrls: ["shopProduct.component.css"]
})
export class shopProductComponent implements OnInit {
  constructor(private data: DataService) { }
  errorMessage: string;
  product: IProduct[] = [];
 
  ngOnInit(): void {
    this.data.getProducts().subscribe({
      next: product => this.product = product,
      error: err => this.errorMessage = err
    });
  }
  AddToProduct(product: IProduct) {
    this.data.AddToOrder(product);
  }

}
