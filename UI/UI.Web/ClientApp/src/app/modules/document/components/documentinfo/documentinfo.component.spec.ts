import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { RouterTestingModule } from '@angular/router/testing';
import { AgGridModule } from '@ag-grid-community/angular';
import { DocumentInfoComponent } from './documentinfo.component';
import { IGridParams, IGridExcelParams } from '../../../../infrastructures';
import { IServerSideGetRowsParams } from '@ag-grid-enterprise/all-modules';
import { DocumentInfoDatasource, DocumentInfoService, DocumentInfoFackService } from '../../services/documentinfo';

class DocumentInfoFackDatasource extends DocumentInfoDatasource {

  constructor() { super(); }

  getRows(params: IServerSideGetRowsParams): void { }

  getParams(): IGridParams { return null; }

  getExcelParams(): IGridExcelParams { return null;}

  destroy?(): void { }
}

describe('DocumentInfoComponent', () => {

  let component: any;
  let fixture: ComponentFixture<DocumentInfoComponent>;  

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        FormsModule,
        AgGridModule,
        RouterTestingModule
      ],
      declarations: [        
        DocumentInfoComponent,        
      ],
      providers: [
        { provide: 'RESOURCE', useValue: { default: { "contract_title": "قرارداد" } }, deps: [] },
        { provide: DocumentInfoService, useClass: DocumentInfoFackService },
        { provide: DocumentInfoDatasource, useClass: DocumentInfoFackDatasource }
      ],
    }).compileComponents();

    // create component and test fixture
    fixture = TestBed.createComponent(DocumentInfoComponent);

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
