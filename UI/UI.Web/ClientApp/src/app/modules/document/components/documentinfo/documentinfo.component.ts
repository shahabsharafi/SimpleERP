import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TextFilter, SimpleFilter, AllModules, Module  } from '@ag-grid-enterprise/all-modules';
import { AgGridUtility, ApiResult, IGridParams, ConvertorService, GridService } from '../../../../infrastructures';
import { Observable } from 'rxjs';
import { saveAs } from 'file-saver';
import * as moment from 'jalali-moment';
import { IDocumentInfoModel, DocumentInfoModel } from '../../models';
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
  private selectedRowData: IDocumentInfoModel;
  public gridOptions: any = {};

  constructor(private fb: FormBuilder, @Inject('RESOURCE') public resource: any, private documentInfoService: DocumentInfoService, private documentInfoDatasource: DocumentInfoDatasource, private convertorService: ConvertorService) {
    documentInfoDatasource.init(documentInfoService);
  }

  ngOnInit() {
    this.form = this.fb.group({
      subject: ['', [Validators.required]],
      no: ['', [Validators.required]]
    });
    this.rowSelection = 'single';
    this.suppressRowClickSelection = false;
    this.defaultColDef = {
      editable: o => !o.data.readonly
    };
    this.columnDefs = [
      { colId: 'selector', hide: true, width: 40, headerName: '', sortable: false, filter: false, checkboxSelection: true },
      { field: 'id', headerName: this.resource.default.document_id, sortable: true, filter: 'agNumberColumnFilter' },
      { field: 'subject', headerName: this.resource.default.document_subject, sortable: true, filter: 'agTextColumnFilter' },
      { field: 'no', headerName: this.resource.default.no, sortable: true, filter: 'agTextColumnFilter' }
    ];
    this.gridOptions.rowModelType = 'serverSide';
    this.gridOptions.paginationPageSize = 10;
    
    this.gridOptions.getRowClass = function (params) {
      return '';
    }
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    params.api.setServerSideDatasource(this.documentInfoDatasource);
    params.api.sizeColumnsToFit();
  }

  onSelectionChanged(event) {
    const selectedNodes: ({ data: IDocumentInfoModel } | null)[] = event.api.getSelectedNodes();
    const nodes = (selectedNodes || [{ data: null }]);
    if (nodes && nodes.length) {
      const data: (IDocumentInfoModel | null) = (nodes[0]).data;
      if (data && data.id) {
        this.documentInfoService.get(data.id).toPromise().then(response => {
          this.selectedRowData = response.data;
          this.form.setValue({ "no": response.data.no });
        });
      }
    }
  }

  select() {
    const column = this.gridColumnApi.getColumn('selector');
    const visible = !column.visible;
    this.doSelect(visible);
  }

  doSelect(visible) {
    this.rowSelection = visible ? 'multiple' : 'single';
    this.suppressRowClickSelection = visible;
    this.gridColumnApi.setColumnVisible('selector', visible);
    this.gridApi.deselectAll();
  }

  private refreshGrid(result: Observable<ApiResult>): Promise<void | ApiResult> {
    return result.toPromise().then(response => {
      if (response) {
        if (response.isSuccess) {
          this.gridApi.refreshInfiniteCache(null);
        } else {
          alert(response.message);
        }
      }
      return response;
    });
  }

  saveForm() {
    const model: IDocumentInfoModel = (<any>Object).assign({}, this.form.value);
    model.id = this.selectedRowData.id || null;
    const result = this.documentInfoService.save(model);
    this.refreshGrid(result);
  }

  delete() {
    const id: number = this.selectedRowData.id;
    const result = this.documentInfoService.delete(id);
    this.refreshGrid(result);
  }

  deleteByFilter() {
    const params: IGridParams = this.documentInfoDatasource.getParams();
    const result = this.documentInfoService.deleteByFilter(params);
    this.refreshGrid(result);
  }

  deleteByIds() {
    let nodes: any[] = this.gridApi.getSelectedNodes();
    if (nodes != null && nodes.length) {
      const list: IDocumentInfoModel[] = nodes.map(o => <IDocumentInfoModel>o.data);
      const ids: string[] = list.map(o => o.id.toString());
      const result = this.documentInfoService.deleteByIds(ids);
      this.refreshGrid(result).then(_ => {
        this.gridApi.deselectAll();
      });      
    }
  }

  exportToExcel() {
    const params: IGridParams = this.documentInfoDatasource.getParams();
    this.documentInfoService.getExcel(params).toPromise().then(o => {
      saveAs(o, 'contect-list');
    });
  }
}
