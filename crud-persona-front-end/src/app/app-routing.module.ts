import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PersonsComponent } from './components/persons/persons.component';

const routes: Routes = [
  {
    path: '', redirectTo: '', pathMatch: 'full',
  },
  {
    path: '',
    component: PersonsComponent
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
