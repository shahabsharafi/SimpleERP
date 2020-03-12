import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent, RegisterComponent, RolesComponent, ContractComponent, FormDesignerComponent, ModelerComponent } from './modules';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'roles', component: RolesComponent },
  { path: 'contract', component: ContractComponent },
  { path: 'form-designer', component: FormDesignerComponent },
  { path: 'modeler', component: ModelerComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
