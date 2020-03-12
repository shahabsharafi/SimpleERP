import { Injectable, Inject } from '@angular/core';
import { ILoginModel, IRegisterModel, IRoleModel, RoleModel } from './models';
import { IApiDataResult, ApiDataResult, ITokenModel, TokenService } from '../../../../infrastructures';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AccountService {

  private _url: string;

  constructor(private rest: HttpClient, private tokenService: TokenService, @Inject('IDENTITY_SERVICE_URL') private baseUrl: string) {
    this._url = this.baseUrl + 'api/Account';
  }

  login(model: ILoginModel) {
    this.rest.post<IApiDataResult<ITokenModel>>(this._url + '/Login', model)
      .toPromise().then(response => {
        if (response) {
          if (response.isSuccess) {
            this.tokenService.updateToken(response.data);
          } else {
            alert(response.message);
          }
        }
      });
  }

  register(model: IRegisterModel) {
    this.rest.post<IApiDataResult<string>>(this._url + '/Register', model)
      .toPromise().then(response => {
        if (response) {
          if (!response.isSuccess) {
            alert(response.message);
          }
        }
      });
  }

  getRoles(): Observable<IApiDataResult<IRoleModel[]>> {
    return this.rest.get<IApiDataResult<IRoleModel[]>>(this._url + '/GetRoles');
  }
}

export class AccountFackService extends AccountService {

  static ROLES: IRoleModel[] = [new RoleModel('')];

  constructor() { super(null, null, null); }

  login(model: ILoginModel) { }

  register(model: IRegisterModel) { }

  getToken(): string { return '' }

  getRoles(): Observable<IApiDataResult<IRoleModel[]>> {
    var obj = new ApiDataResult<IRoleModel[]>(true, 200, "", AccountFackService.ROLES);
    return of(obj);
  }
}
