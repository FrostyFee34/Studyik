import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialsComponent } from './materials.component';
import { MaterialItemComponent } from './material-item/material-item.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { SideBarComponent } from './side-bar/side-bar.component';
import { MaterialCreateComponent } from './material-create/material-create.component';
import { MaterialViewComponent } from './material-view/material-view.component';
import {MaterialsRoutingModule} from "./materials-routing.module";



@NgModule({
  declarations: [
    MaterialsComponent,
    MaterialItemComponent,
    SideBarComponent,
    MaterialCreateComponent,
    MaterialViewComponent
  ],
  imports: [
    CommonModule, SharedModule, CoreModule, MaterialsRoutingModule
  ]
})
export class MaterialsModule { }
