import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ProductsService, Product } from './products.service';
import { CartService } from '../cart.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements AfterViewInit {
  displayedColumns: string[] = ['thumbnail', 'title', 'description', 'price', 'favorite', 'cart'];
  dataSource = new MatTableDataSource<Product>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private productsService: ProductsService, private cartService: CartService) {
    this.fetchProducts();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  private fetchProducts() {
    this.productsService.getProducts().subscribe({
      next: (data) => {
        this.dataSource.data = data.map((p) => ({ ...p, isFavorite: false }));
      },
      error: (err) => {
        console.error('Product fetch error', err);
      }
    });
  }

  toggleFavorite(item: Product) {
    item.isFavorite = !item.isFavorite;
    this.dataSource.data = this.dataSource.data.map((p) => p === item ? item : p);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  addToCart(product: Product) {
    this.cartService.addToCart(product);
  }
}

