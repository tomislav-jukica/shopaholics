import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

export interface Product {
    id: number;
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

    constructor(private http: HttpClient, private authService: AuthService) { }

    getFavouriteProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(`${this.apiBase}/Favourites/${this.authService.getUserEmail()}`);
    }

    getProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(`${this.apiBase}/Products`);
    }
}
