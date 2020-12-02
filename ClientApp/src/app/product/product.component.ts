import { Component, OnInit } from '@angular/core';
import { IProduct } from './product';
import { DataService } from '../shared/data.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ["product.component.css"]
})
export class ProductComponent implements OnInit {
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
