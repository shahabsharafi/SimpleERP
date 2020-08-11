import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TextFilter, SimpleFilter, AllModules, Module  } from '@ag-grid-enterprise/all-modules';
import { AgGridUtility, ApiResult, IGridParams, ConvertorService, GridService, ConvertDate, SystemMessage, IApiDataResult } from '../../../../infrastructures';
import { Observable } from 'rxjs';
import { saveAs } from 'file-saver';
import * as moment from 'jalali-moment';
import { IDocumentInfoModel, DocumentInfoModel, ISelectItemModel } from '../../models';
import { DocumentInfoDatasource, DocumentInfoService } from '../../services/documentinfo';

@Component({
  templateUrl: './documentinfo.component.html',
  styleUrls: ['./documentinfo.component.css']
})
export class DocumentInfoComponent implements OnInit {
  private gridApi;
  private gridColumnApi;

  private form: FormGroup;

  public modules: Module[] = AllModules;
  private rowSelection: string;
  public suppressRowClickSelection: boolean;
  private defaultColDef: any;
  private columnDefs: any[];
  public gridOptions: any = {};
  public issuerList: ISelectItemModel[];
  public domainList: ISelectItemModel[];
  public typeList: ISelectItemModel[];
  private pageMode: string;
  private selectedObject: IDocumentInfoModel;
  public selectedImageUrl: string;

