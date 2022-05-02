import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { HomeserviceService } from '../services/homeservice.service';

@Component({
      selector: 'app-home',
      templateUrl: './home.component.html',
      styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
      data!: HallsShow[];
      cook!: CookShow[];
      photo!: PhotoGrapher[]

      constructor(private routes: Router , private home : HomeserviceService) { 
      
      }

      ngOnInit(): void {
            let data: HallsShow[];
            let cook: CookShow[];
            let photo: PhotoGrapher[];
            this.cook = [
                  {
                        "Special": "CJ Pallazio",
                        "Experience": 14,
                        "name": "Raja Kannu",
                        "img": "https://source.unsplash.com/600x900/?cook"
                  },
                  {
                        "Special": "CJ Pallazio",
                        "Experience": 14,
                        "name": "Raja Kannu",
                        "img": "https://source.unsplash.com/600x900/?cook"
                  },
                  {
                        "Special": "CJ Pallazio",
                        "Experience": 14,
                        "name": "Raja Kannu",
                        "img": "https://source.unsplash.com/600x900/?cook"
                  },

            ]
            this.data = [
                  {
                        "id":"83090584-BAD7-41D2-D06C-08DA2AB4423E",
                        "HallName": "CJ Pallazio",
                        "Location": "salem",
                        "Description": "magna nisi esse ipsum irure ullamco qui qui dolore anim",
                        "img": "https://source.unsplash.com/600x900/?tech,street"
                  },
                  {
                        "id": "1B71E1CF-55FF-4B6D-D06B-08DA2AB4423E",
                        "HallName": "CJ Pallazio",
                        "Location": "chennai",
                        "Description": "aute tempor enim ad excepteur sit pariatur exercitation ea labore",
                        "img": "https://source.unsplash.com/600x900/?tree,nature"
                  },
                  {
                        "id":"DA0BD2D1-AB71-483D-D06D-08DA2AB4423E",
                        "HallName": "JJ Pallazio",
                        "Location": "chennai",
                        "Description": "duis labore et irure quis aliqua aliquip aute non aliquip",
                        "img":
                              "https://source.unsplash.com/600x900/?computer,design"
                  },

            ]

            this.photo = [
                  {
                        title: "Wedding Photogrphy",
                        Experience: 3,
                        Name: "Vijay",
                        img: "https://source.unsplash.com/600x900/?computer,design"

                  },
                  {
                        title: "Specialist in Candid",
                        Experience: 15,
                        Name: "Rama Subbu",
                        img: "https://source.unsplash.com/600x900/?tech,street"

                  },
                  {
                        title: "Creative Pic's",
                        Experience: 5,
                        Name: "Kanaga Raja",
                        img: "https://source.unsplash.com/600x900/?tree,nature"

                  }
            ]


      }
      View(hallId: string) {
            this.home.GetHallData(hallId).subscribe({
                  next : (data) => {
                        // this.routes.navigate(['halls/'])
                        console.log(data);
                  },
                  error : (e) =>{
                        console.error(e);
                  }
            })
      }
}
export interface CookShow {
      Special: string,
      Experience: number,
      name: string,
      img: string

}
export interface HallsShow {
      id: string,
      HallName: string;
      Description: string;
      Location: string;
      img: string;
}

export interface PhotoGrapher {
      title: string;
      Experience: number;
      Name: string;
      img: string;
}



