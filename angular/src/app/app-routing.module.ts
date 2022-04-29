import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './Auth/login/login.component';
import { SignupComponent } from './Auth/signup/signup.component';
import { HallsComponent } from './halls/halls.component';
import { CateringComponent } from './catering/catering.component';
import { PhotographersComponent } from './photographers/photographers.component';
import { BsignupComponent } from './Auth/bsignup/bsignup.component';

const routes: Routes = [
      { path: "", component: HomeComponent, pathMatch: 'full', },
      { path: "home", component: HomeComponent },
      { path: "login", component: LoginComponent },
      { path: "signup", component: SignupComponent },
      { path: "bsignup", component: BsignupComponent },
      { path: "halls", component: HallsComponent },
      { path: "catering", component: CateringComponent },
      { path: "photographer", component: PhotographersComponent }
];

@NgModule({
      imports: [RouterModule.forRoot(routes)],
      exports: [RouterModule]
})
export class AppRoutingModule { }
