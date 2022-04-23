import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BsignupComponent } from './bsignup/bsignup.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
  { path: "", redirectTo: "/home", pathMatch: 'full' },
  { path: "home", component: HomeComponent },
  { path:"login", component:LoginComponent},
  {path:"signup", component:SignupComponent},
  {path:"bb", component:BsignupComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
