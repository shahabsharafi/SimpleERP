import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { RegisterModel } from '../../services/acount/models';
import { AccountService } from '../../../identity/services/acount/account.service';

@Component({
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;

  constructor(private fb: FormBuilder, @Inject('RESOURCE') public resource: any, private accountService: AccountService) { }

  ngOnInit() {
    this.form = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      email: []
    });
  }

  Register() {
    console.log(`Register ${this.form.value}`);
    if (this.form.valid) {
      let model = new RegisterModel(
        this.form.value.userName,
        this.form.value.password,
        this.form.value.password === this.form.value.confirmPassword,
        this.form.value.email
      );
      this.accountService.register(model);
    }
  }
}
