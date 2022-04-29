import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginResponseModel } from '../shared/LoginResponseModel';
import { UserModel } from '../shared/Usermodel';
import { HttpClient } from '@angular/common/http';

@Injectable({
      providedIn: 'root'
})
export class LoginService {

      constructor(private http: HttpClient, public router: Router) { }

      BaseUrl = "https://localhost:5001/api/user/";

      // Login Check For Normal Users
      LoginCheck(_username: string, _password: string): Observable<LoginResponseModel> {

            // let Url = this.BaseUrl + 'login';
            let url = "https://localhost:5001/api/user/login"
            const body = { username: _username, password: _password }
            const headers = { 'content-type': 'application/json' };
            return this.http.post<LoginResponseModel>(url, JSON.stringify(body), { headers: headers });

      }

      // SignupCheck For Users
      SignupCheck(body: UserModel): Observable<LoginResponseModel> {
            let Url = this.BaseUrl + 'signup';
            const headers = { 'content-type': 'application/json' };
            return this.http.post<LoginResponseModel>(Url, JSON.stringify(body), { headers: headers });
      }
}
