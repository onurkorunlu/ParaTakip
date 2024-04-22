export interface FundTradingRate {
    [key: string]: FundInfo ;
  }
  
  export interface FundInfo {
    name: string
    price: number
    dailyReturn: number
    shares: number
    fundTotalValue: number
    category: string
  }