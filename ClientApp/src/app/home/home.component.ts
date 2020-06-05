import { Component, Inject } from '@angular/core';
import { Account } from '../Account';
import { DataService } from '../data.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home-data',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  accounts: Account[];
  searchTerm: string = "";
  filteredAccounts: Account[];
  isSearching: boolean;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private dataService: DataService) {
    http.get<Account[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.accounts = result;
      this.filteredAccounts = this.accounts;
    }, error => console.error(error));
  }

  filterAccounts(term) {
    this.isSearching = true;
    setTimeout(() => {
      this.searchTerm = term;
      this.filteredAccounts = this.accounts.filter(e => e.firstName.includes(this.searchTerm) || e.lastName.includes(this.searchTerm));
      this.isSearching = false;
    }, 3000);
  }
}