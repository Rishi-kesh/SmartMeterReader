import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    constructor(private _router: Router) {

    }
    userName: string = "";
    ngOnInit(): void {
        this.userName = localStorage.getItem('userName');
        if (this.userName == null || this.userName == "") {
            this._router.navigate(['login']);

        }
    }
}
