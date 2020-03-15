export interface IFileModel {
  path: string;
}


export class FileModel implements IFileModel {
  constructor(
    public path: string
  ) { }
}
