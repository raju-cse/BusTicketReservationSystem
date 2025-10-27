
export interface AvailableBus {
  scheduleId: string;
  companyName: string;
  busName: string;
  busNumber: string;
  hasAC: boolean;
  startTime: string;
  arrivalTime: string;
  seatsLeft: number;
  price: number;
}

export interface Seat {
  seatId: string;
  seatNumber: string;
  row: number;
  column: number;
  status: string;
}

export interface SeatPlan {
  busScheduleId: string;
  seats: Seat[];
  boardingPoints: string[];
  droppingPoints: string[];
}

export interface BookSeatInput {
  busScheduleId: string;
  seatId: string;
  passengerName: string;
  mobileNumber: string;
  boardingPoint: string;
  droppingPoint: string;
}

export interface BookSeatResult {
  success: boolean;
  ticketId: string;
  message: string;
}
