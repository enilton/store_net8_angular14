import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NewOrderComponent } from './new-order.component';
import { OrderService } from './../../shared/service/order.service';
import { CustomerService } from './../../shared/service/customer.service';
import { ProductService } from './../../shared/service/product.service';
import { of } from 'rxjs';

describe('NewOrderComponent', () => {
  let component: NewOrderComponent;
  let fixture: ComponentFixture<NewOrderComponent>;
  let mockOrderService: jasmine.SpyObj<OrderService>;
  let mockCustomerService: jasmine.SpyObj<CustomerService>;
  let mockProductService: jasmine.SpyObj<ProductService>;

  beforeEach(async () => {
    mockOrderService = jasmine.createSpyObj('OrderService', ['createOrder']);
    mockCustomerService = jasmine.createSpyObj('CustomerService', ['getCustomers']);
    mockProductService = jasmine.createSpyObj('ProductService', ['getProducts']);

    await TestBed.configureTestingModule({
      declarations: [NewOrderComponent],
      providers: [
        { provide: OrderService, useValue: mockOrderService },
        { provide: CustomerService, useValue: mockCustomerService },
        { provide: ProductService, useValue: mockProductService },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(NewOrderComponent);
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

  it('should load products on init', () => {
    const mockProducts = [{ id: '1', name: 'Laptop', price: 2000 }];
    mockProductService.getProducts.and.returnValue(of(mockProducts));

    component.ngOnInit();
    expect(component.products).toEqual(mockProducts);
  });

  it('should create a new order', () => {
    const newOrder = { id: '', customerId: '1', orderDate: new Date(), totalAmount: 100, status: 1 };
    mockOrderService.createOrder.and.returnValue(of(newOrder));

    component.saveOrder();
    expect(mockOrderService.createOrder).toHaveBeenCalled();
  });
});