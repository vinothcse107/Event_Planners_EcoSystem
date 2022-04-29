import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'hallSearch'
})
export class HallSearchPipe implements PipeTransform {

  transform(value: any,searchString:string, propName: string):any[]{
    if(value.length===0 || searchString===""){
      return value;
    }
    const result: any =[]

    value.forEach((a:any) => {
      if(a[propName].trim().toLowerCase().includes(searchString.toLocaleLowerCase())){
        result.push(a);
      }
    });
    return result;
  }

}
