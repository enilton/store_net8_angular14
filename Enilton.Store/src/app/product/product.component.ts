import { Component, OnInit } from '@angular/core';
import { ProductService } from './../../shared/service/product.service';
import { Product } from '../../shared/model/product.model';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})
export class ProductComponent implements OnInit {
  products: Product[] = [];
  product: Product = { id: '', name: '', price: 0 };

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    });
  }

  saveProduct(): void {
    if (this.product.id) {
      this.productService
        .updateProduct(this.product.id, this.product)
        .subscribe(() => {
          this.loadProducts();
          this.resetForm();
        });
    } else {
      this.productService.createProduct(this.product).subscribe(() => {
        this.loadProducts();
        this.resetForm();
      });
    }
  }

  editProduct(product: Product): void {
    this.product = { ...product };
  }

  deleteProduct(id: string): void {
    this.productService.deleteProduct(id).subscribe(() => {
      this.loadProducts();
    });
  }

  resetForm(): void {
    this.product = { id: '', name: '', price: 0 };
  }
}