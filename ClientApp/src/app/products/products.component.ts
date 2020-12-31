import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  public response: { dbPath: '' };

  constructor() { }

  ngOnInit() {
  }

  public uploadFinished = (event) => {
    this.response = event;
  }

  public createImgPath = () => {
    return 'https://localhost:44367/' + this.response.dbPath;
  }

}
