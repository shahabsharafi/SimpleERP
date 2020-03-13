import { Injectable, Inject } from '@angular/core';
import { IDatasource, IGetRowsParams, IServerSideDatasource, IServerSideGetRowsParams } from '@ag-grid-enterprise/all-modules';
import { GridParams, IGridParams, GridService } from '../../../../infrastructures/services/models/grid-parameter';

//https://www.ag-grid.com/javascript-grid-server-side-model-infinite/

@Injectable()
export class DocumentInfoDatasource implements IServerSideDatasource  { 

  private _gridService: GridService;
  private _currentFilter?: IGridParams;

  constructor() {
    this._currentFilter = null;
  }

  init(gridService: GridService) {
    this._gridService = gridService;
  }
  
  getRows(params: IServerSideGetRowsParams): void {
    const gridParams: IGridParams = new GridParams(params.request.startRow, params.request.endRow, params.request.sortModel, params.request.filterModel);
    this._gridService.getAll(gridParams).toPromise().then(response => {
      if (response) {
        if (response.isSuccess) {
          this._currentFilter = gridParams;
          var lastRow = response.data.rowCount <= params.request.endRow ? response.data.rowCount : -1;
          params.successCallback(response.data.rows, lastRow);
        } else {
          params.failCallback();
        }
      }
    });
  }

  getParams(): IGridParams {
    return this._currentFilter;
  }

  destroy?(): void {
    this._currentFilter = null;
  }
}
