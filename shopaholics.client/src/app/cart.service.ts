import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Product } from './products/products.service';

export interface CartItem {
  product: Product;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartKey = 'shopaholics_cart';
  private cartSubject = new BehaviorSubject<CartItem[]>(this.loadCart());
  cart$ = this.cartSubject.asObservable();

  private loadCart(): CartItem[] {
    const stored = localStorage.getItem(this.cartKey);
    return stored ? JSON.parse(stored) : [];
  }

  private saveCart(cart: CartItem[]) {
    localStorage.setItem(this.cartKey, JSON.stringify(cart));
    this.cartSubject.next(cart);
  }

  addToCart(product: Product) {
    const cart = this.cartSubject.value;
    const existing = cart.find(item => item.product.title === product.title);
    if (existing) {
      existing.quantity++;
    } else {
      cart.push({ product, quantity: 1 });
    }
    this.saveCart(cart);
  }

  removeFromCart(productTitle: string) {
    const cart = this.cartSubject.value.filter(item => item.product.title !== productTitle);
    this.saveCart(cart);
  }

  updateQuantity(productTitle: string, quantity: number) {
    const cart = this.cartSubject.value;
    const item = cart.find(i => i.product.title === productTitle);
    if (item) {
      item.quantity = quantity;
      if (item.quantity <= 0) {
        this.removeFromCart(productTitle);
      } else {
        this.saveCart(cart);
      }
    }
  }

  getCartItems(): CartItem[] {
    return this.cartSubject.value;
  }

  getTotalItems(): number {
    return this.cartSubject.value.reduce((sum, item) => sum + item.quantity, 0);
  }

  getTotalPrice(): number {
    return this.cartSubject.value.reduce((sum, item) => sum + (item.product.price * item.quantity), 0);
  }

  clearCart() {
    this.saveCart([]);
  }
}