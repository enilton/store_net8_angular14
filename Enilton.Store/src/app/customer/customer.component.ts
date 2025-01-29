import { Component, OnInit } from '@angular/core';
import { CustomerService } from './../../shared/service/customer.service';
import { Customer } from '../../shared/model/customer.model';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = [];
  customer: Customer = { id: '', name: '', email: '', phone: '' };

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.customerService.getCustomers().subscribe((data) => {
      this.customers = data;
    });
  }

  saveCustomer(): void {
    if (this.customer.id) {
      this.customerService
        .updateCustomer(this.customer.id, this.customer)
        .subscribe(() => {
          this.loadCustomers();
          this.resetForm();
        });
    } else {
      this.customerService.createCustomer(this.customer).subscribe(() => {
        this.loadCustomers();
        this.resetForm();
      });
    }
  }

  editCustomer(customer: Customer): void {
    this.customer = { ...customer };
  }

  deleteCustomer(id: string): void {
    this.customerService.deleteCustomer(id).subscribe(() => {
      this.loadCustomers();
    });
  }

  resetForm(): void {
    this.customer = { id: '', name: '', email: '', phone: '' };
  }
}
