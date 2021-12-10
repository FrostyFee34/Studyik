import {Component, OnInit} from '@angular/core';
import {AuthService} from "./auth/auth.service";
import {MaterialsService} from "./materials/materials.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(private authService: AuthService, private materialsService: MaterialsService) {
  }

  ngOnInit(): void {
    this.authService.getUserObservable().subscribe(user => {
      if (user != null) {
        this.materialsService.setMaterialParams({sort: 'dateDesc'});
        this.materialsService.updateMaterials();
      }
    })

  }
}