  constructor(
    private fb: FormBuilder, @Inject('RESOURCE') public resource: any,
    private documentInfoService: DocumentInfoService,
    private documentInfoDatasource: DocumentInfoDatasource,
    private convertorService: ConvertorService,
    private systemMessage: SystemMessage) {
    documentInfoDatasource.init(documentInfoService);
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [null, []],
      no: ['', [Validators.required]],
      subject: ['', [Validators.required]],
      dateOfRelease: [],
      dateOfCreate: [],
      creator: [],
      issuerId: [],
      domainId: [],
      typeId: []
    });
    this.rowSelection = 'single';
    this.suppressRowClickSelection = false;
    this.defaultColDef = {
      editable: o => !o.data.readonly
    };
    this.columnDefs = [
      { field: 'id', headerName: this.resource.default.document_id, sortable: true, filter: 'agNumberColumnFilter' },
      { field: 'no', headerName: this.resource.default.document_no, sortable: true, filter: 'agTextColumnFilter' },
      { field: 'subject', headerName: this.resource.default.document_subject, sortable: true, filter: 'agTextColumnFilter' },
      { field: 'issuerTitle', headerName: this.resource.default.document_issuerTitle, sortable: true, filter: 'agTextColumnFilter' },
      { field: 'domainTitle', headerName: this.resource.default.document_domainTitle, sortable: true, filter: 'agTextColumnFilter' },
      { field: 'typeTitle', headerName: this.resource.default.document_typeTitle, sortable: true, filter: 'agTextColumnFilter' }
    ];
    this.gridOptions.rowModelType = 'serverSide';
    this.gridOptions.paginationPageSize = 10;
    
    this.gridOptions.getRowClass = function (params) {
      return '';
    }
    this.pageMode = 'list';
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    params.api.setServerSideDatasource(this.documentInfoDatasource);
    params.api.sizeColumnsToFit();

    this.documentInfoService.getIssuers().toPromise()
      .then(response => { this.issuerList = response.data.rows });
    this.documentInfoService.getDomains().toPromise()
      .then(response => { this.domainList = response.data.rows });
    this.documentInfoService.getTypes().toPromise()
      .then(response => { this.typeList = response.data.rows });    
  }
  private i: number = 0;
  public exportToExcel(): void {    
    const params: IGridParams = this.documentInfoDatasource.getParams();
    this.documentInfoService.getExcel(params).toPromise().then(o => {
      saveAs(o, 'contect-list');
      this.systemMessage.success('عملیات انجام شد.');
    });   
  }  

  public add(): void {
    this.form.setValue({
      "id": null,
      "no": null,
      "subject": null,
      "dateOfRelease": null,
      "dateOfCreate": null,
      "creator": null,
      "issuerId": null,
      "domainId": null,
      "typeId": null
    });
    this.pageMode = 'form';
  }

  onSelectionChanged(event) {
    const nodes = this.gridApi.getSelectedNodes();
    if (nodes && nodes.length) {
      if (nodes[0].data) {
        const data = <IDocumentInfoModel>nodes[0].data;
        if (data.id) {
          const id = data.id;
          this.documentInfoService.get(id).toPromise().then(response => {
            this.selectedObject = response.data;
          });
        }
      }
    }
  }

  public edit(): void {    
    if (this.selectedObject != null && this.selectedObject.id != null) {
      const id = this.selectedObject.id;
      this.documentInfoService.get(id).toPromise().then(response => {
        const obj = {
          "id": response.data.id,
          "no": response.data.no,
          "subject": response.data.subject,
          "dateOfRelease": ConvertDate.toJalali(response.data.dateOfRelease),
          "dateOfCreate": ConvertDate.toJalali(response.data.dateOfCreate),
          "creator": response.data.creator,
          "issuerId": response.data.issuerId,
          "domainId": response.data.domainId,
          "typeId": response.data.typeId
        };
        this.form.setValue(obj);
        this.pageMode = 'form';
      });
    }
  }

  public delete(): void {
    if (this.selectedObject != null && this.selectedObject.id != null) {
      const id = this.selectedObject.id;
      const me = this;
      this.systemMessage.comfirm('آیا مطمعن هستید؟', () => {
        const result = me.documentInfoService.delete(id);
        me.refreshGrid(result);
        me.systemMessage.success('حذف انجام شد.');
      });
    }
  }

  public handleFileInput(files: FileList): void {
    if (this.selectedObject == null)
      throw "upload file is not enabled in creation new document";
    const fileToUpload: File = files.item(0);
    const result = this.documentInfoService.uploadFile(this.selectedObject.id, fileToUpload);
    this.refreshRow(result);    
  }

  public getImageUrl(id: number): string {
    return this.documentInfoService.getImageUrl(id);
  }

  public showImage(id: number): void {
    this.selectedImageUrl = this.documentInfoService.getImageUrl(id);
  }

  public deleteImage(id: number): void {
    const result = this.documentInfoService.deleteImage(id);
    this.refreshRow(result); 
  }

  public save(): void {
    const model: IDocumentInfoModel = (<any>Object).assign({}, this.form.value);
    model.dateOfCreate = ConvertDate.toGeregorian(model.dateOfCreate);
    model.dateOfRelease = ConvertDate.toGeregorian(model.dateOfRelease);  
    if (model.id) {
      const result = this.documentInfoService.update(model);
      this.refreshRow(result);
    }
    else {
      const result = this.documentInfoService.insert(model);      
      this.refreshGrid(result);
    }
    this.pageMode = 'list';
  }

  public cancel(): void {
    this.pageMode = 'list';
  }

  private refreshRow(result: Observable<IApiDataResult<IDocumentInfoModel>>): Promise<void | ApiResult> {
    return result.toPromise().then(response => {
      if (response) {
        if (response.isSuccess) {
          this.selectedObject = response.data;
          this.gridApi.forEachNode(function (rowNode) {
            if (response.data.id === rowNode.data.id) {
              rowNode.setData(response.data);
            }
          });
        } else {
          alert(response.message);
        }
      }
      return response;
    });
  }  

  private refreshGrid(result: Observable<ApiResult>): Promise<void | ApiResult> {
    return result.toPromise().then(response => {
      if (response) {
        if (response.isSuccess) {
          this.selectedObject = null;
          this.gridApi.purgeServerSideCache(null);
        } else {
          alert(response.message);
        }
      }
      return response;
    });
  }  
}
