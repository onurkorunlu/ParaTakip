import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./wealth.component').then(m => m.WealthComponent),
    data: {
      title: $localize`Cüzdanım`
    }
  }
];

