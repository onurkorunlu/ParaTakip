
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { Wealth } from '../models/entities/wealth';
import { Debt, DebtInfo } from '../models/entities/debt';

@Injectable({
  providedIn: 'root'
})
export class DebtService {

  constructor(public httpService: HttpService) { }

  get(): Observable<any> {
    return this.httpService.get<any>("debt/get");
  }

  update(model:DebtInfo[]): Observable<Debt> {
    return this.httpService.post<Debt>("debt/update", model);
  }
}
