import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

const routes: Routes = [{
  path: '',
  loadChildren: () =>
    import('./materials/materials.module').then((mod) => mod.MaterialsModule),
}, {

  path: 'auth',
  loadChildren: () =>
    import('./auth/auth.module').then((mod) => mod.AuthModule),
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
