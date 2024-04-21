import { WealthType } from '../enums/wealthType';
import { BaseEntity } from './baseEntity';

export interface Wealth extends BaseEntity{
    stringAppUserId:string
    values : WealthList;
}

export type WealthList = {
    [key in WealthType]: any;
};