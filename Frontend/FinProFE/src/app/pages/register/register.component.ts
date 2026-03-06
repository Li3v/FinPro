import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  username = '';
  password = '';
  confirmPassword = '';
  email = '';
  error = '';

  constructor(
    private router: Router,
    private authService: AuthService,
  ) {}

  register() {
    if (!this.passwordMatchValidator()) {
      return;
    }
    this.authService
      .register(this.username, this.password, this.email)
      .subscribe({
        next: () => {
          console.log('User created');
          this.router.navigate(['/']);
        },
        error: (err) => {
          this.error = err.error?.message || 'Registration failed';
        },
      });
  }
  passwordMatchValidator(): boolean {
    if (this.password != this.confirmPassword) {
      this.error = 'Passwords do not match!';
      return false;
    }
    return true;
  }
}
