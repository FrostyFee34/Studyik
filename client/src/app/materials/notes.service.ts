import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {ReplaySubject} from "rxjs";
import {INote} from "../shared/models/note";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  apiUrl = environment.apiUrl;
  notes = new ReplaySubject<INote[] | null>();
  constructor(private httpClient: HttpClient) { }

  getNotes(materialId: number) {
    return this.httpClient.get<INote[]>(this.apiUrl + `Notes/material/${materialId}`);
  }
  updateNotes(materialId: number){
    this.getNotes(materialId).subscribe(
      (response)=>{
        this.notes.next(response);
      }, error =>
      {
        this.notes.next(null);
      }
    )
  }
  getNote(noteId: number){
    return this.httpClient.get<INote>(this.apiUrl + `Notes/${noteId}`)
  }
  postNote(note: INote){
    return this.httpClient.post<INote>(this.apiUrl + 'Notes', note);
  }
  putNote(note: INote){
    return this.httpClient.put<INote>(this.apiUrl + 'Notes', note);
  }
  deleteNote(noteId: number){
    return this.httpClient.delete<boolean>(this.apiUrl + `Notes/${noteId}`)
  }


}
