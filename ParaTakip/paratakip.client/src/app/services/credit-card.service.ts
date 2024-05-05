
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { Wealth } from '../models/entities/wealth';
import { CreditCard } from '../models/entities/creditCard';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {

  constructor(public httpService: HttpService) { }

  get(): Observable<CreditCard[]> {
    return this.httpService.get<CreditCard[]>("creditCard/get");
  }

  add(model:any): Observable<CreditCard> {
    return this.httpService.post<CreditCard>("creditCard/add", model);
  }

  delete(id:string): Observable<any> {
    return this.httpService.delete<any>("creditCard/delete?id="+id);
  }
}
