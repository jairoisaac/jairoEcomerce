import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { Observable, of, throwError } from 'rxjs';
import { IProduct } from '../shared/product';
import { catchError, tap } from 'rxjs/operators';
import { Order, OrderItem } from "./order";
import { map } from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  // To edit a product
  public productId = 0;
  public product: IProduct;
  public shop: boolean = false // decide if shoping or changing product data.
  constructor(private http: HttpClient) { }

  public order: Order = new Order();
  

  private productUrl = '';


  getProducts(): Observable<IProduct[]> {
    this.productUrl = '/api/Products';
    return this.http.get<IProduct[]>(this.productUrl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError));
  }


  public AddToOrder(product: IProduct) {

    let item: OrderItem = this.order.items.find(i => i.productId == product.id);

    if (item) {
      item.quantity++;
    } else {
      item = new OrderItem();
      item.productId = product.id;
      item.quantity = 1;
      item.unitPrice = product.price;
      item.productCategory = product.category;
      item.productName = product.name;
      this.order.items.push(item);
    }
  }


  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = 'An error occurred: ${err.error.message}';
    } else {
      errorMessage = 'Server returned code: ${err.status}, error message is: ${err.message}';
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }

  public checkout(): Observable<Order> {
    // Create the product to the order.
    return this.http.post<Order>("/api/orders", this.order, {}).pipe(
      tap(data => console.log('ALL: ' + JSON.stringify(data))),
      catchError(this.handleError)
    );

  }


  // Initial version of the observable
  postProductsForm(product: IProduct): Observable<any> {
    //Saving the product to the datavbase.
    return this.http.post("/api/Products", product, {});
  }


  getProduct(id): Observable<IProduct> {
    this.productUrl = '/api/Products';
    this.productUrl = this.productUrl + "/" + id;
    return this.http.get<IProduct>(this.productUrl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }




  putProduct(product: IProduct): Observable<IProduct> {
    //Saving the product to the datavbase.
    return this.http.put<IProduct>("/api/Products/" + product.id, product);
  }

  deleteProduct(id): Observable<any> {
    //Saving the product to the datavbase.
    return this.http.delete<any>("/api/Products/" + id);
  }

}
