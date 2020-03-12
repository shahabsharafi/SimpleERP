import { AccountService } from './account.service';
import { LoginModel, RegisterModel } from './models';
import { async } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

class RestFackService extends HttpClient {

  constructor() { super(null) }

  post<T>(url: string, body: any | null, options?: any): Observable<T> {
    return of(null);
  }
}

describe('AccountService', () => {
  var restService, get_Spy;
  beforeEach(async(() => {
    restService = new RestFackService();
    get_Spy = spyOn(restService, 'post').and.callThrough();
  }));

  it(`check login`, () => {
    let accountService = new AccountService(restService, null, '');
    accountService.login(new LoginModel('', '', false));
    expect(get_Spy).toHaveBeenCalled();
    //expect(accountService.getToken()).toBe(AccountService.To)
  });

  it(`check register`, () => {
    let accountService = new AccountService(restService, null, '');
    accountService.register(new RegisterModel('', '', true, ''));
    expect(get_Spy).toHaveBeenCalled();
  });
});
