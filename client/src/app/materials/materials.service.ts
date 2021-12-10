import {HttpClient, HttpParams} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {environment} from 'src/environments/environment';
import {IMaterial} from '../shared/models/material';
import {MaterialParams} from '../shared/models/materialParams';

@Injectable({
  providedIn: 'root',
})
export class MaterialsService {
  private baseUrl = environment.apiUrl;
  private materials$?: Observable<IMaterial[]>;
  private materials: IMaterial[] = [];
  private params = new MaterialParams();

  constructor(private http: HttpClient) {
  }

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
    this.materials$ = this.http.get<IMaterial[]>(this.baseUrl + 'materials', {
      params: httpParams,
    });
    this.materials$.subscribe(
      response=>{
        this.materials = response;
      }
    )
  }

  getMaterials() {
    return this.materials$;
  }

  getMaterial(materialId: number, hardLoad = false) {
    if(hardLoad){
      return this.http.get<IMaterial>(this.baseUrl + `materials/${materialId}`);
    }
    const material = this.materials.find(m => m.id === materialId);
    return of(material);

  }

  insertMaterial(material: IMaterial) {
    return this.http.post<IMaterial>(this.baseUrl + 'materials', material);
  }

  updateMaterial(material: IMaterial) {
    return this.http.put<IMaterial>(this.baseUrl + 'materials', material);
  }

  deleteMaterial(materialId: number) {
    return this.http.delete(this.baseUrl + `materials/${materialId}`)
  }

  getMaterialParams() {
    return this.params;
  }

  setMaterialParams(materialParams: MaterialParams) {
    this.params = materialParams;
  }
}
