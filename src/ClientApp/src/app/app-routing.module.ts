import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeoDistanceCalculateComponent } from './geo-distance-calculate/geo-distance-calculate.component';

const routes: Routes = [
  {
    path: '',
    component: GeoDistanceCalculateComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
