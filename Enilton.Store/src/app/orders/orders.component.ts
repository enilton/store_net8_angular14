import { Component, OnInit } from '@angular/core';
import { OrderService } from './../../shared/service/order.service';
import { CustomerService } from './../../shared/service/customer.service';
import { Order } from './../../shared/model/order.model';
import { OrderItem } from './../../shared/model/order-item.model';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
})
export class OrdersComponent implements OnInit {
  orders: OrderWithDetails[] = [];

  constructor(
    private orderService: OrderService,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getOrders().subscribe((orders) => {
      this.customerService.getCustomers().subscribe((customers) => {
        const customerMap = new Map(
          customers.map((customer) => [customer.id, customer.name])
        );

        // Preenche os dados completos com nomes dos clientes
        this.orders = orders.map((order) => ({
          ...order,
          customerName: customerMap.get(order.customerId) || 'Unknown Customer',
          items: [], // Preenchido posteriormente
        }));

        // Para cada pedido, carrega os itens associados
        this.orders.forEach((order) => {
          this.orderService.getOrderItems(order.id).subscribe((items) => {
            order.items = items;
          });
        });
      });
    });
  }

  deleteOrder(id: string): void {
    if (confirm('Are you sure you want to delete this order?')) {
      this.orderService.deleteOrder(id).subscribe(() => {
        this.orders = this.orders.filter((order) => order.id !== id);
        alert('Order deleted successfully!');
      });
    }
  }
}

// Interface adicional para exibir detalhes completos
export interface OrderWithDetails extends Order {
  customerName: string;
  items: OrderItem[];
}