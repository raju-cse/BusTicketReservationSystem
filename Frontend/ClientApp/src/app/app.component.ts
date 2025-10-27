
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <nav class="navbar navbar-dark bg-primary">
      <div class="container">
        <a class="navbar-brand" href="#">🚌 Bus Ticket Reservation</a>
        <div>
          <app-auth></app-auth>
        </div>
      </div>
    </nav>
    <div class="container mt-4">
      <app-search></app-search>
    </div>
  `
})
export class AppComponent {}
