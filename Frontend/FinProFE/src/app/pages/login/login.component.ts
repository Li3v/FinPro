import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  username = '';
  password = '';
  error: string | null = null;

  constructor(
    private router: Router,
    private authService: AuthService,
  ) {}
  login() {
    this.error = null;

    this.authService.login(this.username, this.password).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.access_token);
        console.log('logged in, token stored');
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error(err);
        if (err.status === 401) {
          this.error = 'Invalid username or password!\nPlease try again.';
        }
        else{
          this.error = 'Something went wrong.\nPlease try again.'
        }
      },
    });
  }
}
