import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';
import { IMaterial } from '../shared/models/material';
import { MaterialParams } from '../shared/models/materialParams';

@Injectable({
	providedIn: 'root',
})
export class HomeService {
	baseUrl = environment.apiUrl;
	materials?: Observable<IMaterial[]>;
	params = new MaterialParams();
	constructor(private http: HttpClient, private authService: AuthService) {}

	updateMaterials() {
		let httpParams = new HttpParams();
		if (this.params.categoryId) {
			httpParams = httpParams.append('categoryId', this.params.categoryId);
		}
		if (this.params.groupId) {
			httpParams = httpParams.append('groupId', this.params.groupId);
		}
		if (this.params.sort) {
			httpParams = httpParams.append('sort', this.params.sort);
		}
		if (this.params.search) {
			httpParams = httpParams.append('search', this.params.search);
		}
		this.materials = this.http.get<IMaterial[]>(this.baseUrl + 'materials', {
			params: httpParams,
		});
	}

	getMaterials() {
		return this.materials;
	}

	getMaterial(materialId: number) {
		return this.http.get<IMaterial>(this.baseUrl + `materials/${materialId}`);
	}

	insertMaterial(material: IMaterial){
		return this.http.post(this.baseUrl + 'materials', material);
	}
	
	updateMaterial(material: IMaterial){
		return this.http.put(this.baseUrl + 'materials', material);
	}

	deleteMaterial(materialId: number){
		return this.http.delete(this.baseUrl + `materials/${materialId}`)
	}

	getMaterialParams() {
		return this.params;
	}

	setMaterialParams(materialParams: MaterialParams) {
		this.params = materialParams;
	}
}
