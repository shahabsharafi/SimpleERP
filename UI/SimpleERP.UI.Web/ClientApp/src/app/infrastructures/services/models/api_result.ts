import { extend } from "webdriver-js-extender";
import { Observable } from "rxjs";

export interface IApiResult {
  isSuccess: Boolean;
  statusCode: number;
  message: string;
}


export class ApiResult implements IApiResult {
  constructor(
    public isSuccess: Boolean,
    public statusCode: number,
    public message: string
  ) { }
}

export interface IApiDataResult<TData> {
  isSuccess: Boolean;
  statusCode: number;
  message: string;
  data: TData;
}


export class ApiDataResult<TData> implements IApiDataResult<TData> {
  constructor(
    public isSuccess: Boolean,
    public statusCode: number,
    public message: string,
    public data: TData
  ) { }
}

export class ICollection<TData> {
  rows: TData[];
  rowCount: number;
}

export class Collection<TData> implements ICollection<TData> {
  constructor(
    public rows: TData[],
    public rowCount: number
  ) { }
}

export interface IApiCollectionResult<TData> extends IApiDataResult<ICollection<TData>> { }

export class ApiCollectionResult<TData> extends ApiDataResult<ICollection<TData>> { }

