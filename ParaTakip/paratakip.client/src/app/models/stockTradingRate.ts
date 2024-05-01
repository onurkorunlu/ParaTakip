export interface StockTradingRate {
    [key: string]: StockInfo ;
  }
  
  export interface StockInfo {
    price: number 
    differenceRatio: number 
    difference: number 
    volume: number 
    name: string 
    startValue: number 
    dailyMinValue: number 
    dailyMaxValue: number 
    weeklyMinValue: number 
    weeklyMaxValue: number 
    monthlyMinValue: number 
    monthlyMaxValue: number 
  }