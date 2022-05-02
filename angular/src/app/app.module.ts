import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './Auth/login/login.component';
import { SignupComponent } from './Auth/signup/signup.component';
import { HttpClientModule } from '@angular/common/http';
import { HallsComponent } from './halls/halls.component';
import { HallSearchPipe } from './halls/hall-search.pipe';
import { PhotographersComponent } from './photographers/photographers.component';
import { CateringComponent } from './catering/catering.component';
import { BsignupComponent } from './Auth/bsignup/bsignup.component';
import { HallviewComponent } from './halls/hallview/hallview.component';


@NgModule({
      declarations: [
            AppComponent,
            HomeComponent,
            LoginComponent,
            SignupComponent,
            HallsComponent,
            HallSearchPipe,
            PhotographersComponent,
            CateringComponent,
            BsignupComponent,
            HallviewComponent
      ],
      imports: [
            BrowserModule,
            HttpClientModule,
            AppRoutingModule,
            FormsModule,
            ReactiveFormsModule,
      ],
      providers: [],
      bootstrap: [AppComponent]
})
export class AppModule { }
