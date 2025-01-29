import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CustomerComponent } from './customer.component';
import { CustomerService } from './../../shared/service/customer.service';
import { of } from 'rxjs';

describe('CustomerComponent', () => {
  let component: CustomerComponent;
  let fixture: ComponentFixture<CustomerComponent>;
  let mockCustomerService: jasmine.SpyObj<CustomerService>;

  beforeEach(async () => {
    mockCustomerService = jasmine.createSpyObj('CustomerService', ['getCustomers', 'createCustomer']);

    await TestBed.configureTestingModule({
      declarations: [CustomerComponent],
      providers: [{ provide: CustomerService, useValue: mockCustomerService }],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomerComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load customers on init', () => {
    const mockCustomers = [{ id: '1', name: 'John Doe', email: 'john@example.com', phone: '123456789' }];
    mockCustomerService.getCustomers.and.returnValue(of(mockCustomers));

    component.ngOnInit();
    expect(component.customers).toEqual(mockCustomers);
  });

  it('should save a customer', () => {
    const newCustomer = { id: '', name: 'Jane Doe', email: 'jane@example.com', phone: '987654321' };
    mockCustomerService.createCustomer.and.returnValue(of(newCustomer));

    component.saveCustomer();
    expect(mockCustomerService.createCustomer).toHaveBeenCalled();
  });
});