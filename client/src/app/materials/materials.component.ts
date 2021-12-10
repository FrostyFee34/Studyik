import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {IMaterial} from '../shared/models/material';
import {MaterialsService} from './materials.service';
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './materials.component.html',
  styleUrls: ['./materials.component.scss'],
})
export class MaterialsComponent implements OnInit {
  materialsObservable$?: Observable<IMaterial[]>;

  constructor(public materialsService: MaterialsService, private router: Router) {
  }

  ngOnInit(): void {
    this.materialsObservable$ = this.materialsService.getMaterials();
  }


  onCreate() {
    let material: IMaterial = {
      category: "Text",
      categoryId: 3,
      creationDate: new Date(),
      id: 0,
      title: "New material"
    }

    this.materialsService.insertMaterial(material).subscribe(
      result => {
        if (result.id !== 0)
          this.router.navigateByUrl(`/material-view/${result.id}`);
      }, error => {
        console.log(error);
      }
    )
  }
}
