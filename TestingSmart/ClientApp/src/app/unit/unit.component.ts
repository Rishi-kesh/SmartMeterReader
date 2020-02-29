import { Component, OnInit, Inject } from "@angular/core";
import { AppComponent } from "../app.component";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription, timer, pipe } from 'rxjs';
import { switchMap, delay } from 'rxjs/operators';
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { ToastrService } from 'ngx-toastr';
@Component({
    selector: 'app-unit',
    templateUrl: './unit.component.html',
})
export class UnitComponent {
    p: number = 1;
    myUrl: string = "";
    id: string = "";
    subscription: Subscription;
    subs: Subscription;
    statusText: string;
    statustext: any;
    statusHistory: any;
    unitHistory: any;
    constructor(private http: HttpClient, private _avRoute: ActivatedRoute, private toastr: ToastrService, private _router: Router,@Inject('BASE_URL') baseUrl: string) {
        this.myUrl = baseUrl;
        if (this._avRoute.snapshot.params["id"]) {
            this.id = this._avRoute.snapshot.params["id"];

        }
        let username = localStorage.getItem('userName');
        if (username == null || username == "") {
            this._router.navigate(['login']);
        }
      
    }
    unit: string = "";
    unitData: any;
    unitHis: any;
    unitHistoryData: any;
    ngOnInit() {
        debugger;
        if (this.id != '7b4118ea-54ef-4a3b-a5ce-17340332468b') {
            this.toastr.warning("Not Available User");

            this._router.navigateByUrl('/fetch-data');
            return;
        }
        this.http.get(this.myUrl + 'api/Units', this.requestOptions).subscribe((res: any) => {
            this.unitData = res;
            debugger;
        });
        this.http.get(this.myUrl + 'api/UnitHistories', this.requestOptions).subscribe((res: any) => {
            this.unitHistoryData = res;
        });
        this.sub();
    }
   
    requestOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('userToken'),

        })
    };
    sub() {
        
        this.subscription = timer(0, 15000).pipe(
                
                switchMap(() => this.checkdata())
            ).subscribe(result => {
                this.statustext = result,
                    console.log(this.statustext);
                if (this.statustext.feeds.length > 0) {
                    debugger;
                    let item = this.statustext.feeds[1].field1;
                    // alert(item);
                    if (item == null)
                        return;
                    this.unit = item;
                    var mdate = this.statustext.feeds[1].entry_id;
                    let units = +this.unit;
                    var data = {
                        unites: units,
                        userId: '7b4118ea-54ef-4a3b-a5ce-17340332468b',

                    };

                    this.http.post(this.myUrl + 'api/Units', JSON.stringify(data), this.requestOptions).subscribe(async (result: any) => {
                        debugger;
                        let cutLine = result;
                        if (cutLine) {
                            debugger;
                            this.http.get('https://api.thingspeak.com/update?api_key=ABY8NXKF8QVK8E1Z&field5=0').subscribe((res: any) => {

                            });
                            setTimeout(() => {
                                this.http.get('https://api.thingspeak.com/update?api_key=ABY8NXKF8QVK8E1Z&field5=0').subscribe((res: any) => {

                                });
                            },
                                8000);
                            setTimeout(() => {
                                this.http.get('https://api.thingspeak.com/update?api_key=ABY8NXKF8QVK8E1Z&field5=0').subscribe((res: any) => {

                                });
                            },
                                8000);
                           
                        }
                       
                        this.http.get(this.myUrl + 'api/Units', this.requestOptions).subscribe((res: any) => {
                            this.unitData = res;
                        });



                    }, error => alert(error.error.text));
                }

            });
        this.subs = timer(3600000, 3600000).pipe(
            switchMap(() => this.history())
        ).subscribe(result => {
            this.statusHistory = result
            debugger;
            var data = {
                userId: '7b4118ea-54ef-4a3b-a5ce-17340332468b',
                units: this.statusHistory[0].unites
            }
            this.http.post(this.myUrl + 'api/UnitHistories', JSON.stringify(data), this.requestOptions).subscribe((result: any) => {
                
                
            }, error => alert(error.error.text));      
            this.http.get(this.myUrl + 'api/UnitHistories', this.requestOptions).subscribe((res: any) => {
                this.unitHistoryData = res;
            });
        });

    }
    history(): any {
       return this.http.get(this.myUrl + 'api/Units', this.requestOptions)
            
    }
    checkdata(): any {
       
            return this.http.get('https://api.thingspeak.com/channels/857055/fields/1.json?api_key=V4P0S99X2O49DU7R&results=2'
            );
        
    }

    bill() {
        this._router.navigate(['billAmount']);
    }
}
