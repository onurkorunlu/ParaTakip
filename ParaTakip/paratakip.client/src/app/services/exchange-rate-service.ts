
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { ExchangeRate } from '../models/exchangeRate';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateService {

  constructor(public httpService: HttpService) { }

  get(): Observable<ExchangeRate> {
    return this.httpService.get<ExchangeRate>("exchangerate/get");
  }

}
