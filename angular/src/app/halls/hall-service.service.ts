import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HallModel } from '../shared/hall.model';
@Injectable({
  providedIn: 'root'
})
export class HallServiceService {
readonly API ="https://localhost:5001/Hall/"
  constructor(private http: HttpClient) { }
  hall !:HallModel;
  
  gethalls():Observable<HallModel[]>{
  let Url =this.API+"Get_All_Halls_Cards"
  return this.http.get<HallModel[]>(Url);
  }

}
