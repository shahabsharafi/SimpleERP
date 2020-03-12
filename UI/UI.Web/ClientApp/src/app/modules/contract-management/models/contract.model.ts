export class IContractModel {
  uniqueId?: any;
  id?: number;
  no: string;
  title: string;
  archived: boolean;
  startDate: string
}


export class ContractModel implements IContractModel {
  constructor(
    public uniqueId: any | null,
    public id: number | null,
    public no: string,
    public title: string,
    public archived: boolean,
    public startDate: string
  ) { }
}
