export class IRoleModel {
  name: string;
}


export class RoleModel implements IRoleModel {
  constructor(
    public name: string
  ) { }
}
