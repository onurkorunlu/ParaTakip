export interface ExchangeRate {
    [key: string]: ExchangeRateInfo ;
  }
  
  export interface ExchangeRateInfo {
    currencyCode:string;
    name:string;
    buying:number;
    selling:number;
    minValue:number;
    maxValue:number;
    change:number;
    time:string;
  }
  