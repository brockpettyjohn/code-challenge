import { Component, Inject, OnInit } from '@angular/core';
import { Account } from '../Account';
import { DataService } from '../data.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  accounts: Account[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private dataService: DataService) {
    http.get<Account[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.accounts = result;
    }, error => console.error(error));
  }
}





