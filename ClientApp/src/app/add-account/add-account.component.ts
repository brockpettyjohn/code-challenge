import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { Account } from '../Account';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent {

  constructor(private dataService: DataService, private router: Router) { }

  firstName: string;
  lastName: string;
  address: string;
  age: number;
  interests: string;
  newAccount: Account;


  createAccount() {
    this.newAccount = new Account();
    this.newAccount = {
      firstName: this.firstName,
      lastName: this.lastName,
      address: this.address,
      age: this.age,
      interests: this.interests
    }
    this.dataService.createAccount(this.newAccount).subscribe(response => {
      this.router.navigate([''])
    }, error => {
      console.error(error)
    });
    this.newAccount = new Account();
    
  }

}
