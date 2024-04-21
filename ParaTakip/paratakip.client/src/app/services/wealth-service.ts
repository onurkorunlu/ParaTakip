
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { Wealth } from '../models/entities/wealth';

@Injectable({
  providedIn: 'root'
})
export class WealthService {

  constructor(public httpService: HttpService) { }

  get(): Observable<Wealth> {
    return this.httpService.get<Wealth>("wealth/get");
  }

  update(model:Wealth[]): Observable<Wealth[]> {
    return this.httpService.post<Wealth[]>("wealth/update", model);
  }
}
