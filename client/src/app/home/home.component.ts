import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IMaterial } from '../shared/models/material';
import { HomeService } from './home.service';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
	materialsObservable$?: Observable<IMaterial[]>; 
	constructor(public homeService: HomeService) {}
	
	ngOnInit(): void {
		this.homeService.updateMaterials();
		this.materialsObservable$ = this.homeService.getMaterials();
	}


}
