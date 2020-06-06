import { Component, Inject } from '@angular/core';
import { Account } from '../Account';
import { DataService } from '../data.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-home-data',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  accounts: Account[];
  searchTerm: string = "";
  filteredAccounts: Account[];
  isSearching: boolean;
  length: number;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private dataService: DataService) {
    //I realize putting this here isn't a best practice.  Had trouble getting the service to resolve the subscribe and decided to move oon because of time.
    http.get<Account[]>(baseUrl + 'account').subscribe(result => {
      this.accounts = result;
      this.filteredAccounts = this.accounts;
    }, error => console.error(error));
  }

  getAccounts() {
    this.http.get<Account[]>(this.baseUrl + 'account').subscribe(result => {
      this.accounts = result;
      this.filteredAccounts = this.accounts;
    }, error => console.error(error));
  }

  filterAccounts(term) {
    this.isSearching = true;
    this.searchTerm = term;
    this.filteredAccounts = this.accounts.filter(e => e.firstName.toLocaleLowerCase().includes(this.searchTerm) || e.lastName.toLocaleLowerCase().includes(this.searchTerm));
    //this is to simulate the slow search as requested in the challenge requirements.  I would not purposely throttle this in the real world
    setTimeout(() => {
      this.isSearching = false;
    }, 2000);
  }
}