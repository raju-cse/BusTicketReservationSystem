
import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-auth',
  template: `
    <div class="d-flex gap-2 align-items-center">
      <div *ngIf="!auth.isAuthenticated()">
        <button class="btn btn-outline-light btn-sm me-2" (click)="showLogin = !showLogin">Login</button>
        <button class="btn btn-light btn-sm" (click)="showRegister = !showRegister">Register</button>
      </div>
      <div *ngIf="auth.isAuthenticated()">
        <button class="btn btn-light btn-sm" (click)="logout()">Logout</button>
      </div>
    </div>

    <div *ngIf="showLogin" class="card mt-2 p-2">
      <form (ngSubmit)="login()" #loginForm="ngForm">
        <div class="d-flex gap-2">
          <input class="form-control form-control-sm" placeholder="Mobile" [(ngModel)]="loginMobile" name="loginMobile" required>
          <input class="form-control form-control-sm" type="password" placeholder="Password" [(ngModel)]="loginPassword" name="loginPassword" required>
          <button class="btn btn-success btn-sm" [disabled]="!loginForm.form.valid">Sign In</button>
        </div>
      </form>
    </div>

    <div *ngIf="showRegister" class="card mt-2 p-2">
      <form (ngSubmit)="register()" #regForm="ngForm">
        <div class="d-flex gap-2">
          <input class="form-control form-control-sm" placeholder="Name" [(ngModel)]="regName" name="regName" required>
          <input class="form-control form-control-sm" placeholder="Mobile" [(ngModel)]="regMobile" name="regMobile" required>
          <input class="form-control form-control-sm" type="password" placeholder="Password" [(ngModel)]="regPassword" name="regPassword" required>
          <button class="btn btn-primary btn-sm" [disabled]="!regForm.form.valid">Register</button>
        </div>
      </form>
    </div>
  `
})
export class AuthComponent {
  showLogin = false;
  showRegister = false;
  loginMobile = '';
  loginPassword = '';
  regName = '';
  regMobile = '';
  regPassword = '';

  constructor(public auth: AuthService) {}

  login() {
    this.auth.login(this.loginMobile, this.loginPassword).subscribe({
      next: () => {
        this.showLogin = false;
        alert('Logged in successfully');
      },
      error: () => alert('Login failed')
    });
  }

  register() {
    this.auth.register(this.regName, this.regMobile, this.regPassword).subscribe({
      next: () => {
        this.showRegister = false;
        alert('Registered successfully. Please login.');
      },
      error: () => alert('Register failed')
    });
  }

  logout() {
    this.auth.logout();
    alert('Logged out');
  }
}
