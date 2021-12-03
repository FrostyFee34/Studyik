import {Component, OnInit} from '@angular/core';
import {MaterialParams} from 'src/app/shared/models/materialParams';
import {MaterialsService} from '../materials.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
})
export class SideBarComponent implements OnInit {
  params!: MaterialParams;
  checkedNavLink = 'all';

  constructor(private homeService: MaterialsService) {
  }

  ngOnInit(): void {
    this.params = this.homeService.getMaterialParams();
  }

  onNavLink(value: string) {
    if (value) {
      this.checkedNavLink = value;
      this.params = new MaterialParams();
      this.params.search = this.homeService.getMaterialParams().search;
      switch (value) {
        case 'recent': {
          this.params.sort = 'dateDesc';
          break;
        }
        case 'videos': {
          this.params.categoryId = 2;
          break;
        }
        case 'files': {
          this.params.categoryId = 1;
          break;
        }
        case 'text': {
          this.params.categoryId = 3;
          break;
        }
        default: {
        }
      }
    }

    this.homeService.setMaterialParams(this.params);
    this.homeService.updateMaterials();
    console.log(this.homeService.getMaterialParams());
  }
}
