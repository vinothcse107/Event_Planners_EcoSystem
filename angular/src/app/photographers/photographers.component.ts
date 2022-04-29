import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-photographers',
  templateUrl: './photographers.component.html',
  styleUrls: ['./photographers.component.css']
})
export class PhotographersComponent implements OnInit {

  photo!: PhotographerShow[];
  searchString:any="";

  constructor(private routes: Router) { }

  ngOnInit(): void {
      let photo: PhotographerShow[];
      this.photo = [
        {
          "Special": "CJ Pallazio",
          "Experience": 14,
          "name":"Raja Kannu",
          "img" : "https://source.unsplash.com/600x900/?cook"
        },
        {
          "Special": "CJ Pallazio",
          "Experience": 14,
          "name":"Raja Kannu",
          "img" : "https://source.unsplash.com/600x900/?cook"
        },
        {
          "Special": "CJ Pallazio",
          "Experience": 14,
          "name":"Raja Kannu",
          "img" : "https://source.unsplash.com/600x900/?cook"
        },

      ]
  }
  View(hall: number)
  {
    this.routes.navigate(['halls/hallv'])


  }
}
export interface PhotographerShow{
  Special:string,
  Experience: number,
  name:string,
  img:string

}
