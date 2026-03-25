import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  model = {
    email: '',
    password: ''
  };
  serverErrors: string[] = [];

  constructor(private http: HttpClient, private router: Router) { }

  onSubmit() {
    this.http.post('http://localhost:5223/api/Users/register', this.model).subscribe({
      next: (res) => {
        alert('Registration successful!');
        this.router.navigate(['/login']);
      },
      error: err => {
        this.serverErrors = err.error;
      }
    });
  }
}
