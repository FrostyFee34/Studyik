import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {IMaterial} from "../../shared/models/material";
import {ActivatedRoute} from "@angular/router";
import {MaterialsService} from "../materials.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {FilesService} from "../files.service";
import {NotesService} from "../notes.service";
import {Observable} from "rxjs";
import {INote} from "../../shared/models/note";
import {YouTubePlayer} from "@angular/youtube-player";

@Component({
  selector: 'app-material-view',
  templateUrl: './material-view.component.html',
  styleUrls: ['./material-view.component.scss']
})
export class MaterialViewComponent implements OnInit, AfterViewInit {

  material?: IMaterial;
  materialForm?: FormGroup;
  notes$?: Observable<INote[] | null>;
  @ViewChild('youtubePlayer', {static: false}) youtubePlayer?: YouTubePlayer;

  constructor(private materialsService: MaterialsService, private route: ActivatedRoute,
              private filesService: FilesService, private notesService: NotesService) {
  }

  seekTo(seconds: number){
    console.log(seconds);
    this.youtubePlayer?.seekTo(seconds, true);
  }

  ngAfterViewInit(): void {

  }

  ngOnInit(): void {
    this.setMaterial(true);
  }

  createMaterialForm() {
    this.materialForm = new FormGroup({
      title: new FormControl(this.material?.title),
      content: new FormControl(this.material?.content),
      link: new FormControl(this.material?.link, [Validators.required]),
      fileSource: new FormControl('', [Validators.required]),
      fileName: new FormControl('',)
    })
  }

  setMaterial(hardLoad = false) {
    let id = this.route.snapshot.paramMap.get('id') as unknown as number;
    if (!isNaN(id)) {
      this.materialsService.getMaterial(+id, hardLoad).subscribe(
        response => {
          this.material = response;
          this.setNotes();
          this.createMaterialForm();
        }
      )
    }
  }

  setNotes(){
    if (this.material) {
      this.notesService.updateNotes(this.material?.id);
      this.notes$ = this.notesService.notes.asObservable();
    }
  }

  onSubmit() {
    let updatedMaterial: IMaterial = {
      id: this.material!.id,
      title: this.materialForm?.value.title,
      content: this.materialForm?.value.content,
      categoryId: this.material?.categoryId
    }

    if (this.materialForm?.value.fileName) {
      updatedMaterial.categoryId = 1;
      const formData = new FormData();
      formData.append('file', this.materialForm.get('fileName')?.value);
      this.filesService.uploadFile(formData, this.material!.id).subscribe(
        () => {
        }, error => {
          console.log(error);
        }
      );
    } else if (this.materialForm?.value.link) {
      updatedMaterial.link = this.materialForm.value.link
      updatedMaterial.categoryId = 2;
    }
    this.materialsService.updateMaterial(updatedMaterial).subscribe(
      () => {
        this.setMaterial(true);
      }, error => {
        console.log(error);
      }
    )
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.materialForm?.patchValue({
        fileName: event.target.files[0],
      })
    }
  }

  onRemoveFile() {
    this.materialForm?.get('file')?.reset();

  }

  onDownloadFile() {
    this.filesService.downloadFile(this.material!.id).subscribe((blob: Blob): void => {
      const file = new Blob([blob], {type: 'application/pdf'});
      const fileURL = URL.createObjectURL(file);
      window.open(fileURL, '_blank', 'width=1000, height=800');
    })
  }

  onNoteCreate() {
    if (this.material?.id) {
      const note: INote = {
        id: 0,
        title: 'new note',
        content: '',
        materialId: this.material.id,
      }
      if(this.youtubePlayer){
        note.startIndex = Math.trunc(this.youtubePlayer.getCurrentTime());
      }
      this.notesService.postNote(note).subscribe(
        () => {
          this.notesService.updateNotes(note.materialId);
        }
      )
    }
  }

}


