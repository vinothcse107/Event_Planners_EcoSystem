import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserModel } from '../shared/Usermodel';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  SignUp!: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.SignUp= new FormGroup({
      // "users": new FormControl(null,Validators.required),
      "name": new FormControl(null,[Validators.required]),
      "username": new FormControl(null,[Validators.required,Validators.pattern("[a-zA-Z0-9_]{8,12}")]),
      "email": new FormControl(null,[Validators.required,Validators.email]),
      "password": new FormControl(null,Validators.required),
      "confirmpass": new FormControl(null,[Validators.minLength(8),Validators.maxLength(16)])
    })
  }
  Signup()
  {
    const body : UserModel = {
      name : this.SignUp.get('name')?.value,
      username : this.SignUp.get('username')?.value,
      email : this.SignUp.get('email')?.value,
      password : this.SignUp.get('mobile')?.value,
      
  }
  }

}
