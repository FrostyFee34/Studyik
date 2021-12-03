import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { HomeService } from 'src/app/home/home.service';
import { IUser, User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  currentUser?: User | null;
  constructor(public authService: AuthService, public homeService: HomeService) { }

  ngOnInit(): void {
    this.setUser();
  }

  setUser(){
    this.authService.user$.subscribe((user) => {
      if(user != null){
        this.currentUser = user;
      }
    })
  }

  logout(){
    this.authService.logout();
    this.homeService.updateMaterials();
  }
}
