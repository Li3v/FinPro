import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private api = 'http://localhost:5172/auth';

  constructor(private http: HttpClient) {}

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUserName(): string | null {
    const token = this.getToken();
    if (!token) return null;

    const decoded: any = jwtDecode(token);
    console.log(jwtDecode(token));
    return decoded[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
    ];
  }

  login(username: string, password: string) {
    return this.http.post<any>(`${this.api}/login`, {
      name: username,
      password: password,
    });
  }

  logOut() {
    localStorage.removeItem('token');
  }

  register(name: string, password: string, email: string) {
    return this.http.post<any>(`${this.api}/register`, {
      name,
      password,
      email,
    });
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }
}
