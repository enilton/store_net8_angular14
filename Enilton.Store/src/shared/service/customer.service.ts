import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../model/customer.model';
import { ApiService } from '../../shared/http/api.service';
import { environment } from '../../environments/environment'; 


@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private endpoint = `${environment.apiBaseUrl}/customer`;

  constructor(private apiService: ApiService) { }

  getCustomers(): Observable<Customer[]> {
    return this.apiService.get<Customer[]>(this.endpoint);
  }

  getCustomerById(id: string): Observable<Customer> {
    return this.apiService.get<Customer>(`${this.endpoint}/${id}`);
  }

  createCustomer(customer: Customer): Observable<Customer> {
    return this.apiService.post<Customer>(this.endpoint, {
      id: customer.id || null, 
      name: customer.name,
      email: customer.email,
      phone: customer.phone
    });
  }

  updateCustomer(id: string, customer: Customer): Observable<Customer> {
    return this.apiService.put<Customer>(`${this.endpoint}/${id}`, customer);
  }

  deleteCustomer(id: string): Observable<void> {
    return this.apiService.delete<void>(`${this.endpoint}/${id}`);
  }
}
