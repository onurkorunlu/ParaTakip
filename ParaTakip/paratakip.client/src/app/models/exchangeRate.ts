export interface ExchangeRate {
    [key: string]: CurrencyInfo ;
  }
  
  export interface CurrencyInfo {
    name: string
    currencyCode: string
    currencyCodeDigit: string
    unit: number
    buying: number
    lastUpdateDate: Date
  }
  