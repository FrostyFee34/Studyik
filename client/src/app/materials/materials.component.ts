import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IMaterial } from '../shared/models/material';
import { MaterialsService } from './materials.service';

@Component({
	selector: 'app-home',
	templateUrl: './materials.component.html',
	styleUrls: ['./materials.component.scss'],
})
export class MaterialsComponent implements OnInit {
	materialsObservable$?: Observable<IMaterial[]>;
	constructor(public homeService: MaterialsService) {}

	ngOnInit(): void {
		this.homeService.updateMaterials();
		this.materialsObservable$ = this.homeService.getMaterials();
	}


}
