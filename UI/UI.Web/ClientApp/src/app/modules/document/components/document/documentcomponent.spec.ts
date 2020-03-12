import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { RouterTestingModule } from '@angular/router/testing';
import { AgGridModule } from '@ag-grid-community/angular';
import { ContractComponent } from './contract.component';
import { ContractService, ContractFackService, ContractDatasource } from '../../services/contract';
import { IGridParams, IGridExcelParams } from '../../../../infrastructures';
import { IGetRowsParams } from '@ag-grid-enterprise/all-modules';

class ContractFackDatasource extends ContractDatasource {

  constructor() { super(); }

  getRows(params: IGetRowsParams): void { }

  getParams(): IGridParams { return null; }

  getExcelParams(): IGridExcelParams { return null;}

  destroy?(): void { }
}

describe('ContractComponent', () => {

  let component: any;
  let fixture: ComponentFixture<ContractComponent>;  

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        FormsModule,
        AgGridModule,
        RouterTestingModule
      ],
      declarations: [        
        ContractComponent,        
      ],
      providers: [
        { provide: 'RESOURCE', useValue: { default: { "contract_title": "قرارداد" } }, deps: [] },
        { provide: ContractService, useClass: ContractFackService },
        { provide: ContractDatasource, useClass: ContractFackDatasource }
      ],
    }).compileComponents();

    // create component and test fixture
    fixture = TestBed.createComponent(ContractComponent);

    // get test component from the fixture
    component = fixture.componentInstance;
    component.selectedRowData = {
      id: null
    };
    component.ngOnInit();
  }));

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it(`check save`, () => {
    expect(component).toBeTruthy();
    const getSave_Spy = spyOn(component.contractService, 'save').and.callThrough();
    component.save();
    expect(getSave_Spy).toHaveBeenCalled();
  });
});
