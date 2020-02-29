import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './users/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { UnitComponent } from './unit/unit.component';
import { BillAmountComponent } from './billAmount/billAmount.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
        FetchDataComponent,
        LoginComponent,
        UnitComponent,
        BillAmountComponent,      
        
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      CommonModule,

      FormsModule,
      ReactiveFormsModule,
      NgxPaginationModule,
      BrowserAnimationsModule, // required animations module
      ToastrModule.forRoot({
          timeOut: 3000,

          positionClass: 'toast-top-right',
      }), 
      RouterModule.forRoot([
          { path: '', component: LoginComponent },
          { path: 'login', component: LoginComponent },
          { path: 'billAmount', component: BillAmountComponent },
      { path: 'home', component: HomeComponent },
          { path: 'counter', component: CounterComponent },
          { path: 'counter/:id', component: CounterComponent },
          { path: 'fetch-data', component: FetchDataComponent },
          { path: 'unit/:id', component: UnitComponent },



    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
