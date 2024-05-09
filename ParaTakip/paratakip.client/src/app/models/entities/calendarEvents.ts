export class AppCalendarEvent{
    eventType:AppEventType;
    title:string;
    startDate:Date;
    endDate:Date;
    allDay:boolean;
    eventData:any | null;

    constructor(){
        this.eventType = AppEventType.CreditCardStatement;
        this.title = "";
        this.startDate = new Date();
        this.endDate = new Date();
        this.allDay = false;
        this.eventData = null;
    }
}

export enum AppEventType{
    CreditCardStatement,
    CreditCardStatementLastPayment,
    LoanDebt
}