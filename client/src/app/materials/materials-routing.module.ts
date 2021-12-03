import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {MaterialsComponent} from "./materials.component";
import {MaterialViewComponent} from "./material-view/material-view.component";
import {MaterialCreateComponent} from "./material-create/material-create.component";


const routes: Routes = [
  {path: '', component: MaterialsComponent},
  {path: 'material-view', component: MaterialViewComponent},
  {path: 'material-create', component: MaterialCreateComponent},
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [
    RouterModule
  ]
})
export class MaterialsRoutingModule {
}
