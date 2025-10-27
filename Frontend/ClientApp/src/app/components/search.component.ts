
import { Component } from '@angular/core';
import { BusService } from '../services/bus.service';
import { AvailableBus } from '../models/bus.model';

@Component({
  selector: 'app-search',
  template: `
    <div class="card">
      <div class="card-body">
        <h3>Search Buses</h3>
        <form (ngSubmit)="searchBuses()" #searchForm="ngForm">
          <div class="row">
            <div class="col-md-3">
              <input type="text" class="form-control" [(ngModel)]="searchFrom" 
                     name="from" placeholder="From City" required>
            </div>
            <div class="col-md-3">
              <input type="text" class="form-control" [(ngModel)]="searchTo" 
                     name="to" placeholder="To City" required>
            </div>
            <div class="col-md-3">
              <input type="date" class="form-control" [(ngModel)]="searchDate" 
                     name="date" required>
            </div>
            <div class="col-md-3">
              <button type="submit" class="btn btn-primary w-100" 
                      [disabled]="!searchForm.form.valid">Search</button>
            </div>
          </div>
        </form>
      </div>
    </div>

    <div *ngIf="buses.length > 0" class="mt-4">
      <h4>Available Buses ({{buses.length}})</h4>
      <div class="card mb-3" *ngFor="let bus of buses">
        <div class="card-body">
          <div class="row align-items-center">
            <div class="col-md-3">
              <h5>{{bus.companyName}}</h5>
              <p class="text-muted">{{bus.busName}} - {{bus.busNumber}}</p>
              <span class="badge" [ngClass]="bus.hasAC ? 'bg-success' : 'bg-secondary'">
                {{bus.hasAC ? 'AC' : 'NON AC'}}
              </span>
            </div>
            <div class="col-md-2">
              <strong>Departure</strong>
              <p>{{formatTime(bus.startTime)}}</p>
            </div>
            <div class="col-md-2">
              <strong>Arrival</strong>
              <p>{{formatTime(bus.arrivalTime)}}</p>
            </div>
            <div class="col-md-2">
              <strong>Seats Left</strong>
              <p>{{bus.seatsLeft}}</p>
            </div>
            <div class="col-md-2">
              <strong>Fare</strong>
              <p>৳{{bus.price}}</p>
            </div>
            <div class="col-md-1">
              <button class="btn btn-success mt-2" 
                      (click)="viewSeats(bus.scheduleId)">View Seats</button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div *ngIf="showSeatSelection && selectedScheduleId">
      <app-seat-selection [scheduleId]="selectedScheduleId"></app-seat-selection>
    </div>
  `
})
export class SearchComponent {
  searchFrom = 'Dhaka';
  searchTo = 'Chittagong';
  searchDate = '';
  buses: AvailableBus[] = [];
  showSeatSelection = false;
  selectedScheduleId = '';

  constructor(private busService: BusService) {
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    this.searchDate = tomorrow.toISOString().split('T')[0];
  }

  searchBuses() {
    this.busService.searchBuses(this.searchFrom, this.searchTo, this.searchDate)
      .subscribe({
        next: (buses) => {
          this.buses = buses;
          this.showSeatSelection = false;
        },
        error: (error) => {
          console.error('Search failed:', error);
          alert('Search failed. Please try again.');
        }
      });
  }

  viewSeats(scheduleId: string) {
    this.selectedScheduleId = scheduleId;
    this.showSeatSelection = true;
  }

  formatTime(timeString: string): string {
    const time = new Date('1970-01-01T' + timeString + 'Z');
    return time.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: true });
  }
}
