import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../shared/http/api.service';
import { Order } from './../model/order.model';
import { OrderItem } from './../model/order-item.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private endpoint = `${environment.apiBaseUrl}/order`;

  constructor(private apiService: ApiService) {}

  createOrder(order: Order, items: OrderItem[]): Observable<any> {
    const payload = {
      ...order,
      items: items.map((item) => ({
        id: item.id || null, // Permite valores nulos para Id
        orderId: item.orderId || null,
        productId: item.productId,
        productName: item.productName,
        quantity: item.quantity,
        unitPrice: item.unitPrice,
        totalPrice: item.totalPrice,
      })),
    };

    return this.apiService.post<any>(this.endpoint, payload);
  }

  getOrders(): Observable<Order[]> {
    return this.apiService.get<Order[]>(`${this.endpoint}`);
  }
  
  getOrderItems(orderId: string): Observable<OrderItem[]> {
    return this.apiService.get<OrderItem[]>(`${this.endpoint}/${orderId}/items`);
  }
  
  deleteOrder(orderId: string): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${orderId}`);
  }

  calculateOrder(items: OrderItem[]): Observable<{ totalAmount: number; items: OrderItem[] }> {
   
    const payload = {
      items: items.map((item) => ({
        //id: item.id || null, // Permite valores nulos para Id e OrderId
        //orderId: item.orderId || null,
        //productId: item.productId,
        //productName: item.productName,
        quantity: item.quantity,
        unitPrice: item.unitPrice,
        totalPrice: item.totalPrice,
      })),
    };

    return this.apiService.post<{ totalAmount: number; items: OrderItem[] }>(
      `${this.endpoint}/calculate`,
      payload
    );
  }
}