
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { CurrencyCache } from '../models/currencyCache';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  constructor(public httpService: HttpService) { }

  get(): Observable<CurrencyCache> {
    return this.httpService.get<CurrencyCache>("currency/get");
  }

}
