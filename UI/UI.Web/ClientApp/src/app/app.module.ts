import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AgGridModule } from '@ag-grid-community/angular';

import { AppRoutingModule } from './app-routing.module';
import { TokenService, LayoutComponent, JalaliPipe, ConvertorService } from './infrastructures';
import * as resource from './infrastructures/resource.json'; 
import { AuthInterceptor } from './infrastructures/utilities/auth-interceptor';
import { MessageService } from './infrastructures/services/message.service';
import { DynamicFormBuilderModule } from './infrastructures/lib';
import { DocumentInfoComponent, DocumentInfoService, DocumentInfoDatasource } from './modules';


@NgModule({
  declarations: [
    LayoutComponent,
    DocumentInfoComponent,
    JalaliPipe
  ],
  imports: [
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    AgGridModule.withComponents([]),
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    DynamicFormBuilderModule
  ],
  providers: [    
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    ConvertorService,
    MessageService,
    TokenService,
    { provide: 'CONTRACT_MANAGEMENT_SERVICE_URL', useFactory: function () { return 'https://localhost:8000/contractmanagement/' }, deps: [] },
    DocumentInfoService,
    DocumentInfoDatasource,
    { provide: 'RESOURCE', useValue: resource, deps: [] }
  ],
  bootstrap: [LayoutComponent]
})
export class AppModule { }
