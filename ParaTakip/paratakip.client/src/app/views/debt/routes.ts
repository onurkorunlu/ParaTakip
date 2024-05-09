import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./debt.component').then(m => m.DebtComponent),
    data: {
      title: $localize`Kartlarım`
    }
  }
];

