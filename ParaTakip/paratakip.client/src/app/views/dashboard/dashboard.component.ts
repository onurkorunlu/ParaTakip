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

  constructor(private modal: NgbModal) {

  }

  ngOnInit(): void {
  }

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any> | undefined;


  json: JsonPipe | undefined
  calendarDate:CalendarDatePipe | undefined;
  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  modalData: {
      action: string;
      event: CalendarEvent;
  } | undefined;

  actions: CalendarEventAction[] = [
      {
          label: '<i class="fas fa-fw fa-pencil-alt"></i>',
          a11yLabel: 'Edit',
          onClick: ({ event }: { event: CalendarEvent }): void => {
              this.handleEvent('Edited', event);
          },
      },
      {
          label: '<i class="fas fa-fw fa-trash-alt"></i>',
          a11yLabel: 'Delete',
          onClick: ({ event }: { event: CalendarEvent }): void => {
              this.events = this.events.filter((iEvent) => iEvent !== event);
              this.handleEvent('Deleted', event);
          },
      },
  ];

  refresh = new Subject<void>();

  events: CalendarEvent[] = [
      {
          start: subDays(startOfDay(new Date()), 1),
          end: addDays(new Date(), 5),
          title: 'A 3 day event',
          color: { ...colors['red'] },
          actions: this.actions,
          allDay: true,
          resizable: {
              beforeStart: true,
              afterEnd: true,
          },
          draggable: true,
      },
      {
        start: subDays(startOfDay(new Date()), 1),
        end: addDays(new Date(), 5),
        title: 'A 3 day event',
        color: { ...colors['red'] },
        actions: this.actions,
        allDay: true,
        resizable: {
            beforeStart: true,
            afterEnd: true,
        },
        draggable: true,
    },
      {
          start: startOfDay(new Date()),
          title: 'An event with no end date',
          color: { ...colors['yellow'] },
          actions: this.actions,
      },
      {
          start: subDays(endOfMonth(new Date()), 3),
          end: addDays(endOfMonth(new Date()), 3),
          title: 'A long event that spans 2 months',
          color: { ...colors['blue'] },
          allDay: true,
      },
      {
          start: addHours(startOfDay(new Date()), 2),
          end: addHours(new Date(), 2),
          title: 'A draggable and resizable event',
          color: { ...colors['yellow'] },
          actions: this.actions,
          resizable: {
              beforeStart: true,
              afterEnd: true,
          },
          draggable: true,
      },
  ];

  activeDayIsOpen: boolean = true;


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

  eventTimesChanged({
      event,
      newStart,
      newEnd,
  }: CalendarEventTimesChangedEvent): void {
      this.events = this.events.map((iEvent) => {
          if (iEvent === event) {
              return {
                  ...event,
                  start: newStart,
                  end: newEnd,
              };
          }
          return iEvent;
      });
      this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
      this.modalData = { event, action };
      this.modal.open(this.modalContent, { size: 'lg' });
  }

  addEvent(): void {
      this.events = [
          ...this.events,
          {
              title: 'New event',
              start: startOfDay(new Date()),
              end: endOfDay(new Date()),
              color: colors['red'],
              draggable: true,
              resizable: {
                  beforeStart: true,
                  afterEnd: true,
              },
          },
      ];
  }

  deleteEvent(eventToDelete: CalendarEvent) {
      this.events = this.events.filter((event) => event !== eventToDelete);
  }

  setView(view: CalendarView) {
      this.view = view;
  }

  closeOpenMonthViewDay() {
      this.activeDayIsOpen = false;
  }
}
