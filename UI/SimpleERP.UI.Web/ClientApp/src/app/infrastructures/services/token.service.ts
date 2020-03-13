import { Injectable } from "@angular/core";
import { ITokenModel } from "./models";
import { HttpHeaders, HttpParams } from "@angular/common/http";

@Injectable()
export class TokenService {

  private _now: Date;
  private _tokenModel: ITokenModel

  constructor() { }

  public updateToken(tokenModel: ITokenModel) {
    this._tokenModel = tokenModel;
    this._now = new Date();
  }

  public getToken(): string {
    if (this._tokenModel != null && this._tokenModel.access_token != null && this._tokenModel.expires_in != null) {
      //let d = this._now;
      //d.setSeconds(d.getSeconds() + this._tokenModel.expires_in);
      //if (d <= new Date())
        return this._tokenModel.access_token;
    }
    return null;
  }
}
