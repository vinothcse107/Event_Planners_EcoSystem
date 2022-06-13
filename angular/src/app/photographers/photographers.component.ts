import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PhotographerService } from '../services/photographer.service';
import { PhotoModel } from '../shared/photographer.model';

@Component({
  selector: 'app-photographers',
  templateUrl: './photographers.component.html',
  styleUrls: ['./photographers.component.css']
})
export class PhotographersComponent implements OnInit {

  photo!: any[];
  searchString:any="";

  constructor(private routes: Router, private service : PhotographerService) { }

  ngOnInit(): void {
      this.getPhotographer();
  }
  getPhotographer()
  {
    this.service.getPhotographer().subscribe(data =>{
      this.photo= data;
      console.log("data");
    })
  }
  View(hall: number)
  {
    this.routes.navigate(['halls/hallv'])


  }
}

