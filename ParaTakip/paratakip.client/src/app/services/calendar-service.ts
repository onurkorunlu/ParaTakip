
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { Wealth } from '../models/entities/wealth';
import { AppCalendarEvent } from '../models/entities/calendarEvents';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {

  constructor(public httpService: HttpService) { }

  get(yearAndMonth:string): Observable<AppCalendarEvent[]> {
    return this.httpService.get<AppCalendarEvent[]>("calendar/getevents?yearAndMonth="+ yearAndMonth);
  }
}
