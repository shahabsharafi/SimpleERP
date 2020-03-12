export class IDocumentModel {
  id?: number;
  no: string;
}


export class DocumentModel implements IDocumentModel {
  constructor(
    public id: number | null,
    public no: string
  ) { }
}
