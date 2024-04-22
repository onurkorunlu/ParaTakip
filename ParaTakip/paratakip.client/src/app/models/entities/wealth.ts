import { WealthType } from '../enums/wealthType';
import { BaseEntity } from './baseEntity';

export interface Wealth extends BaseEntity{
    stringAppUserId:string
    values : any;
}

