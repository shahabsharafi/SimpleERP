import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DocumentInfoComponent } from './modules';

const routes: Routes = [
  { path: 'document', component: DocumentInfoComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
