import { BaseEntity } from './baseEntity';

export class Debt extends BaseEntity {

    stringAppUserId: string;
    values: DebtInfo[];

    constructor() {
        super();
        this.stringAppUserId = '-1';
        this.values = [];
    }
}

export class DebtInfo extends BaseEntity {
    debtType: DebtType;
    totalAmount: number;
    date: Date;
    bankName: string;
    installmentCount: number;

    constructor() {
        super();
        this.debtType = DebtType.None;
        this.totalAmount = 0;
        this.date = new Date();
        this.bankName = '';
        this.installmentCount = 0;
    }

    isValid(): boolean {
        if (this.debtType == null || this.debtType == DebtType.None) {
            return false;
        }

        if (this.totalAmount == null || this.totalAmount < 0) {
            return false;
        }

        if (this.date == null) {
            return false;
        }

        if (this.bankName == null || this.bankName == '') {
            return false;
        }

        return true;
    }
}

export enum DebtType {
    None = -1,
    Loan
}
