import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';
import { UserModel } from '../../shared/Usermodel';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  SignUp!: FormGroup;

  constructor(private _loginService : LoginService) { }

  ngOnInit(): void {
    this.SignUp= new FormGroup({
      "name": new FormControl(null,[Validators.required]),
      "username": new FormControl(null,[Validators.required,]),
      "email": new FormControl(null,[Validators.required,Validators.email]),
      "password": new FormControl(null,Validators.required),

      "phone": new FormControl(null,[Validators.required]),
      "location": new FormControl(null,[Validators.required,]),
      
      "role": new FormControl(null,[Validators.required,]),
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
      phone : this.SignUp.get("phone")?.value,
      location : this.SignUp.get("location")?.value,
      role : "user"
  }
  this._loginService.SignupCheck(body);
  }

}
