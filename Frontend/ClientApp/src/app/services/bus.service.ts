
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AvailableBus, SeatPlan, BookSeatInput, BookSeatResult } from '../models/bus.model';

@Injectable({
  providedIn: 'root'
})
export class BusService {
  private apiUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) { }

  searchBuses(from: string, to: string, date: string): Observable<AvailableBus[]> {
    return this.http.get<AvailableBus[]>(`${this.apiUrl}/search/buses?from=${from}&to=${to}&journeyDate=${date}`);
  }

  getSeatPlan(scheduleId: string): Observable<SeatPlan> {
    return this.http.get<SeatPlan>(`${this.apiUrl}/booking/seat-plan/${scheduleId}`);
  }

  bookSeat(bookingInput: BookSeatInput): Observable<BookSeatResult> {
    return this.http.post<BookSeatResult>(`${this.apiUrl}/booking/book`, bookingInput);
  }
}
