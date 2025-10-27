
import { Component, Input, OnInit } from '@angular/core';
import { BusService } from '../services/bus.service';
import { SeatPlan, Seat, BookSeatInput } from '../models/bus.model';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-seat-selection',
  template: `
    <div class="container mt-4" *ngIf="seatPlan">
      <div class="card">
        <div class="card-body">
          <h3>Select Your Seat</h3>

          <!-- Seat Layout -->
          <div class="seat-layout">
            <div *ngFor="let seat of seatPlan.seats" 
                 class="seat" 
                 [ngClass]="getSeatClass(seat)"
                 (click)="selectSeat(seat)"
                 [title]="seat.seatNumber + ' - ' + seat.status">
              {{seat.seatNumber}}
            </div>
          </div>

          <!-- Legend -->
          <div class="legend mt-3">
            <span class="legend-item available">Available</span>
            <span class="legend-item booked">Booked</span>
            <span class="legend-item selected">Selected</span>
          </div>

          <!-- Passenger Form -->
          <div class="card mt-4" *ngIf="selectedSeat">
            <div class="card-body">
              <h5>Passenger Details - Seat {{selectedSeat.seatNumber}}</h5>
              <form (ngSubmit)="confirmBooking()" #bookingForm="ngForm">
                <div class="row">
                  <div class="col-md-6">
                    <label>Passenger Name *</label>
                    <input type="text" class="form-control" [(ngModel)]="passengerName" 
                           name="name" placeholder="Enter full name" required>
                  </div>
                  <div class="col-md-6">
                    <label>Mobile Number *</label>
                    <input type="tel" class="form-control" [(ngModel)]="mobileNumber" 
                           name="mobile" placeholder="01XXXXXXXXX" required>
                  </div>
                </div>

                <div class="row mt-3">
                  <div class="col-md-6">
                    <label>Boarding Point *</label>
                    <select class="form-control" [(ngModel)]="boardingPoint" name="boarding" required>
                      <option value="">Select Boarding Point</option>
                      <option *ngFor="let point of seatPlan.boardingPoints" [value]="point">
                        {{point}}
                      </option>
                    </select>
                  </div>
                  <div class="col-md-6">
                    <label>Dropping Point *</label>
                    <select class="form-control" [(ngModel)]="droppingPoint" name="dropping" required>
                      <option value="">Select Dropping Point</option>
                      <option *ngFor="let point of seatPlan.droppingPoints" [value]="point">
                        {{point}}
                      </option>
                    </select>
                  </div>
                </div>

                <button type="submit" class="btn btn-success mt-3" 
                        [disabled]="!bookingForm.form.valid || isBooking">
                  {{isBooking ? 'Booking...' : 'Confirm Booking'}}
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div *ngIf="!seatPlan" class="text-center mt-4">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>
  `,
  styles: [`
    .seat-layout {
      display: grid;
      grid-template-columns: repeat(4, 1fr);
      gap: 10px;
      max-width: 300px;
      margin: 0 auto;
    }
    .seat {
      width: 50px;
      height: 50px;
      border: 2px solid #ddd;
      display: flex;
      align-items: center;
      justify-content: center;
      cursor: pointer;
      border-radius: 8px;
      font-weight: bold;
      transition: all 0.2s;
    }
    .available { 
      background-color: #d4edda; 
      border-color: #c3e6cb;
    }
    .available:hover { 
      background-color: #c3e6cb; 
    }
    .booked { 
      background-color: #f8d7da; 
      border-color: #f5c6cb;
      cursor: not-allowed;
    }
    .selected { 
      background-color: #cce7ff; 
      border-color: #b3d9ff;
    }
    .legend {
      display: flex;
      justify-content: center;
      gap: 15px;
    }
    .legend-item {
      padding: 5px 15px;
      border-radius: 5px;
      font-size: 14px;
    }
    .available { background-color: #d4edda; }
    .booked { background-color: #f8d7da; }
    .selected { background-color: #cce7ff; }
  `]
})
export class SeatSelectionComponent implements OnInit {
  @Input() scheduleId!: string;
  seatPlan?: SeatPlan;
  selectedSeat?: Seat;
  passengerName = '';
  mobileNumber = '';
  boardingPoint = '';
  droppingPoint = '';
  isBooking = false;

  constructor(private busService: BusService, private auth: AuthService) {}

  ngOnInit() {
    this.loadSeatPlan();
  }

  loadSeatPlan() {
    this.busService.getSeatPlan(this.scheduleId)
      .subscribe({
        next: (plan) => {
          this.seatPlan = plan;
          if (plan.boardingPoints.length > 0) {
            this.boardingPoint = plan.boardingPoints[0];
          }
          if (plan.droppingPoints.length > 0) {
            this.droppingPoint = plan.droppingPoints[0];
          }
        },
        error: (error) => {
          console.error('Failed to load seat plan:', error);
          alert('Failed to load seat plan. Please try again.');
        }
      });
  }

  getSeatClass(seat: Seat): string {
    if (seat.status === 'Booked' || seat.status === 'Sold') {
      return 'booked';
    }
    return this.selectedSeat?.seatId === seat.seatId ? 'selected' : 'available';
  }

  selectSeat(seat: Seat) {
    if (seat.status === 'Available') {
      this.selectedSeat = seat;
    }
  }

  confirmBooking() {
    if (!this.auth.isAuthenticated()) {
      alert('You must be logged in to book. Please login or register first.');
      return;
    }

    if (this.selectedSeat && this.passengerName && this.mobileNumber) {
      this.isBooking = true;

      const bookingInput: BookSeatInput = {
        busScheduleId: this.scheduleId,
        seatId: this.selectedSeat.seatId,
        passengerName: this.passengerName,
        mobileNumber: this.mobileNumber,
        boardingPoint: this.boardingPoint,
        droppingPoint: this.droppingPoint
      };

      this.busService.bookSeat(bookingInput)
        .subscribe({
          next: (result) => {
            this.isBooking = false;
            if (result.success) {
              alert(`🎉 Booking confirmed!\nTicket ID: ${result.ticketId}\n${result.message}`);
              this.loadSeatPlan();
              this.selectedSeat = undefined;
              this.passengerName = '';
              this.mobileNumber = '';
            } else {
              alert(`❌ Booking failed: ${result.message}`);
            }
          },
          error: (error) => {
            this.isBooking = false;
            console.error('Booking failed:', error);
            alert('Booking failed. Please try again.');
          }
        });
    }
  }
}
