import { Guid } from "guid-typescript";

export interface HallModel{

    hallId: Guid;
    ownerUsername: string
    displayImg:string
    hallName:string
    location:string
    description:string

}