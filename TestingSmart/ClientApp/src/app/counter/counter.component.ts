
import { Component, OnInit, Inject } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse, HttpHeaders, HttpClient } from '@angular/common/http';
import { AppComponent } from '../app.component';
import { Register } from './register';

@Component({
    selector: 'counter-in',
    templateUrl: './counter.component.html'
})
export class CounterComponent implements OnInit {
    registerForm: FormGroup;
    submitted = false;
    myUrl: string = "";
    id: string = "";
    constructor(private app: AppComponent, private formBuilder: FormBuilder, private _router: Router, private _avRoute: ActivatedRoute, private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.myUrl = baseUrl;
        if (this._avRoute.snapshot.params["id"]) {
            this.id = this._avRoute.snapshot.params["id"];

        }
        let username = localStorage.getItem('userName');
        if (username == null || username == "") {
            this._router.navigate(['login']);

        }


    }

    userType: any = ['Customer','Admin'];
    register: Register = new Register();
    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            fullName: ['', Validators.required],
            email: ['', Validators.required],
            userName: ['', Validators.required],

            password: ['', [Validators.required, Validators.minLength(6)]],
            phoneNumber: ['', Validators.required],
            address: ['', Validators.required],
            userType: ['Customer', Validators.required],
            id:['']

        });
        if (this.id != null && this.id != "") {
            
            this._http.get(this.myUrl + 'api/Account/GetUserDetailById/'+this.id, this.requestOptions).subscribe((result: any) => {
                debugger;
                this.registerForm = this.formBuilder.group({
                    fullName: [result.fullName, Validators.required],
                    email: [result.email, Validators.required],
                    userName: [result.userName, Validators.required],

                    password: [result.password, [Validators.required, Validators.minLength(6)]],
                    phoneNumber: [result.phoneNumber, Validators.required],
                    address: [result.address, Validators.required],
                    userType: [result.userType, Validators.required],
                    id:[result.id]

                });                

          
             

            }, error => alert(error.error.text));
        }
    }
    get f() { return this.registerForm.controls; }

    cancel() {
        this.ngOnInit();
    }
    changeUserType(value: any) {
        debugger;

    }
    requestOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('userToken'),

        })
    };
    onSubmit() {
        if (this.id == null || this.id == "") {
            var value = this.registerForm.value;
            if (!this.registerForm.valid) {
                alert("Fill required filed!");
                return false;
            }
            this._http.post(this.myUrl + 'api/Account/RegisterUser', value, this.requestOptions).subscribe((result: any) => {
                debugger;
                if (result == "error") {
                    alert("error submitting form!");
                    return true;

                }
                alert("User created!");
                this.registerForm.reset(this.registerForm.value);

                this._router.navigate(['counter']);

            }, error => alert(error.error.text));
        }
        else {
            debugger;
            var value = this.registerForm.value;
            value.id = this.id;
            if (!this.registerForm.valid) {
                alert("Fill required filed!");
                return false;
            }
            this._http.post(this.myUrl + 'api/Account/UpdateUser', value, this.requestOptions).subscribe((result: any) => {
                debugger;
                if (result == "error") {
                    alert("error submitting form!");
                    return true;

                }
                alert("User updated!");

                this._router.navigate(['counter']);

            }, error => alert(error.error.text));
        }
      
    }






    

}

