import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TextFilter, SimpleFilter, AllModules, Module  } from '@ag-grid-enterprise/all-modules';
import { AgGridUtility, ApiResult, IGridParams, ConvertorService, GridService, ConvertDate, AlertModel } from '../../../../infrastructures';
import { Observable } from 'rxjs';
import { saveAs } from 'file-saver';
import * as moment from 'jalali-moment';
import { IDocumentInfoModel, DocumentInfoModel, ISelectItemModel } from '../../models';
import { DocumentInfoDatasource, DocumentInfoService } from '../../services/documentinfo';
import { AlertService } from '../../../../infrastructures/services/alert.service';

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
  private fileToUpload: File = null;
  private pageMode: string;

  constructor(private fb: FormBuilder, @Inject('RESOURCE') public resource: any, private documentInfoService: DocumentInfoService, private documentInfoDatasource: DocumentInfoDatasource, private convertorService: ConvertorService, private alertService: AlertService) {
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

  public exportToExcel(): void {
    this.alertService.show(new AlertModel('success', 'export to excel...'));
    //const params: IGridParams = this.documentInfoDatasource.getParams();
    //this.documentInfoService.getExcel(params).toPromise().then(o => {
    //  saveAs(o, 'contect-list');
    //});
  }

  public delete(): void {
    const id = this.getId();
    if (id != null) {
      const result = this.documentInfoService.delete(id);
      this.refreshGrid(result);
      this.alertService.show(new AlertModel('success', 'deleting is successed'));
    }
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

  private getId(): number | null {
    const selectedNodes: ({ data: IDocumentInfoModel } | null)[] = this.gridApi.getSelectedNodes();
    const nodes = (selectedNodes || [{ data: null }]);
    if (nodes && nodes.length) {
      const data: (IDocumentInfoModel | null) = (nodes[0]).data;
      if (data && data.id) {
        return data.id;
      }
    }
    return null;
  }

  public edit(): void {
    const id = this.getId();
    if (id != null) {
      this.documentInfoService.get(id).toPromise().then(response => {
        this.form.setValue({
          "id": response.data.id,
          "no": response.data.no,
          "subject": response.data.subject,
          "dateOfRelease": ConvertDate.toJalali(response.data.dateOfRelease),
          "dateOfCreate": ConvertDate.toJalali(response.data.dateOfCreate),
          "creator": response.data.creator,
          "issuerId": response.data.issuerId,
          "domainId": response.data.domainId,
          "typeId": response.data.typeId
        });
        this.pageMode = 'form';
      });
    }
  }

  public handleFileInput(files: FileList): void {
    this.fileToUpload = files.item(0);
  }

  public save(): void {
    const model: IDocumentInfoModel = (<any>Object).assign({}, this.form.value);
    model.dateOfCreate = ConvertDate.toGeregorian(model.dateOfCreate);
    model.dateOfRelease = ConvertDate.toGeregorian(model.dateOfRelease);    
    const result = this.documentInfoService.save(model, this.fileToUpload);
    this.refreshGrid(result);
    this.pageMode = 'list';
  }

  public cancel(): void {
    this.pageMode = 'list';
  }

  private refreshGrid(result: Observable<ApiResult>): Promise<void | ApiResult> {
    return result.toPromise().then(response => {
      if (response) {
        if (response.isSuccess) {
          this.gridApi.purgeServerSideCache(null);
        } else {
          alert(response.message);
        }
      }
      return response;
    });
  }  
}
