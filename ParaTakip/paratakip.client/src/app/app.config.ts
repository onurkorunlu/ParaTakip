import { ApplicationConfig as AppModule, importProvidersFrom } from '@angular/core';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
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
import { CommonModule, DatePipe, NgFor, registerLocaleData } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { CurrencyService } from './services/currency-service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { ToastService } from './services/toast.service';
import { CreditCardDirectivesModule } from 'angular-cc-library';
import { IgxMaskModule, IgxInputGroupModule, IgxIconModule } from 'igniteui-angular';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { FlatpickrModule } from 'angularx-flatpickr';
import { FormsModule } from '@angular/forms';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { LOCALE_ID  } from '@angular/core';
import localTr from '@angular/common/locales/tr';
registerLocaleData(localTr);

export const appConfig: AppModule = {
  providers: [
    { provide: LOCALE_ID, useValue: "tr-TR" }, //replace "en-US" with your locale
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
    importProvidersFrom(CommonModule, FormsModule, NgbModalModule,SidebarModule, DropdownModule, BrowserModule, HttpClientModule, CreditCardDirectivesModule,BrowserAnimationsModule),
    IconSetService,
    HttpService,
    CurrencyService,
    provideAnimations(),
    LoginGuard,
    ToastService,
    ToastrService,
    importProvidersFrom(ToastrModule.forRoot()),
    importProvidersFrom(IgxMaskModule,IgxInputGroupModule,IgxIconModule),
    importProvidersFrom(CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    })),
    importProvidersFrom(FlatpickrModule.forRoot()),
    DatePipe,
  ]
};

export function getBaseUrl() {
  return '/api/';
}