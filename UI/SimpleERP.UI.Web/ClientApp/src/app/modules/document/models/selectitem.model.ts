export interface ISelectItemModel {
  id?: number;
  title: string;
}


export class SelectItemModel implements ISelectItemModel {
  constructor(
    public id: number | null,
    public title: string
  ) { }
}
