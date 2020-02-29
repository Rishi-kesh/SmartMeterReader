import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Subscription, timer } from 'rxjs';
import {  HttpClient, HttpHeaders } from "@angular/common/http";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
    constructor(private _router: Router, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.myUrl = baseUrl;

    }
    userName: string = "";
    myUrl: string = "";

    unit: string = "";
    unitData: any;
    hide: boolean = false;
    ngOnInit(): void {
        this.userName = localStorage.getItem('userName');
        if (this.userName == 'Avanish1')
            this.hide = true;
    }
    requestOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('userToken'),

        })
    };
    
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
    }

    logOut() {
        localStorage.removeItem('userToken');
        localStorage.removeItem('userName');
        this._router.navigate(['login']);

    }   
       
    
}
