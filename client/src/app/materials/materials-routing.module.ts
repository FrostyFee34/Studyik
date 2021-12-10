import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {MaterialsComponent} from "./materials.component";
import {MaterialViewComponent} from "./material-view/material-view.component";


const routes: Routes = [
  {path: '', component: MaterialsComponent},
  {path: 'material-view/:id', component: MaterialViewComponent},
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
