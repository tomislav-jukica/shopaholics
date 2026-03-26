import { Component, OnInit } from '@angular/core';
import { CartService, CartItem } from '../cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  totalPrice = 0;

  constructor(private cartService: CartService) {}

  ngOnInit() {
    this.cartService.cart$.subscribe(items => {
      this.cartItems = items;
      this.totalPrice = this.cartService.getTotalPrice();
    });
  }

  removeFromCart(productTitle: string) {
    this.cartService.removeFromCart(productTitle);
  }

  updateQuantity(productTitle: string, quantity: number) {
    this.cartService.updateQuantity(productTitle, quantity);
  }

  clearCart() {
    this.cartService.clearCart();
  }
}
