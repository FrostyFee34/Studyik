import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class FilesService {

  apiUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  uploadFile(formData: FormData, id: number){
    return this.httpClient.put(this.apiUrl + `files/${id}`, formData);
  }
  downloadFile(id: number){
    return this.httpClient.get(this.apiUrl + `files/${id}`, {responseType: "blob"})
  }

}
