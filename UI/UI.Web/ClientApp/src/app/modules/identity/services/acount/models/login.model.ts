export class ILoginModel {
  userName: string;
  password: string;
  rememberMe: Boolean;
}


export class LoginModel implements ILoginModel {
  constructor(
    public userName: string,
    public password: string,
    public rememberMe: Boolean
  ) { }
}
