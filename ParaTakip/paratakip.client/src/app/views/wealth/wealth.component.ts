import { Component, OnInit } from '@angular/core';
import { CommonProviders } from 'src/app/helpers/commonProviders';
import { CurrencyInfo, ExchangeRate } from 'src/app/models/exchangeRate';
import { ExchangeRateService } from 'src/app/services/exchange-rate-service';
import { formatDate } from '@angular/common';
import { WealthService } from 'src/app/services/wealth-service';
import { ToastService } from 'src/app/services/toast.service';
import { WealthType } from 'src/app/models/enums/wealthType';
import { FundService } from 'src/app/services/fund-service';
import { FundInfo, FundTradingRate } from 'src/app/models/fundTradingRate';

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

  public stockMarketAssetsTableRows: string[] | undefined;
  public stockMarketAssets: StockMarketAssets[] = [];

  public fundsTableRows: string[] | undefined;
  public fundsAssets: ForeignExchangeAssets[] = [];

  public ExchangeRateCache: ExchangeRate = <any>[];
  public ExchangeRateList: CurrencyInfo[] = <any>[];

  public FundTradingRateCache: FundTradingRate = <any>[];

  wealth:any = <any>{};
  public WealthType = WealthType;

  public addForeignExchangeForm = <any>{
    date: formatDate(new Date(), 'yyyy-MM-dd', 'en'),
    total: 0
  };

  public addFundsForm = <any>{
    date: formatDate(new Date(), 'yyyy-MM-dd', 'en'),
    total: 0,
    buying:0,
    fundCode:''
  };

  public addStockTradeForm = <any>{
    date: formatDate(new Date(), 'yyyy-MM-dd', 'en'),
    total: 0,
    buying:0,
    symbol:''
  };

  public Intl = Intl;
  constructor(public exchangeRateService: ExchangeRateService, 
    private wealthService:WealthService, 
    private toastService:ToastService,
    private fundService:FundService,
  ) { }

  ngOnInit() {
 

    this.foreignExchangeTableRows = ['Para Birimi', 'Alış Fiyatı', 'Miktar', 'Maliyet', 'Güncel Fiyat', 'Güncel Tutar', 'Kar/Zarar', 'Kar/Zarar(%)']

    this.fundsTableRows = ['Fon', 'Alış Fiyatı', 'Miktar', 'Maliyet', 'Güncel Fiyat', 'Güncel Tutar', 'Kar/Zarar', 'Kar/Zarar(%)']

    this.stockMarketAssets = [
      { currency: 'TRY', stock: { symbol: 'KCHOL', symbolDesc: 'Koç Hoding' }, buyingPrice: 88.4, currentPrice: 90, amount: 1000, currentAmount: 90000, profitAndLoss: 1600, profitAndLossPercentage: 2 },
      { currency: 'TRY', stock: { symbol: 'THY', symbolDesc: 'Türk Hava Yolları' }, buyingPrice: 170, currentPrice: 180, amount: 1000, currentAmount: 160000, profitAndLoss: 10000, profitAndLossPercentage: 6 },
      { currency: 'TRY', stock: { symbol: 'YKB', symbolDesc: 'Yapı Ve Kredi Bankası' }, buyingPrice: 12, currentPrice: 11, amount: 1000, currentAmount: 12500, profitAndLoss: -500, profitAndLossPercentage: -4 }
    ];

    this.stockMarketAssetsTableRows = ['Sembol', 'Alış Fiyatı', 'Miktar', 'Maliyet', 'Güncel Fiyat', 'Güncel Tutar', 'Kar/Zarar', 'Kar/Zarar(%)'];
    


    this.exchangeRateService.get().subscribe({
      next: (v) => {
        this.ExchangeRateCache = v;
        this.ExchangeRateList = Object.values(this.ExchangeRateCache);
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })

    this.fundService.get().subscribe({
      next: (v) => {
        this.FundTradingRateCache = v;
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })


    this.wealthService.get().subscribe({
      next: (v) => {
        this.wealth = v;

        this.wealth.values[WealthType[WealthType.FUND_TRADING]].forEach((element: { fundCode: any; }) => {
          this.getFundBuyingPriceByFundCode(element.fundCode);
        });

      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })

  }

  getFundBuyingPrice(){
    this.getFundBuyingPriceByFundCode(this.addFundsForm.fundCode);
  }

  getFundBuyingPriceByFundCode(fundCode:string){
    this.fundService.getFundValue(fundCode).subscribe({
      next: (v:FundInfo) => {
        if(v.price <=0){
          this.toastService.showError('Fon değeri alınamadı');
        }
        this.addFundsForm.buying = v.price;
        if (this.FundTradingRateCache[fundCode] == undefined) {
          this.FundTradingRateCache[fundCode] == v;
        }
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    });
  }

  add(wealthType:WealthType, model: any) {
    (this.wealth.values[WealthType[wealthType]]).push(model);
    this.addForeignExchangeForm = <any>{
      date: formatDate(new Date(), 'yyyy-MM-dd', 'en'),
      total: 0
    };

  }

  isStock(object: any): object is Stock {
    if (object.symbol) {
      return true;
    }
    return false;
  }

  calculateTotal(form:any) {
    if (form.buying && form.amount) {
      form.total = form.buying * form.amount;
    }
  }

  setBuying(form:any) {
    if (form.currency) {
      form.buying = this.ExchangeRateCache[form.currency].buying;
    }
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
        this.wealth = v;
        this.toastService.showSuccess('Kayıt başarılı');
      },
      error: (e) => this.toastService.showError(e.message),
      complete: () => console.info('complete')
    })
  }

  delete(item:any,wealthType:WealthType){
    this.wealth.values[WealthType[wealthType]] = this.wealth.values[WealthType[wealthType]].filter((v:any) => v !== item);
  }


}
