import {Component, OnInit} from '@angular/core';
import {IMaterial} from "../../shared/models/material";
import {ActivatedRoute} from "@angular/router";
import {MaterialsService} from "../materials.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {FilesService} from "../files.service";

@Component({
  selector: 'app-material-view',
  templateUrl: './material-view.component.html',
  styleUrls: ['./material-view.component.scss']
})
export class MaterialViewComponent implements OnInit {


  material?: IMaterial;
  materialForm?: FormGroup;


  constructor(private materialsService: MaterialsService, private route: ActivatedRoute,
              private filesService: FilesService) {
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
          this.createMaterialForm();
        }
      )
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


}


