import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public response: { dbPath: '' };

  public uploadFinished = (event) => {
    this.response = event;
  }

  public createImgPath = () => {
    return 'https://localhost:44367/'+this.response.dbPath;
  }
}

