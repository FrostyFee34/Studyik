import { Component, Input, OnInit } from '@angular/core';
import { IMaterial } from 'src/app/shared/models/material';

@Component({
	selector: 'app-material-item',
	templateUrl: './material-item.component.html',
	styleUrls: ['./material-item.component.scss'],
})
export class MaterialItemComponent implements OnInit {
	@Input("materialValue") material?: IMaterial;
	constructor() {}

	ngOnInit(): void {

	}
}
