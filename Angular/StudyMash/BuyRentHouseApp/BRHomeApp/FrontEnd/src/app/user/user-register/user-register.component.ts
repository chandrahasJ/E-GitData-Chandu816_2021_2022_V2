import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import CustomValidations from 'src/app/helper/validation';
import { IUserForRegister } from 'src/app/models/user';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registerationForm!: FormGroup;
  userData! : IUserForRegister;
  regexpNumber = /[0-9\+\-\ ]/;
  submittedData = false;
  constructor(private formBuilder : FormBuilder,
              private authService : AuthService,
              private alertifyService: AlertifyService) { }

  ngOnInit(): void {
    // this.registerationForm = new FormGroup({
    //   userName : new FormControl(null,Validators.required),
    //   email : new FormControl(null,[Validators.required, Validators.email]),
    //   password : new FormControl(null, [Validators.required, Validators.minLength(8)]),
    //   confirmPassword : new FormControl(null, [Validators.required]),
    //    mobile : new FormControl(null,[Validators.required, Validators.maxLength(10)])
    // }, {
    //   validators : CustomValidations.match('password','confirmPassword')
    // });

    this.createRegisterationControls();
  }

  createRegisterationControls(){
    this.registerationForm = this.formBuilder.group({
      userName :[null,Validators.required],
      email :[null,[Validators.required, Validators.email]],
      password :[null, [Validators.required, Validators.minLength(8)]],
      confirmPassword : [null, [Validators.required]],
       mobile : [null,[Validators.required, Validators.maxLength(10)]]
    },{
      Validators : CustomValidations.match('password','confirmPassword')
    });
  }

  getUserData() : IUserForRegister{
    return this.userData = {
      userName : this.userName.value,
      email : this.email.value,
      password : this.password.value,
      mobile : this.mobile.value
    };
  }

  get userName(){
    return this.registerationForm.get('userName') as FormControl;
  }

  get email(){
    return this.registerationForm.get('email') as FormControl;
  }

  get password(){
    return this.registerationForm.get('password') as FormControl;
  }

  get confirmPassword(){
    return this.registerationForm.get('confirmPassword') as FormControl;
  }

  get mobile(){
    return this.registerationForm.get('mobile') as FormControl;
  }

  onSubmit(){
    console.log(this.registerationForm.value);
    this.submittedData = true;
    if(this.registerationForm.valid){
      //this.userData = Object.assign(this.userData, this.registerationForm.value);
      this.authService.addUsers(this.getUserData()).subscribe(() => {
        this.registerationForm.reset();
        this.submittedData = false;
        this.alertifyService.success("Your are successfully registered.");
      });
    }
    else{
      this.alertifyService.error("Kindly provide the required  fields");
    }
  }
}
