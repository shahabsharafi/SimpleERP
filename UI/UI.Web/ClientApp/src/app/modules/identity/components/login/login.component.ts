import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { LoginModel } from '../../services/acount/models';
import { AccountService } from '../../../identity/services/acount/account.service';
import { MessageService } from '../../../../infrastructures/services/message.service';
import { MessageModel } from '../../../../infrastructures';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  constructor(private fb: FormBuilder, @Inject('RESOURCE') public resource: any, private accountService: AccountService, private messageService: MessageService) {
    //this.subscribeToEvents();
  }

  private subscribeToEvents(): void {
    this.messageService.messageReceived.subscribe((message: MessageModel) => {
      alert(message.body);
    });
  }  

  ngOnInit() {
    this.form = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      rememberMe: []
    });
  }

  Login() {
    console.log(`Login ${this.form.value}`);
    if (this.form.valid) {
      let model = new LoginModel(
        this.form.value.userName,
        this.form.value.password,
        this.form.value.rememberMe || false
      );
      this.accountService.login(model);
    }
  }
}
