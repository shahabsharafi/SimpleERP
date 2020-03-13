export class ITokenModel {
  access_token: string;
  expires_in: number;
}


export class TokentModel implements ITokenModel {
  constructor(
    public access_token: string, public expires_in: number
  ) { }
}
