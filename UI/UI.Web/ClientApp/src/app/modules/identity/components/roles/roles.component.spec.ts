import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { RouterTestingModule } from '@angular/router/testing';
import { RolesComponent } from './';
import { AgGridModule } from '@ag-grid-community/angular';
import { AccountService, AccountFackService } from '../../services/acount/account.service';

describe('RolesComponent', () => {

  let component: any;
  let fixture: ComponentFixture<RolesComponent>;  

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        FormsModule,
        AgGridModule,
        RouterTestingModule
      ],
      declarations: [        
        RolesComponent,        
      ],
      providers: [
        { provide: 'RESOURCE', useValue: { default: { "role_title": "نقش" } }, deps: [] },
        { provide: AccountService, useClass: AccountFackService }
      ],
    }).compileComponents();

    // create component and test fixture
    fixture = TestBed.createComponent(RolesComponent);

    // get test component from the fixture
    component = fixture.componentInstance;
    component.ngOnInit();
  }));

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it(`check ngOnInit`, () => {
    expect(component).toBeTruthy();
    component.ngOnInit();
    fixture.whenStable()
      .then(() => {
        expect(component.columnDefs.length).toEqual(1);
      });
  });

  it(`check GridReady`, () => {
    expect(component).toBeTruthy();
    const getRoles_Spy = spyOn(component.accountService, 'getRoles').and.callThrough();
    const params = { api: { getSelectedRows: function () { }, sizeColumnsToFit: function () { } } };
    component.onGridReady(params);
    expect(getRoles_Spy).toHaveBeenCalled();
    fixture.whenStable()
      .then(() => {
        expect(component.rowData.length).toEqual(1);
      });
  });
});
