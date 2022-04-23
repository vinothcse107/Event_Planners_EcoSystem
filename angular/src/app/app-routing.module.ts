import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BsignupComponent } from './bsignup/bsignup.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './Auth/login/login.component';
import { SignupComponent } from './Auth/signup/signup.component';

const routes: Routes = [
  { path: "",component: HomeComponent, pathMatch: 'full', },
  { path: "home", component: HomeComponent },
  { path:"login", component:LoginComponent},
  {path:"signup", component:SignupComponent},
  {path:"blogin", component:BsignupComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
