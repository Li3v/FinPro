import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private api = 'http://localhost:5172/auth';

  constructor(private http: HttpClient) { }
  login(username: string, password: string) {
    return this.http.post<any>(`${this.api}/login`, {
      name: username,
      password: password
    });
  }

  saveToken(token: string){
    localStorage.setItem('token', token);
  }
  getToken(): string | null{
    return localStorage.getItem('token');
  }
  logOut(){
    localStorage.removeItem('token');
  }
}
