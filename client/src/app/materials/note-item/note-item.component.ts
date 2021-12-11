import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

import {FormControl, FormGroup} from "@angular/forms";
import {NotesService} from "../notes.service";
import {INote} from "../../shared/models/note";

@Component({
  selector: 'app-note-item',
  templateUrl: './note-item.component.html',
  styleUrls: ['./note-item.component.scss']
})
export class NoteItemComponent implements OnInit {
  @Output() seekTo = new EventEmitter<number>();
  @Input() note?: INote;
  noteForm!: FormGroup;

  constructor(private notesService: NotesService) {
  }

  ngOnInit(): void {
    this.createForm();
    console.log(this.note);
  }

  createForm() {
    this.noteForm = new FormGroup({
        title: new FormControl(this.note?.title,),
        content: new FormControl(this.note?.content)
      },
    )
  }

  onSubmit() {
    if (this.note !== undefined) {
      this.note = {
        id: this.note!.id,
        title: this.noteForm.value.title,
        content: this.noteForm.value.text,
        materialId: this.note.materialId
      }
      this.notesService.putNote(this.note).subscribe();
    }
  }

  onReset() {
    this.createForm();
  }

  onDelete() {
    if (confirm(`Are you sure you want to delete ${this.note?.title}`)) {
      if (this.note && this.note!.id) {
        this.notesService.deleteNote(this.note!.id).subscribe(() => {
          this.notesService.updateNotes(this.note!.materialId);
        });
      }
    }
  }

  goToTime() {
    console.log(this.note?.startIndex);
    console.log(this.seekTo);
    if (this.note?.startIndex) {
      this.seekTo.emit(this.note.startIndex);
    }
  }
}
