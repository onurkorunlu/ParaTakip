
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { StockTradingRate } from '../models/stockTradingRate';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  constructor(public httpService: HttpService) { }

  get(): Observable<StockTradingRate> {
    return this.httpService.get<StockTradingRate>("stockTrading/get");
  }

  getStockValue(stockCode:string): Observable<any> {
    return this.httpService.get<any>("stockTrading/getStockValue?stockCode="+stockCode);
  }
}
