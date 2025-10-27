
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/auth';
  private _tokenKey = 'bt_token';

  constructor(private http: HttpClient) {}

  register(name: string, mobile: string, password: string, email?: string) {
    return this.http.post(`${this.apiUrl}/register`, { name, mobileNumber: mobile, password, email });
  }

  login(mobile: string, password: string) {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, { mobileNumber: mobile, password })
      .pipe(tap(res => {
        if (res && res.token) {
          localStorage.setItem(this._tokenKey, res.token);
        }
      }));
  }

  logout() {
    localStorage.removeItem(this._tokenKey);
  }

  getToken() {
    return localStorage.getItem(this._tokenKey);
  }

  isAuthenticated() {
    return !!this.getToken();
  }
}
