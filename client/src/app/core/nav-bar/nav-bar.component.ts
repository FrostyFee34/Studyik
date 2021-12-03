import {Component, OnInit} from '@angular/core';
import {AuthService} from 'src/app/auth/auth.service';
import {MaterialsService} from 'src/app/materials/materials.service';
import {User} from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  currentUser?: User | null;

  constructor(public authService: AuthService, public homeService: MaterialsService) {
  }

  ngOnInit(): void {
    this.setUser();
  }

  setUser() {
    this.authService.user$.subscribe((user) => {
      if (user != null) {
        this.currentUser = user;
      }
    })
  }

  logout() {
    this.authService.logout();
    this.homeService.updateMaterials();
  }
}
