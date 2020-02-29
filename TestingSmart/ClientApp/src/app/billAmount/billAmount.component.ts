import { OnInit, Inject, Component } from "@angular/core";
import { Router } from "@angular/router";
import {  HttpClient, HttpHeaders } from '@angular/common/http';
import { delay } from "q";
import { ToastrService } from 'ngx-toastr'
@Component({
    selector: 'billAmount-unit',
    templateUrl: './billAmount.component.html',
})
export class BillAmountComponent implements OnInit {

    myUrl: string;
    isClient: boolean = false;
    constructor(private _router: Router, private _http: HttpClient ,private toastr: ToastrService, @Inject('BASE_URL') baseUrl: string) {
        this.myUrl = baseUrl;
        let username = localStorage.getItem('userName');
        if (username == null || username == "") {

            this._router.navigate(['login']);

        }
        else {
            if (username == "Avanish1") {
                this.isClient = true;
            }
        }
    }

    unitData: any;
    billData: any;
    isShown: boolean = false;
    rep: any;
    ngOnInit(): void {
        debugger;
        //this._http.get(this.myUrl + 'api/Units', this.requestOptions).subscribe((res: any) => {
        //    this.unitData = res;
       
        //this._http.post(this.myUrl + 'api/BillAmounts', this.unitData[0], this.requestOptions).subscribe((result: any) => {
        //    debugger;
        //    this.billData = result;
            
        //        this.isShown = true;
          
           
           

        //}, error => alert(error.error.text));
        //});

    }

    requestOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('userToken'),

        })
    };
    totalPrice: number;
    payment() {
        debugger;
        
        var amount = this.totalPrice;
        if (amount < 50) {
            this.toastr.warning("Amount should be atleast 100");
            return;
        }
            var data = {
            userId: '7b4118ea-54ef-4a3b-a5ce-17340332468b',
            payingAmount: amount
        }
        this._http.post(this.myUrl + 'api/BillPayments', data, this.requestOptions).subscribe((result: any) => {
            debugger;
            if (result.amount > 0) {
                debugger;
                this._http.get('https://api.thingspeak.com/update?api_key=ABY8NXKF8QVK8E1Z&field5=1').subscribe((res: any) => {

                });
                setTimeout(() => {
                    this._http.get('https://api.thingspeak.com/update?api_key=ABY8NXKF8QVK8E1Z&field5=1').subscribe((res: any) => {

                    });
                },
                    5000);
                setTimeout(() => {
                    this._http.get('https://api.thingspeak.com/update?api_key=ABY8NXKF8QVK8E1Z&field5=1').subscribe((res: any) => {

                    });
                },
                    10000);
            }
            this.toastr.success("Successfully Paid "+amount);
            this.totalPrice =0;
            this.ngOnInit();
           
        }, error => alert(error.error.text));

        this.isShown = false;
    }
    
}

