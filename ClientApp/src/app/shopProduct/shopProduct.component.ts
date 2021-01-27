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
  Product: IProduct[] = [];
 
  ngOnInit(): void {
    this.data.getProduct().subscribe({
      next: product => this.Product = product,
      error: err => this.errorMessage = err
    });
  }
  AddToProduct(product: IProduct) {
    this.data.AddToOrder(product);
  }

}
