import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./credit-card.component').then(m => m.CreditCardComponent),
    data: {
      title: $localize`Kartlarım`
    }
  }
];

