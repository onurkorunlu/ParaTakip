export class BaseEntity{
    stringRecordId:string
    recordCreateDate:Date
    recordUpdateDate:Date
    recordStatus:boolean
    recordCreateUsername:string
    recordUpdateUsername:string

    constructor(){
        this.stringRecordId = '-1';
        this.recordCreateDate = new Date();
        this.recordUpdateDate = new Date();
        this.recordStatus = true;
        this.recordCreateUsername = '';
        this.recordUpdateUsername = '';
    }
}