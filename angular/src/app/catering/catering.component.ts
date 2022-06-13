import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CateringServiceService } from '../services/catering-service.service';
import { CateringModel } from '../shared/catering.model';

@Component({
  selector: 'app-catering',
  templateUrl: './catering.component.html',
  styleUrls: ['./catering.component.css']
})
export class CateringComponent implements OnInit {

  searchString:string='';
  catering!: any[];
  constructor( private routes: Router, private service : CateringServiceService) { }
  

  ngOnInit(): void {

    this.getCatering()
  }
  // View(hall: number)
  // {
  //   this.routes.navigate(['halls/hallv'])
  // }
  getCatering()
  {
    this.service.getCatering().subscribe(data =>{
      this.catering= data;
      console.log(data);
    })
  }
  

}
// export interface CookShow{
//   Special:string,
//   Experience: number,
//   name:string,
//   img:string

// }