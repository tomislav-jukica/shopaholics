import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Product {
  title: string;
  description: string;
  price: number;
  thumbnail: string;
  isFavorite?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private apiBase = 'http://localhost:5223/api';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiBase}/Products`);
  }
}
