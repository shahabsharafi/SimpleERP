import { Injectable, Inject } from '@angular/core';
import { IContractModel, ContractModel } from '../../models';
import { IApiDataResult, ApiResult, ApiDataResult, ApiCollectionResult, ICollection, Collection, QueryString, TokenService } from '../../../../infrastructures';
import { Observable, of } from 'rxjs';
import { IGridParams, IGridExcelParams, GridExcelParams, GridService } from '../../../../infrastructures/services/models/grid-parameter';

import { HttpClient } from '@angular/common/http';

@Injectable()
export class ContractService extends GridService {

  private _url: string;

  constructor(private rest: HttpClient, @Inject('CONTRACT_MANAGEMENT_SERVICE_URL') private baseUrl: string) {
    super();
    this._url = this.baseUrl + 'api/contracts';   
  }

  getAll(request: IGridParams): Observable<ApiCollectionResult<IContractModel>> {
    const queryString: string = QueryString.serialize(request);
    let url: string = this._url + (queryString ? ('?' + queryString) : '');
    return this.rest.get<ApiCollectionResult<IContractModel>>(url);
  }

  getExcel(request: IGridParams): Observable<Blob> {
    const gridExcelParams: IGridExcelParams = new GridExcelParams(request.startRow, request.endRow, request.sortModel, request.filterModel, 1);
    const queryString: string = QueryString.serialize(gridExcelParams);
    return this.rest.get(this._url + (queryString ? ('?' + queryString) : ''), { responseType: 'blob' });
  }

  get(id: number): Observable<IApiDataResult<IContractModel>> {
    return this.rest.get<IApiDataResult<IContractModel>>(this._url + '/' + id);
  }

  save(model: IContractModel): Observable<IApiDataResult<IContractModel>> {
    let result: Observable<IApiDataResult<IContractModel>>;
    if (model.id) {
      result = this.rest.put<IApiDataResult<IContractModel>>(this._url + '/' + model.id, model);
    } else {
      model.no = Math.floor(Math.random() * 1000) + '';
      result = this.rest.post<IApiDataResult<IContractModel>>(this._url, model);
    }
    return result;
  }

  delete(id: number): Observable<ApiResult> {
    return this.rest.delete<ApiResult>(this._url + '/' + id);
  }

  deleteByFilter(request: IGridParams): Observable<ApiResult> {
    const queryString: string = QueryString.serialize(request);
    return this.rest.delete<ApiResult>(this._url + (queryString ? ('?' + queryString) : ''));
  }

  deleteByIds(ids: string[]): Observable<ApiResult> {
    const queryString: string = QueryString.serialize({ ids: ids });
    return this.rest.delete<ApiResult>(this._url + (queryString ? ('?' + queryString) : ''));
  }
}

export class ContractFackService extends ContractService {

  static CONTRACT: IContractModel = new ContractModel("", 0, '', '', false, (new Date()).toJSON());
  static CONTRACT_ARRAY: IContractModel[] = [ContractFackService.CONTRACT];
  static CONTRACTS: ICollection<IContractModel> = new Collection<IContractModel>(ContractFackService.CONTRACT_ARRAY, 1);

  constructor() { super(null, null); }

  getAll(request: IGridParams): Observable<ApiCollectionResult<IContractModel>> {
    var obj = new ApiCollectionResult<IContractModel>(true, 200, "", ContractFackService.CONTRACTS);
    return of(obj);
  }

  get(id: number): Observable<IApiDataResult<IContractModel>> {
    var obj = new ApiDataResult<IContractModel>(true, 200, "", ContractFackService.CONTRACT);
    return of(obj);
  }

  save(model: IContractModel): Observable<IApiDataResult<IContractModel>> { return of(null); }

  delete(id: number): Observable<ApiResult> { return of(null) }

  deleteByFilter(request: IGridParams): Observable<ApiResult> { return of(null) }

  deleteByIds(ids: string[]): Observable<ApiResult> { return of(null) }
}
