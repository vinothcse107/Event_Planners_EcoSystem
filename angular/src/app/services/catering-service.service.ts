import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { CateringModel } from '../shared/catering.model';

@Injectable({
  providedIn: 'root'
})
export class CateringServiceService {
  readonly APIUrl="https://localhost:5001/Catering/"
  constructor(private http:HttpClient) { }
  getCatering():Observable<any[]>
  {
    let Url=this.APIUrl+'Get_All_Catering_Cards'
    return this.http.get<any[]>(Url);
 
  }
}
