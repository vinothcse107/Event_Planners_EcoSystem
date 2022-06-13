import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { CateringModel } from '../shared/catering.model';
import { PhotoModel } from '../shared/photographer.model';

@Injectable({
  providedIn: 'root'
})
export class PhotographerService {
readonly API="https://localhost:5001/Photographer/"
  constructor(private http :HttpClient) {
  }
  getPhotographer():Observable<any>
  {
    let Url=this.API+'Get_All_Photographer_Cards'
    return this.http.get<any>(Url);
 
  }
}
