import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ContractService } from '../../services/contract';
import { ContractDatasource } from '../../services/contract/contract.datasource';
import { TextFilter, SimpleFilter, AllModules, Module  } from '@ag-grid-enterprise/all-modules';
import { AgGridUtility, ApiResult, IGridParams, ConvertorService, GridService } from '../../../../infrastructures';
import { Observable } from 'rxjs';
import { saveAs } from 'file-saver';
import * as moment from 'jalali-moment';
import { IContractModel, ContractModel } from '../../models';

@Component({
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.css']
})
export class ContractComponent implements OnInit {
  private gridApi;
  private gridColumnApi;

  private form: FormGroup;

  public modules: Module[] = AllModules;
  private rowSelection: string;
  public suppressRowClickSelection: boolean;
  private rowData: IContractModel[];
  private defaultColDef: any;
  private columnDefs: any[];
  private updatedData: IContractModel;
  private selectedRowData: IContractModel;
  private selectedColumn: string;
  private selectedRowIndex: number;
  private editingRowIndex: number;
  private checkBoxColumn: any;
  public gridOptions: any = {};

  constructor(private fb: FormBuilder, @Inject('RESOURCE') public resource: any, private contractService: ContractService, private contractDatasource: ContractDatasource, private convertorService: ConvertorService) {
    contractDatasource.init(contractService);
  }

  ngOnInit() {
    this.form = this.fb.group({
      title: ['', [Validators.required]]
    });
    this.rowSelection = 'single';
    this.suppressRowClickSelection = false;
    this.defaultColDef = {
      editable: o => !o.data.readonly
    };
    this.columnDefs = [
      { colId: 'selector', hide: true, width: 40, headerName: '', sortable: false, filter: false, checkboxSelection: true },
      { field: 'title', headerName: this.resource.default.contract_title, sortable: true, filter: 'agTextColumnFilter' },
      { field: 'id', headerName: 'ID', sortable: true, filter: 'agNumberColumnFilter' },
      { field: 'readonly', headerName: 'Is readonly', sortable: true, filter: true, filterParams: AgGridUtility.getBooleanFilterParams(), cellRenderer: o => this.convertorService.toBoolean(o.getValue()) },      
      { field: 'startDate', headerName: 'Date of start', sortable: true, filter: 'agDateColumnFilter', cellRenderer: o => this.convertorService.toJalali(o.getValue()) }
    ];
    this.gridOptions.rowModelType = 'serverSide';
    this.gridOptions.paginationPageSize = 10;
    //this.gridOptions.datasource = this.contractDatasource;
    
    this.gridOptions.getRowClass = function (params) {
      return '';// (params.data.readonly ? 'readonly-row' : '');
    }
    this.updatedData = null;
    this.selectedRowIndex = null;
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    params.api.setServerSideDatasource(this.contractDatasource);
    params.api.sizeColumnsToFit();
  }

  onSelectionChanged(event) {
    const selectedNodes: ({ data: IContractModel } | null)[] = event.api.getSelectedNodes();
    const nodes = (selectedNodes || [{ data: null }]);
    if (nodes && nodes.length) {
      const data: (IContractModel | null) = (nodes[0]).data;
      if (data && data.id) {
        this.contractService.get(data.id).toPromise().then(response => {
          this.selectedRowData = response.data;
          this.form.setValue({ "title": response.data.title });
        });
      }
    }
  }

  onCellClick(event: any): void {
    this.selectedColumn = event.column.colId; 
  }

  onRowClick(event: any): void {
    this.selectedRowIndex = event.rowIndex;
    if (this.editingRowIndex != null && this.selectedRowIndex != null && this.editingRowIndex != this.selectedRowIndex)
      this.saveRow();
    this.editingRowIndex = null;
  }

  onCellEditingStarted(event: any) {
    this.editingRowIndex = event.rowIndex;
  }

  onCellValueChanged(event) {
    if (event.data) {
      this.updatedData = <IContractModel>event.data;      
    }
  }

  onCellEditingStopped(event) {
  }

  saveRow() {
    if (this.updatedData != null) {
      const d = this.updatedData;
      const model: IContractModel = new ContractModel(d.uniqueId, d.id, d.no, d.title, d.archived, d.startDate);
      this.contractService.save(model).toPromise().then(rsp => {
        this.updatedData = null;

        this.gridApi.deselectAll();
        this.gridApi.refreshInfiniteCache();
        this.gridApi.setSortModel(null);
        this.gridApi.setFilterModel(null);

        this.contractService.get(rsp.data.id).toPromise().then(response => {
          this.selectedRowData = response.data;
          this.form.setValue({ "title": response.data.title });
        });
      });
    }
  }

  addItem() {
    if (this.updatedData == null) {
      this.doSelect(false);     
      let d = new Date();
      this.updatedData = new ContractModel(null, null, '', '', false, d.toJSON());
      this.gridApi.updateRowData({
        add: [this.updatedData],
        addIndex: 0
      });
      
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

  edit() {
    if (this.selectedRowIndex != null && this.selectedColumn != null)
    this.gridApi.startEditingCell({
      rowIndex: this.selectedRowIndex,
      colKey: this.selectedColumn
    });
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
    const model: IContractModel = (<any>Object).assign({}, this.form.value);
    model.id = this.selectedRowData.id || null;
    const result = this.contractService.save(model);
    this.refreshGrid(result);
  }

  delete() {
    const id: number = this.selectedRowData.id;
    const result = this.contractService.delete(id);
    this.refreshGrid(result);
  }

  deleteByFilter() {
    const params: IGridParams = this.contractDatasource.getParams();
    const result = this.contractService.deleteByFilter(params);
    this.refreshGrid(result);
  }

  deleteByIds() {
    let nodes: any[] = this.gridApi.getSelectedNodes();
    if (nodes != null && nodes.length) {
      const list: IContractModel[] = nodes.map(o => <IContractModel>o.data);
      const ids: string[] = list.map(o => o.id.toString());
      const result = this.contractService.deleteByIds(ids);
      this.refreshGrid(result).then(_ => {
        this.gridApi.deselectAll();
      });      
    }
  }

  exportToExcel() {
    const params: IGridParams = this.contractDatasource.getParams();
    this.contractService.getExcel(params).toPromise().then(o => {
      saveAs(o, 'contect-list');
    });
  }
}
