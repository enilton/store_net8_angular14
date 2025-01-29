import { Component, OnInit } from '@angular/core';
import { OrderService } from './../../shared/service/order.service';
import { CustomerService } from './../../shared/service/customer.service';
import { ProductService } from './../../shared/service/product.service';
import { Order } from './../../shared/model/order.model';
import { OrderStatus } from'./../../shared/model/enum/order-status.enum';
import { OrderItem } from './../../shared/model/order-item.model';
import { Customer } from './../../shared/model/customer.model';
import { Product } from './../../shared/model/product.model';


@Component({
  selector: 'app-new-order',
  templateUrl: './new-order.component.html',
})
export class NewOrderComponent implements OnInit {
  order: Order = { id: '', customerId: '', orderDate: new Date(), totalAmount: 0, status: OrderStatus.InProgress };
  orderItems: OrderItem[] = [];
  customers: Customer[] = [];
  products: Product[] = [];

  constructor(
    private orderService: OrderService,
    private customerService: CustomerService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.loadCustomers();
    this.loadProducts();
  }

  loadCustomers(): void {
    this.customerService.getCustomers().subscribe((data) => {
      this.customers = data;
    });
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    });
  }

  addItem(product: Product, quantity: number): void {
    const existingItem = this.orderItems.find((item) => item.productId === product.id);
    if (existingItem) {
      existingItem.quantity += quantity;
    } else {
      this.orderItems.push({
        id: '',
        orderId: '',
        productId: product.id,
        quantity,
        productName: product.name,
        unitPrice: product.price,
        totalPrice: 0,
      });
    }
    this.calculateOrder();
  }

  calculateOrder(): void {
    this.order.totalAmount = this.orderItems.reduce(
      (total, item) => total + item.quantity * item.unitPrice,
      0
    );
    this.orderItems.forEach((item) => {
      item.totalPrice = item.quantity * item.unitPrice;
    });
  }

  saveOrder(): void {
    this.orderService.createOrder(this.order, this.orderItems).subscribe(() => {
      alert('Order and items created successfully!');
      this.resetForm();
    });
  }

  resetForm(): void {
    this.order = { id: '', customerId: '', orderDate: new Date(), totalAmount: 0, status: OrderStatus.InProgress };
    this.orderItems = [];
  }
}