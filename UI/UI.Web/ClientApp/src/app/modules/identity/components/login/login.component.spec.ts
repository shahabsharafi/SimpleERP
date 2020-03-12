/* tslint:disable:no-unused-variable */

import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { LoginComponent } from "./login.component";
import { AccountService, AccountFackService } from '../../services/acount/account.service';



describe('Component: Login', () => {

  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let VALUE: string = "AAAA";

  beforeEach(() => {

    // refine the test module by declaring the test component
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, FormsModule],
      declarations: [LoginComponent],
      providers: [
        { provide: 'RESOURCE', useValue: {}, deps: [] },
        { provide: AccountService, useClass: AccountFackService }
      ],
    });

    // create component and test fixture
    fixture = TestBed.createComponent(LoginComponent);

    // get test component from the fixture
    component = fixture.componentInstance;
    component.ngOnInit();
  });

  it('form invalid when empty', () => {
    expect(component.form.valid).toBeFalsy();
  });

  it('userName field validity', () => {
    let errors = {};
    let userName = component.form.controls['userName'];

    // vin field is required
    errors = userName.errors || {};
    expect(errors['required']).toBeTruthy();

    // Set vin to something
    userName.setValue(VALUE);
    errors = userName.errors || {};
    expect(errors['required']).toBeFalsy();
  });

  it('password field validity', () => {
    let errors = {};
    let password = component.form.controls['password'];

    // color field is required
    errors = password.errors || {};
    expect(errors['required']).toBeTruthy();

    // Set color to something
    password.setValue(VALUE);
    errors = password.errors || {};
    expect(errors['required']).toBeFalsy();
  });

  it('submitting a form emits a user', () => {
    expect(component.form.valid).toBeFalsy();
    component.form.controls['userName'].setValue(VALUE);
    component.form.controls['password'].setValue(VALUE);
    expect(component.form.valid).toBeTruthy();
  });
});
