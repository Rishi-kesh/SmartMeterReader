import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
    p: number = 1;
    userList: any = [];
    myUrl: any = [];
    requestOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('userToken'),

        })
    };
    edit(id: string) {
        this._router.navigate(['counter/'+id]);
    }
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private _router: Router) {
        debugger;
        this.myUrl = baseUrl;
        http.get(baseUrl + 'api/Account/GetUserDetailList', this.requestOptions).subscribe(result => {
            debugger;

            this.userList = result;

    }, error => console.error(error));
    }



    unit(id: string) {
        this._router.navigate(['unit/' + id]);
    }
}


