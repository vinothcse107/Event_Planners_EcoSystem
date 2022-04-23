import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  LoginF!: FormGroup
  constructor() { }

  ngOnInit(): void {
    this.LoginF = new FormGroup({
      "email": new FormControl(null, [Validators.required, Validators.email]),
      "password": new FormControl(null, [Validators.required, Validators.pattern("[a-zA-Z0-9_@#$!?></|+*]{8,12}")])
    })
  }
  tohome()
  {
    let _email = this.LoginF.get('email')?.value;
    let _password = this.LoginF.get('password')?.value;
  }

}
