import { ApiCollectionResult } from ".";
import { Observable } from "rxjs";

export interface IGridParams {

  /** The first row index to get. */
  startRow: number;

  /** The first row index to NOT get. */
  endRow: number;

  /** If doing server side sorting, contains the sort model */
  sortModel: any;

  /** If doing server side filtering, contains the filter model */
  filterModel: any;
}

export class GridParams implements IGridParams {

  constructor(public startRow: number, public endRow: number, public sortModel: any, public filterModel: any) { }
}

export interface IGridExcelParams extends IGridParams {

  /** If excepted excel output */
  isExcel: number;
}

export class GridExcelParams implements IGridExcelParams {

  constructor(public startRow: number, public endRow: number, public sortModel: any, public filterModel: any, public isExcel: number) { }
}

export interface IEntity { id?: number; }

export abstract class GridService {
  abstract getAll(request: IGridParams): Observable<ApiCollectionResult<any>>;
}

