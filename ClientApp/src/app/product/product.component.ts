import { Component, OnInit } from '@angular/core';
import { IProduct } from './product';
import { DataService } from '../shared/data.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})
export class ProductComponent implements OnInit {
  constructor(private ProductService: DataService) { }
  errorMessage: string;
  Product: IProduct[] = [];
  ngOnInit(): void {
    this.ProductService.getProduct().subscribe({
      next: product => this.Product = product,
      error: err => this.errorMessage = err
    });
    }
}
