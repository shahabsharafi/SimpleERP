export class IRegisterModel {
  userName: string;
  password: string;
  passwordConfirmed: Boolean;
  email: string;
}


export class RegisterModel implements IRegisterModel {
  constructor(
    public userName: string,
    public password: string,
    public passwordConfirmed: Boolean,
    public email: string
  ) { }
}
