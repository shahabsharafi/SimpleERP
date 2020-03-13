export interface IDocumentInfoModel {
  id?: number;
  no: string;
}


export class DocumentInfoModel implements IDocumentInfoModel {
  constructor(
    public id: number | null,
    public no: string
  ) { }
}
