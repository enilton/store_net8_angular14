import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../shared/http/api.service';
import { Product } from './../model/product.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
    private endpoint = `${environment.apiBaseUrl}/product`;

  constructor(private apiService: ApiService) {}

  getProducts(): Observable<Product[]> {
    return this.apiService.get<Product[]>(this.endpoint);
  }

  getProductById(id: string): Observable<Product> {
    return this.apiService.get<Product>(`${this.endpoint}/${id}`);
  }

  createProduct(product: Product): Observable<Product> {
    return this.apiService.post<Product>(this.endpoint, {
      id: product.id || null,
      name: product.name,
      price: product.price
    });
  }

  updateProduct(id: string, product: Product): Observable<Product> {
    return this.apiService.put<Product>(`${this.endpoint}/${id}`, product);
  }

  deleteProduct(id: string): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${id}`);
  }
}