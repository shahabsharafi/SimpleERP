import { Injectable, Inject } from '@angular/core';
import { IDocumentInfoModel, DocumentInfoModel } from '../../models';
import { IApiDataResult, ApiResult, ApiDataResult, ApiCollectionResult, ICollection, Collection, QueryString, TokenService } from '../../../../infrastructures';
import { Observable, of } from 'rxjs';
import { IGridParams, IGridExcelParams, GridExcelParams, GridService } from '../../../../infrastructures/services/models/grid-parameter';

import { HttpClient } from '@angular/common/http';

@Injectable()
export class DocumentInfoService extends GridService {

  private _url: string;

  constructor(private rest: HttpClient, @Inject('CONTRACT_MANAGEMENT_SERVICE_URL') private baseUrl: string) {
    super();
    this._url = this.baseUrl + 'api/contracts';   
  }

  getAll(request: IGridParams): Observable<ApiCollectionResult<IDocumentInfoModel>> {
    const queryString: string = QueryString.serialize(request);
    let url: string = this._url + (queryString ? ('?' + queryString) : '');
    return this.rest.get<ApiCollectionResult<IDocumentInfoModel>>(url);
  }

  getExcel(request: IGridParams): Observable<Blob> {
    const gridExcelParams: IGridExcelParams = new GridExcelParams(request.startRow, request.endRow, request.sortModel, request.filterModel, 1);
    const queryString: string = QueryString.serialize(gridExcelParams);
    return this.rest.get(this._url + (queryString ? ('?' + queryString) : ''), { responseType: 'blob' });
  }

  get(id: number): Observable<IApiDataResult<IDocumentInfoModel>> {
    return this.rest.get<IApiDataResult<IDocumentInfoModel>>(this._url + '/' + id);
  }

  save(model: IDocumentInfoModel): Observable<IApiDataResult<IDocumentInfoModel>> {
    let result: Observable<IApiDataResult<IDocumentInfoModel>>;
    if (model.id) {
      result = this.rest.put<IApiDataResult<IDocumentInfoModel>>(this._url + '/' + model.id, model);
    } else {
      model.no = Math.floor(Math.random() * 1000) + '';
      result = this.rest.post<IApiDataResult<IDocumentInfoModel>>(this._url, model);
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

export class DocumentInfoFackService extends DocumentInfoService {

  static CONTRACT: IDocumentInfoModel = new DocumentInfoModel(0, "");
  static CONTRACT_ARRAY: IDocumentInfoModel[] = [DocumentInfoFackService.CONTRACT];
  static CONTRACTS: ICollection<IDocumentInfoModel> = new Collection<IDocumentInfoModel>(DocumentInfoFackService.CONTRACT_ARRAY, 1);

  constructor() { super(null, null); }

  getAll(request: IGridParams): Observable<ApiCollectionResult<IDocumentInfoModel>> {
    var obj = new ApiCollectionResult<IDocumentInfoModel>(true, 200, "", DocumentInfoFackService.CONTRACTS);
    return of(obj);
  }

  get(id: number): Observable<IApiDataResult<IDocumentInfoModel>> {
    var obj = new ApiDataResult<IDocumentInfoModel>(true, 200, "", DocumentInfoFackService.CONTRACT);
    return of(obj);
  }

  save(model: IDocumentInfoModel): Observable<IApiDataResult<IDocumentInfoModel>> { return of(null); }

  delete(id: number): Observable<ApiResult> { return of(null) }

  deleteByFilter(request: IGridParams): Observable<ApiResult> { return of(null) }

  deleteByIds(ids: string[]): Observable<ApiResult> { return of(null) }
}
