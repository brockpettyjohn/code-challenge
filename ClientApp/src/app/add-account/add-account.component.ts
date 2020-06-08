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
  imageUrl: string;
  imageFile: string = "./assets/img/stock-avatar.jpg";
  selectedFile: File = null;


  createAccount() {
    this.newAccount = new Account();
    this.newAccount = {
      firstName: this.firstName,
      lastName: this.lastName,
      address: this.address,
      age: this.age,
      interests: this.interests,
      imageUrl: this.imageUrl,
    }
    this.dataService.createAccount(this.newAccount).subscribe(response => {
      this.router.navigate([''])
    }, error => {
      console.error(error)
    });
    this.uploadFile();
    this.newAccount = new Account();
  }

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
    this.imageUrl = this.selectedFile.name;

    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageFile = event.target.result;
    }
    reader.readAsDataURL(this.selectedFile);
  }

  uploadFile() {
    this.dataService.uploadImage(this.selectedFile).subscribe(response => {
      console.log("file uploaded: " + response)
    }, error => {
      console.error(error);
    })
  }
}
