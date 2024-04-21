export class Utils {
  static GetCurrencySymbol(localCurrency: string) {
    switch (localCurrency) { 
      case 'TRY': return '₺';
      case 'USD': return '$';
      case 'EUR': return '€';
      default: return '';
    }
  }
  static getErrorMessage(err: any): string {

    debugger;

    if(err.error.errors != undefined){
        return err.error.errors.join(',');
    }

    return 'Beklenmeyen bir hata oluştu.';
  }
  constructor() {}

  public static stringIsEmpty(word:string){
    if (word==null || word=='') {
        return true;
    }

    return false;
  }
}