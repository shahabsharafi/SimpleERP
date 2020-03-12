import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { IRoleModel } from '../../services/acount/models';
import { AccountService } from '../../../identity/services/acount/account.service';



@Component({
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {
  private gridApi;
  private gridColumnApi;

  private form: FormGroup;

  private rowSelection;
  private rowData: IRoleModel[];
  private columnDefs: any[];

  constructor(private fb: FormBuilder, @Inject('RESOURCE') public resource: any, private accountService: AccountService) { }

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', [Validators.required]]
    });
    this.rowSelection = 'single';
    this.columnDefs = [
      { field: 'name', headerName: this.resource.default.role_title, sortable: true, filter: true }
    ];
  }

  onSelectionChanged(event) {
    console.log("selection changed");
    const selectedNodes: ({ data: IRoleModel } | null)[] = event.api.getSelectedNodes();
    const data: (IRoleModel | null) = ((selectedNodes || [{ data: null }])[0]).data;
    if (data) {
      this.form.setValue({ "name": data.name });
    }
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;

    params.api.sizeColumnsToFit();

    this.accountService.getRoles().toPromise().then(response => this.rowData = response.data);
  }

  Save() {

  }
}
