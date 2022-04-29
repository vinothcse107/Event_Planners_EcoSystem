import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-catering',
  templateUrl: './catering.component.html',
  styleUrls: ['./catering.component.css']
})
export class CateringComponent implements OnInit {

  searchString:string='';
  cook!: CookShow[];
  constructor( private routes: Router) { }
  

  ngOnInit(): void {
    let cook: CookShow[];
      this.cook = [
        {
          "Special": "Dehli Idly",
          "Experience": 14,
          "name":"Raja Kannu",
          "img" : "https://source.unsplash.com/600x900/?cook"
        },
        {
          "Special": "Bombay Chutney ",
          "Experience": 14,
          "name":"Raja Kannu",
          "img" : "https://source.unsplash.com/600x900/?cook"
        },
        {
          "Special": "Mysore Vada",
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
export interface CookShow{
  Special:string,
  Experience: number,
  name:string,
  img:string

}