import { WealthType } from '../enums/wealthType';
import { BaseEntity } from './baseEntity';

export class Wealth extends BaseEntity{
    values : any;

    constructor(){
        super();
        this.values = [];
    }
}

