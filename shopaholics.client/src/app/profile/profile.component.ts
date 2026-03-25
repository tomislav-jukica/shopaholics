import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  userEmail: string | null = null;

  constructor(private authService: AuthService) {
    this.userEmail = this.authService.getUserEmail();
  }

  logout() {
    this.authService.logout();
  }

}
