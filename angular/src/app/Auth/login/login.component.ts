import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
      selector: 'app-login',
      templateUrl: './login.component.html',
      styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

      LoginF!: FormGroup
      constructor(private _loginService: LoginService, private _route: Router) { }

      ngOnInit(): void {
            this.LoginF = new FormGroup({
                  "username": new FormControl(null, [Validators.required, Validators.email]),
                  "password": new FormControl(null, [Validators.required, Validators.pattern("[a-zA-Z0-9_@#$!?></|+*]{8,12}")])
            })
      }
      tohome() {
            let _username = this.LoginF.get('username')?.value;
            let _password = this.LoginF.get('password')?.value;

            this._loginService.LoginCheck(_username, _password).subscribe({
                  next: (data) => {
                        localStorage.setItem("user", JSON.stringify(data));
                        this._route.navigateByUrl("/home");

                  },
                  error: (data) => {
                        console.log("An Error Occured !!!");
                  }
            })

      }

}
