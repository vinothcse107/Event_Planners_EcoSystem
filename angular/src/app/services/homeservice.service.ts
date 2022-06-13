import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeserviceService {

  constructor(public http: HttpClient, public router: Router) {


  }
  BaseUrl = "http://localhost:5000/";
  Plans!: any;

  // *----------------*--------------*-------------

  /**
   * GetAddons Methods Receive a Array of Objects 
   * Action : GetAll Addons
   * Type : @class AddonModel
   * return AddonsModel[]
   * 
   * Used In ViewAddonComponent Class 
   */

  GetHallData(Hallid : string): Observable<any[]> {
    let Url = this.BaseUrl + "Hall/Get_Hall_Details/"+Hallid;
    return this.http.get<any>(Url);
  }

  // GetHallData(Hallid : string): Observable<any[]> {
  //   let Url = this.BaseUrl + "Hall/Get_Hall_Details/"+Hallid;
  //   return this.http.get<any>(Url);
  // }
}