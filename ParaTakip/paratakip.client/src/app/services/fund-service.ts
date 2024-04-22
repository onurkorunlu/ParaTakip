
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { FundTradingRate } from '../models/fundTradingRate';

@Injectable({
  providedIn: 'root'
})
export class FundService {

  constructor(public httpService: HttpService) { }

  get(): Observable<FundTradingRate> {
    return this.httpService.get<FundTradingRate>("fundTrading/get");
  }

  getFundValue(fundCode:string): Observable<any> {
    return this.httpService.get<any>("fundTrading/getFundValue?fundCode="+fundCode);
  }
}
