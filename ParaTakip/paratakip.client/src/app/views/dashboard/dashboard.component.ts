import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import {
    AvatarComponent,
    ButtonDirective,
    ButtonGroupComponent,
    CardBodyComponent,
    CardComponent,
    CardFooterComponent,
    CardHeaderComponent,
    ColComponent,
    FormCheckLabelDirective,
    GutterDirective,
    ProgressBarDirective,
    ProgressComponent,
    RowComponent,
    TableDirective,
    TextColorDirective,
} from '@coreui/angular';
import { ChartjsComponent } from '@coreui/angular-chartjs';
import { IconDirective } from '@coreui/icons-angular';

import { WidgetsBrandComponent } from '../widgets/widgets-brand/widgets-brand.component';
import { WidgetsDropdownComponent } from '../widgets/widgets-dropdown/widgets-dropdown.component';
import { CommonModule, JsonPipe } from '@angular/common';
import { NgbModal, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { CalendarView, CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarModule } from 'angular-calendar';
import { CalendarDatePipe } from 'angular-calendar/modules/common/calendar-date/calendar-date.pipe';
import { subDays, startOfDay, addDays, endOfMonth, addHours, isSameMonth, isSameDay, endOfDay } from 'date-fns';
import { Subject } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { FlatpickrModule } from 'angularx-flatpickr';
import { EventColor } from 'calendar-utils';
import { CalendarService } from 'src/app/services/calendar-service';
import { AppCalendarEvent, AppEventType } from 'src/app/models/entities/calendarEvents';
import { ToastService } from 'src/app/services/toast.service';
import { EventType } from '@angular/router';

const colors: Record<string, EventColor> = {
    red: {
        primary: '#ad2121',
        secondary: '#FAE3E3',
    },
    blue: {
        primary: '#1e90ff',
        secondary: '#D1E8FF',
    },
    yellow: {
        primary: '#e3bc08',
        secondary: '#FDF1BA',
    },
};

@Component({
    templateUrl: 'dashboard.component.html',
    styleUrls: ['dashboard.component.scss'],
    standalone: true,
    imports: [WidgetsDropdownComponent, TextColorDirective, CardComponent, CardBodyComponent, RowComponent, ColComponent, ButtonDirective, IconDirective, ButtonGroupComponent, FormCheckLabelDirective, ChartjsComponent, CardFooterComponent, GutterDirective, ProgressBarDirective, ProgressComponent, WidgetsBrandComponent, CardHeaderComponent, TableDirective, AvatarComponent, FlatpickrModule, NgbModalModule, CalendarModule, CommonModule, FormsModule, JsonPipe, CardComponent]
})
export class DashboardComponent implements OnInit {

    @ViewChild('cardModalContent', { static: true }) cardModalContent: TemplateRef<any> | undefined;

    CalendarEvents: AppCalendarEvent[] = <any>[];
    events: CalendarEvent[] = [];
    json: JsonPipe | undefined
    calendarDate: CalendarDatePipe | undefined;
    view: CalendarView = CalendarView.Month;
    CalendarView = CalendarView;
    viewDate: Date = new Date();
    refresh = new Subject<void>();
    activeDayIsOpen: boolean = true;
    modalData: {
        action: string;
        event: CalendarEvent;
    } | undefined;

    constructor(private modal: NgbModal, public calendarService: CalendarService, public toastService: ToastService) {

    }

    ngOnInit(): void {

        this.calendarService.get(new Date().getFullYear() + "-" + (new Date().getMonth() + 1)).subscribe({
            next: (v) => {
                this.CalendarEvents = v;

                this.events = this.CalendarEvents.map((item) => {
                    return {
                        start: startOfDay(item.startDate),
                        end: startOfDay(item.endDate),
                        title: item.title,
                        color: this.getEventColor(item.eventType),
                        allDay: item.allDay,
                        eventData: item.eventData,
                        appEventType: item.eventType,
                    }
                });

            },
            error: (e) => this.toastService.showError(e.message),
            complete: () => console.info('complete')
        })
    }

    dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
        if (isSameMonth(date, this.viewDate)) {
            if (
                (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
                events.length === 0
            ) {
                this.activeDayIsOpen = false;
            } else {
                this.activeDayIsOpen = true;
            }
            this.viewDate = date;
        }
    }

    handleEvent(action: string, event: CalendarEvent): void {
        this.modalData = { event, action };
        switch (event.appEventType) {
            case AppEventType.CreditCardStatement:
                this.modal.open(this.cardModalContent, { size: 'lg' });
                break;
            case AppEventType.CreditCardStatementLastPayment:
                this.modal.open(this.cardModalContent, { size: 'lg' });
                break;
        }
    }

    setView(view: CalendarView) {
        this.view = view;
    }

    closeOpenMonthViewDay() {
        this.activeDayIsOpen = false;
    }

    getEventColor(event: AppEventType): EventColor {
        switch (event) {
            case AppEventType.CreditCardStatement:
                return colors['yellow'];
            case AppEventType.CreditCardStatementLastPayment:
                return colors['red'];
            default:
                return colors['red'];
        }
    }

    getCreditCardType(creditCardNumber: string) {
        // start without knowing the credit card type
        var result = "unknown";

        // first check for MasterCard
        if (/^5[1-5]/.test(creditCardNumber)) {
            result = "mastercard";
        }
        // then check for Visa
        else if (/^4/.test(creditCardNumber)) {
            result = "visa";
        }
        else if (/^9792|^65|^3657|^2205/.test(creditCardNumber)) {
            result = "troy";
        }
        // then check for AmEx
        else if (/^3[47]/.test(creditCardNumber)) {
            result = "amex";
        }
        // then check for Discover
        else if (/6(?:011|5[0-9]{2})[0-9]{12}/.test(creditCardNumber)) {
            result = "discover";
        }



        return result;
    }

    getDate(day: number) {
        return new Date(new Date().getFullYear(), new Date().getMonth() + 1, day);
    }
}
