export interface IDocumentInfoModel {
  id?: number;
  no: string;
  subject: string;
  text: string;
  dateOfRelease: string;
  dateOfCreate: string;
  creator: string;
  dateOfModify: string;
  modifier: string;
  issuerId: number;
  issuerTitle: string;
  domainId: number;
  domainTitle: string;
  typeId: number;
  typeTitle: string;
  DocumetFileIds: number[];
}


export class DocumentInfoModel implements IDocumentInfoModel {
  constructor(
    public id: number | null,
    public no: string,
    public subject: string,
    public text: string,
    public dateOfRelease: string,
    public dateOfCreate: string,
    public creator: string,
    public dateOfModify: string,
    public modifier: string,
    public issuerId: number,
    public issuerTitle: string,
    public domainId: number,
    public domainTitle: string,
    public typeId: number,
    public typeTitle: string,
    public DocumetFileIds: number[]
  ) { }
}
