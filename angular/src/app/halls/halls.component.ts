import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { HallModel } from '../shared/hall.model';
import { HallServiceService } from './hall-service.service';

@Component({
  selector: 'app-halls',
  templateUrl: './halls.component.html',
  styleUrls: ['./halls.component.css']
})
export class HallsComponent implements OnInit {
  halls!: HallModel[];
  searchString:string='';

  constructor( private routes: Router, private  service : HallServiceService) { }

  ngOnInit(): void {
   this.getHalls();
  }
  getHalls()
  {
    this.service.gethalls().subscribe(data =>{
      this.halls= data;
      console.log(this.halls);
    })
  }
  View(id :number)
  {

  }
}
