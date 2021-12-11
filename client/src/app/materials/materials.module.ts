import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialsComponent } from './materials.component';
import { MaterialItemComponent } from './material-item/material-item.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { SideBarComponent } from './side-bar/side-bar.component';
import { MaterialViewComponent } from './material-view/material-view.component';
import {MaterialsRoutingModule} from "./materials-routing.module";
import {NgbDropdownModule} from "@ng-bootstrap/ng-bootstrap";
import {YouTubePlayerModule} from "@angular/youtube-player";
import { NoteItemComponent } from './note-item/note-item.component';



@NgModule({
  declarations: [
    MaterialsComponent,
    MaterialItemComponent,
    SideBarComponent,
    MaterialViewComponent,
    NoteItemComponent
  ],
    imports: [
        CommonModule, SharedModule, CoreModule, MaterialsRoutingModule, NgbDropdownModule, YouTubePlayerModule
    ]
})
export class MaterialsModule { }
