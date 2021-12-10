import {Component, Input, OnInit} from '@angular/core';
import {IMaterial} from 'src/app/shared/models/material';
import {Router} from "@angular/router";
import {MaterialsService} from "../materials.service";

@Component({
  selector: 'app-material-item',
  templateUrl: './material-item.component.html',
  styleUrls: ['./material-item.component.scss'],
})
export class MaterialItemComponent implements OnInit {
  @Input("materialValue") material?: IMaterial;

  constructor(private router: Router, private materialsService: MaterialsService) {
  }

  ngOnInit(): void {

  }

  onView() {
    this.router.navigate(['/material-view'], {state: {data: this.material}});
  }

  onDelete() {
    if (confirm(`Are you sure you want to delete ${this.material?.title}, all related content will be deleted as well`)) {
      this.materialsService.deleteMaterial(this.material!.id).subscribe(() => {
        this.materialsService.updateMaterials();
      }, error => console.log(error));

    }
  }
}
