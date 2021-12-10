import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {AuthService} from 'src/app/auth/auth.service';
import {MaterialsService} from 'src/app/materials/materials.service';
import {User} from 'src/app/shared/models/user';
import {Observable} from "rxjs";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm?: ElementRef;

  currentUser$?: Observable<User | null>;

  constructor(public authService: AuthService, public materialsService: MaterialsService) {
  }

  ngOnInit(): void {
    this.currentUser$ = this.authService.getUserObservable();
  }

  logout() {
    this.authService.logout().then(r => {
      this.materialsService.updateMaterials()
      this.currentUser$?.subscribe(result=>{console.log(result)})
    });

  }

  onSearch(){
    if(this.searchTerm){
      const params = this.materialsService.getMaterialParams();
      params.search = this.searchTerm.nativeElement.value;
      this.materialsService.setMaterialParams(params);
      this.materialsService.updateMaterials();
    }
  }

  onReset() {
    this.searchTerm!.nativeElement.value = '';
    const params = this.materialsService.getMaterialParams();
    params.search = undefined;
    this.materialsService.setMaterialParams(params);
    this.materialsService.updateMaterials();
  }
}
