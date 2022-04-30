import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';
import { UserModel } from 'src/app/shared/Usermodel';

@Component({
      selector: 'app-bsignup',
      templateUrl: './bsignup.component.html',
      styleUrls: ['./bsignup.component.css']
})
export class BsignupComponent implements OnInit {

      constructor(private _loginService: LoginService, private _route: Router) { }
      bsignUp!: FormGroup;


      ngOnInit(): void {

            this.bsignUp = new FormGroup({
                  "name": new FormControl(null, [Validators.required]),
                  "username": new FormControl(null, [Validators.required,]),
                  "email": new FormControl(null, [Validators.required, Validators.email]),
                  "role": new FormControl(null, [Validators.required,]),
                  "password": new FormControl(null, Validators.required),
                  "confirmpass": new FormControl(null, [Validators.minLength(8), Validators.maxLength(16)]),
                  "phone": new FormControl(null, [Validators.required]),
                  "location": new FormControl(null, [Validators.required,]),
            })
      }

      BSignup() {
            const body: UserModel = {
                  name: this.bsignUp.get('name')?.value,
                  username: this.bsignUp.get('username')?.value,
                  email: this.bsignUp.get('email')?.value,
                  password: this.bsignUp.get('mobile')?.value,
                  phone: this.bsignUp.get("phone")?.value,
                  location: this.bsignUp.get("location")?.value,
                  role: this.bsignUp.get("role")?.value,
            }
            // console.log(body);
            this._loginService.SignupCheck(body).subscribe(
                  {
                        next: (data) => {
                              localStorage.setItem("user", JSON.stringify(data));
                              this._route.navigateByUrl("/login");
                        },
                        error: (data) => {
                              console.log("An Error Occured ! Please Try Signup Again");
                        }
                  }
            )
      }

}
