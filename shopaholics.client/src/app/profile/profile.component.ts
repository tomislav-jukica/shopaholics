import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { CartService } from '../cart.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  userEmail: string | null = null;
  cartItemCount = 0;

  constructor(private authService: AuthService, private cartService: CartService) {
    this.userEmail = this.authService.getUserEmail();
  }

  ngOnInit() {
    this.cartService.cart$.subscribe(items => {
      this.cartItemCount = this.cartService.getTotalItems();
    });
  }

  logout() {
    this.authService.logout();
  }
}
