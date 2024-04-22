import { ApplicationConfig as AppModule, importProvidersFrom } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { LoginGuard } from './helpers/login-guard';
import { HttpService } from './services/http.service';
import { HttpConfigInterceptor } from './helpers/httpInterceptor';

import {
  provideRouter,
  withEnabledBlockingInitialNavigation,
  withHashLocation,
  withInMemoryScrolling,
  withRouterConfig,
  withViewTransitions
} from '@angular/router';

import { DropdownModule, SidebarModule } from '@coreui/angular';
import { IconSetService } from '@coreui/icons-angular';
import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { CurrencyService } from './services/currency-service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ToastService } from './services/toast.service';

export const appConfig: AppModule = {
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
    { provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true },
    provideHttpClient(),
    provideRouter(routes,
      withRouterConfig({
        onSameUrlNavigation: 'reload'
      }),
      withInMemoryScrolling({
        scrollPositionRestoration: 'top',
        anchorScrolling: 'enabled'
      }),
      withEnabledBlockingInitialNavigation(),
      withViewTransitions(),
      withHashLocation()
    ),
    importProvidersFrom(SidebarModule, DropdownModule, BrowserModule, HttpClientModule),
    IconSetService,
    HttpService,
    CurrencyService,
    provideAnimations(),
    LoginGuard,
    ToastService,
    ToastrService,
    importProvidersFrom(ToastrModule.forRoot())
  ]
};

export function getBaseUrl() {
  return '/api/';
}