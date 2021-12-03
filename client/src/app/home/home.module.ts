import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { MaterialItemComponent } from './material-item/material-item.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { SideBarComponent } from './side-bar/side-bar.component';



@NgModule({
  declarations: [
    HomeComponent,
    MaterialItemComponent,
    SideBarComponent
  ],
  imports: [
    CommonModule, SharedModule, CoreModule
  ]
})
export class HomeModule { }
