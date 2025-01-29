import {  OrderStatus } from './enum/order-status.enum';

export interface Order {
    id: string;
    customerId: string;
    orderDate: Date;
    totalAmount: number;
    status: OrderStatus;
  }