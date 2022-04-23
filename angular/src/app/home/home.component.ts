import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  data!: HallsShow[];

  constructor() { }

  ngOnInit(): void {
      let data : HallsShow[];
      this.data = [
      {
        "HallName": "CJ Pallazio",
        "Location": "salem",
        "Description": "magna nisi esse ipsum irure ullamco qui qui dolore anim",
        "img" : "https://source.unsplash.com/600x900/?tech,street"
      },
      {
        "HallName": "CJ Pallazio",
        "Location": "chennai",
        "Description": "aute tempor enim ad excepteur sit pariatur exercitation ea labore",
        "img" : "https://source.unsplash.com/600x900/?tree,nature"
      },
      {
        "HallName": "CJ Pallazio",
        "Location": "chennai",
        "Description": "duis labore et irure quis aliqua aliquip aute non aliquip",
        "img" : "https://source.unsplash.com/600x900/?tree,nature"
      },
      {
        "HallName": "GRT",
        "Location": "banglore",
        "Description": "exercitation non consequat irure labore culpa fugiat occaecat nostrud officia",
        "img":"https://source.unsplash.com/600x900/?tree,nature"
      },
      {
        "HallName": "CJ Pallazio",
        "Location": "kochi",
        "Description": "in veniam magna sunt in Lorem consequat enim labore veniam",
        "img":"https://source.unsplash.com/600x900/?tree,nature"
      }
    ]

    
  }

}
export interface HallsShow{
  HallName : string;
  Description : string;
  Location : string; 
  img : string;
}