import { Component, OnInit, Inject } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse, HttpHeaders, HttpClient } from '@angular/common/http';
import { AppComponent } from '../app.component';

import { Login } from './login';
//import { } from '../../../../ClientApp/assets/img/'

@Component({
    selector: 'app-sign-in',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
    title: string = "Create";
    id: string = "";
    isLoginError: boolean = false;
    errorMessage: any;
    userName: string = "";
    myUrl: string = "";
    constructor(private app: AppComponent, private _router: Router, private _avRoute: ActivatedRoute, private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.myUrl = baseUrl;

    }
    login: Login = new Login();
    tokenData: any;
   
    bg: any;
    bgList: any;
    ngOnInit() {
        
    }
    changeBg(list: any) {
        console.log(list);
        this.bg = list.bg;
        list.active = true;

        for (let bList of this.bgList) {
            if (bList != list) {
                bList.active = false;
            }
        }
    }


    requestOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            //'Authorization': 'Bearer ' + localStorage.getItem('userToken'),
        })
    };
    
    saveLogin(userName: string, password: string): void {
        console.log(userName);

        this.login.userName = userName;
        this.login.password = password;
        this._http.post(this.myUrl + 'api/Users/authenticate', this.login, this.requestOptions).subscribe((result:any) => {
            debugger;
            if (result == "error") {
                alert("Username or password is incorrect!");
                return true;
            }
            localStorage.setItem('userToken', result.securityStamp);
            localStorage.setItem('userName', result.userName);
            this._router.navigate(['home']);
        }, error => alert(error.error.text));

    }
   
}

