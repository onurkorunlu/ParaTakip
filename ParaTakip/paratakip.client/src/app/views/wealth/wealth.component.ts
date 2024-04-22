import { Component, OnInit } from '@angular/core';
import { CurrencyCache } from 'src/app/models/currencyCache';
import { CurrencyService } from 'src/app/services/currency-service';
import { CommonProviders } from 'src/app/helpers/commonProviders';
import { CurrencyInfo, ExchangeRate } from 'src/app/models/exchangeRate';
import { ExchangeRateService } from 'src/app/services/exchange-rate-service';
import { Utils } from 'src/app/helpers/utils';
import { formatDate } from '@angular/common';
import { Wealth } from 'src/app/models/entities/wealth';
import { WealthService } from 'src/app/services/wealth-service';
import { ToastService } from 'src/app/services/toast.service';
import { WealthType } from 'src/app/models/enums/wealthType';

declare interface TableData {
  headerRow: string[];
  dataRows: any[][];
}

declare interface ForeignExchangeAssets {
  currency: string;
  buying: number;
  amount: number;
}

declare interface StockMarketAssets {
  stock: Stock;
  currency: string;
  buyingPrice: number;
  currentPrice: number;
  amount: number;
  currentAmount: number;
  profitAndLoss: number;
  profitAndLossPercentage: number;
}

declare interface Stock {
  symbol: string;
  symbolDesc: string;
}


@Component({
  selector: 'app-wealth',
  templateUrl: 'wealth.component.html',
  styleUrls: ['wealth.component.scss'],
  standalone: true,
  imports: [CommonProviders.Set()],
})

export class WealthComponent implements OnInit {

  public digitFormat = '1.2-2';
  public localCurrency = 'TRY';

  public foreignExchangeTableRows: string[] | undefined;
  public foreignExchangeAssets: ForeignExchangeAssets[] = [];

  public stockMarketAssetsTableRows: string[] | undefined;
  public stockMarketAssets: StockMarketAssets[] = [];

  public ExchangeRateCache: ExchangeRate = <any>[];
  public ExchangeRateList: CurrencyInfo[] = <any>[];

  wealth:any = <any>{};
  public WealthType = WealthType;

  public addForeignExchangeForm = <any>{
    date: formatDate(new Date(), 'yyyy-MM-dd', 'en'),
    total: 0
  };

  public Intl = Intl;
  constructor(public exchangeRateService: ExchangeRateService, private wealthService:WealthService, private toastService:ToastService) { }

  ngOnInit() {
    //#region foreignExchangeAssets
    this.foreignExchangeAssets = [
      { currency: 'USD', buying: 25.20, amount: 2000},
      { currency: 'EUR', buying: 30.30, amount: 5000 },
    ];

    this.foreignExchangeTableRows = ['Para Birimi', 'Alış Fiyatı', 'Miktar', 'Maliyet', 'Güncel Fiyat', 'Güncel Tutar', 'Kar/Zarar', 'Kar/Zarar(%)']

    //#endregion

    //#region stockMarketAssets
    this.stockMarketAssets = [
      { currency: 'TRY', stock: { symbol: 'KCHOL', symbolDesc: 'Koç Hoding' }, buyingPrice: 88.4, currentPrice: 90, amount: 1000, currentAmount: 90000, profitAndLoss: 1600, profitAndLossPercentage: 2 },
      { currency: 'TRY', stock: { symbol: 'THY', symbolDesc: 'Türk Hava Yolları' }, buyingPrice: 170, currentPrice: 180, amount: 1000, currentAmount: 160000, profitAndLoss: 10000, profitAndLossPercentage: 6 },
      { currency: 'TRY', stock: { symbol: 'YKB', symbolDesc: 'Yapı Ve Kredi Bankası' }, buyingPrice: 12, currentPrice: 11, amount: 1000, currentAmount: 12500, profitAndLoss: -500, profitAndLossPercentage: -4 }
    ];

    this.stockMarketAssetsTableRows = ['Sembol', 'Alış Fiyatı', 'Miktar', 'Maliyet', 'Güncel Fiyat', 'Güncel Tutar', 'Kar/Zarar', 'Kar/Zarar(%)'];
    //#endregion
    this.exchangeRateService.get().subscribe({
      next: (v) => {
        this.ExchangeRateCache = v;
        this.ExchangeRateList = Object.values(this.ExchangeRateCache);
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })

    this.wealthService.get().subscribe({
      next: (v) => {
        this.wealth = v;
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })

  }

  add(wealthType:WealthType, model: any) {
    (this.wealth.values[WealthType[wealthType]]).push(model);
    this.addForeignExchangeForm = <any>{
      date: formatDate(new Date(), 'yyyy-MM-dd', 'en'),
      total: 0
    };

  }

  addNewForeignExchangeAsset() {

  }

  isStock(object: any): object is Stock {
    if (object.symbol) {
      return true;
    }
    return false;
  }

  calculateTotal() {
    if (this.addForeignExchangeForm.buying && this.addForeignExchangeForm.amount) {
      this.addForeignExchangeForm.total = this.addForeignExchangeForm.buying * this.addForeignExchangeForm.amount;
    }
  }

  setBuying() {
    ;
    if (this.addForeignExchangeForm.currency) {
      this.addForeignExchangeForm.buying = this.ExchangeRateCache[this.addForeignExchangeForm.currency].buying;
    }
  }

  saveAddForeignExchangeForm() {
  }

  calculateProfit(buying:number,amount:number, current:number){
    return (current - buying) * amount;
  }

  calculateProfitRatio(buying:number,amount:number, current:number){
    return (current - buying) * amount / (buying * amount) * 100;
  }

  save(wealthType:WealthType){

    let model ={
      wealthType:wealthType,
      wealthValues: Object.values(this.wealth.values[WealthType[wealthType]])
    };

    this.wealthService.update(model).subscribe({
      next: (v) => {
        this.toastService.showSuccess('Kayıt başarılı');
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })

  }
}
