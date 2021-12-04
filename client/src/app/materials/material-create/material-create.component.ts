import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-material-create',
  templateUrl: './material-create.component.html',
  styleUrls: ['./material-create.component.scss']
})
export class MaterialCreateComponent implements OnInit {

  materialForm!: FormGroup;
  returnUrl!: string;
  constructor(
    private activatedRoute: ActivatedRoute,) { }


  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '';
    this.createMaterialForm();
  }


  private createMaterialForm() {
    this.materialForm = new FormGroup({
      title: new FormControl('', [
        Validators.required
      ]),
      category: new FormControl('', Validators.required),
      link: new FormControl(''),
      content: new FormControl(''),
      group: new FormControl('')
    });
  }

  onSubmit(){

  }
}
