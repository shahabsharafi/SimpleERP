export class IAlertModel {
  type: string;
  text: string;
}


export class AlertModel implements IAlertModel {
  constructor(
    public type: string,
    public text: string
  ) { }
}
