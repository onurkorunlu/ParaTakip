import { BaseEntity } from './baseEntity';

export class CreditCard extends BaseEntity{
    maskedCardNumber : string;
    statementDay : number;
    lastPaymentDay : number;

    constructor(){
        super();
        this.maskedCardNumber = '';
        this.statementDay = 1;
        this.lastPaymentDay = 10;
    }

    isValid(): boolean {
        if(this.maskedCardNumber == null || this.maskedCardNumber == '' || this.maskedCardNumber.length != 10){
            return false;
        }

        if(this.statementDay == null || this.statementDay < 1 || this.statementDay > 31){
            return false;
        }

        if(this.lastPaymentDay == null || this.lastPaymentDay < this.statementDay || this.lastPaymentDay > 31){
            return false;
        }

        return true;
    }
}

