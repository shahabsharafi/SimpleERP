import { Injectable, Inject } from '@angular/core';
import { IDocumentInfoModel, DocumentInfoModel, ISelectItemModel, SelectItemModel, IFileModel } from '../../models';
import { IApiDataResult, ApiResult, ApiDataResult, ApiCollectionResult, ICollection, Collection, QueryString, TokenService, IApiResult } from '../../../../infrastructures';
import { Observable, of } from 'rxjs';
import { IGridParams, IGridExcelParams, GridExcelParams, GridService } from '../../../../infrastructures/services/models/grid-parameter';

import { HttpClient } from '@angular/common/http';

@Injectable()
export class DocumentInfoService extends GridService {

  private _url: string;

  constructor(private rest: HttpClient, @Inject('CONTRACT_MANAGEMENT_SERVICE_URL') private baseUrl: string) {
    super();
    this._url = this.baseUrl + 'api/documentinfos';   
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

  getIssuers(): Observable<ApiCollectionResult<ISelectItemModel>> {
    return this.rest.get<ApiCollectionResult<ISelectItemModel>>(this.baseUrl + 'api/documentinfos/issuers');
  }

  getDomains(): Observable<ApiCollectionResult<ISelectItemModel>> {
    return this.rest.get<ApiCollectionResult<ISelectItemModel>>(this.baseUrl + 'api/documentinfos/domains');
  }

  getTypes(): Observable<ApiCollectionResult<ISelectItemModel>> {
    return this.rest.get<ApiCollectionResult<ISelectItemModel>>(this.baseUrl + 'api/documentinfos/types');
  }

  update(model: IDocumentInfoModel): Observable<IApiDataResult<IDocumentInfoModel>> {
    return this.rest.put<IApiDataResult<IDocumentInfoModel>>(this._url + '/' + model.id, model);
  }

  insert(model: IDocumentInfoModel): Observable<IApiDataResult<IDocumentInfoModel>> {
    model.no = Math.floor(Math.random() * 1000) + '';
    return this.rest.post<IApiDataResult<IDocumentInfoModel>>(this._url, model);
  }

  delete(id: number): Observable<ApiResult> {
    return this.rest.delete<ApiResult>(this._url + '/' + id);
  }

  uploadFile(id: number, file: File): Observable<IApiDataResult<IDocumentInfoModel>> {
    let result: Observable<IApiDataResult<IDocumentInfoModel>>;
    let formData: FormData = new FormData();
    formData.append('file', file);
    result = this.rest.put<IApiDataResult<IDocumentInfoModel>>(this._url + '/upload/' + id, formData);
    return result;
  }

  getImageUrl(id: number): string {
    return this._url + '/download/' + id;
  }

  deleteImage(id: number): Observable<IApiDataResult<IDocumentInfoModel>> {
    return this.rest.delete<IApiDataResult<IDocumentInfoModel>>(this._url + '/deleteimage/' + id);
  }
}

export class DocumentInfoFackService extends DocumentInfoService {

  static DOCUMENT_INFO: IDocumentInfoModel = new DocumentInfoModel(0, "", "", "", "", "", "", "", "", 0, "", 0, "", 0, "", []);
  static DOCUMENT_INFO_ARRAY: IDocumentInfoModel[] = [DocumentInfoFackService.DOCUMENT_INFO];
  static DOCUMENT_INFOS: ICollection<IDocumentInfoModel> = new Collection<IDocumentInfoModel>(DocumentInfoFackService.DOCUMENT_INFO_ARRAY, 1);

  static SELECT_ITEM: ISelectItemModel = new SelectItemModel(0, "");
  static SELECT_ITEM_ARRAY: ISelectItemModel[] = [DocumentInfoFackService.SELECT_ITEM];
  static SELECT_ITEMS: ICollection<ISelectItemModel> = new Collection<ISelectItemModel>(DocumentInfoFackService.SELECT_ITEM_ARRAY, 1);

  constructor() { super(null, null); }

  getAll(request: IGridParams): Observable<ApiCollectionResult<IDocumentInfoModel>> {
    var obj = new ApiCollectionResult<IDocumentInfoModel>(true, 200, "", DocumentInfoFackService.DOCUMENT_INFOS);
    return of(obj);
  }

  get(id: number): Observable<IApiDataResult<IDocumentInfoModel>> {
    var obj = new ApiDataResult<IDocumentInfoModel>(true, 200, "", DocumentInfoFackService.DOCUMENT_INFO);
    return of(obj);
  }
  getIssuers(): Observable<ApiCollectionResult<ISelectItemModel>> {
    var obj = new ApiCollectionResult<ISelectItemModel>(true, 200, "", DocumentInfoFackService.SELECT_ITEMS);
    return of(obj);
  }

  getDomains(): Observable<ApiCollectionResult<ISelectItemModel>> {
    var obj = new ApiCollectionResult<ISelectItemModel>(true, 200, "", DocumentInfoFackService.SELECT_ITEMS);
    return of(obj);
  }

  getTypes(): Observable<ApiCollectionResult<ISelectItemModel>> {
    var obj = new ApiCollectionResult<ISelectItemModel>(true, 200, "", DocumentInfoFackService.SELECT_ITEMS);
    return of(obj);
  }

  save(model: IDocumentInfoModel): Observable<IApiDataResult<IDocumentInfoModel>> { return of(null); }

  delete(id: number): Observable<ApiResult> { return of(null) }
}
