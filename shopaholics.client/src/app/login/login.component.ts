import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model = {
    email: '',
    password: ''
  };
  serverErrors: string[] = [];

  constructor(private http: HttpClient, private router: Router, private authService: AuthService) { }

  onSubmit() {
    this.http.post<any>('http://localhost:5223/api/Users/login', {
      email: this.model.email,
      password: this.model.password
    }).subscribe({
      next: (res) => {
        this.authService.setToken(res.token);
        this.authService.setUserEmail(res.email ?? this.model.email);
        this.router.navigate(['/products']);
      },
      error: (err) => {
        console.error('Login error', err);
        this.serverErrors = err.error;
      }
    });
  }
}

