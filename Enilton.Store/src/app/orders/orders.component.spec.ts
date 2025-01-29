import { ComponentFixture, TestBed } from '@angular/core/testing';
import { OrdersComponent } from './orders.component';
import { OrderService } from './../../shared/service/order.service';
import { of } from 'rxjs';

describe('OrdersComponent', () => {
  let component: OrdersComponent;
  let fixture: ComponentFixture<OrdersComponent>;
  let mockOrderService: jasmine.SpyObj<OrderService>;

  beforeEach(async () => {
    mockOrderService = jasmine.createSpyObj('OrderService', ['getOrders', 'deleteOrder']);

    await TestBed.configureTestingModule({
      declarations: [OrdersComponent],
      providers: [{ provide: OrderService, useValue: mockOrderService }],
    }).compileComponents();

    fixture = TestBed.createComponent(OrdersComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load orders on init', () => {
    const mockOrders = [{ id: '1', customerId: '2', orderDate: new Date(), totalAmount: 500, status: 1 }];
    mockOrderService.getOrders.and.returnValue(of(mockOrders));

    component.ngOnInit();
    expect(component.orders).toEqual(mockOrders);
  });

  it('should delete an order', () => {
    const orderId = '1';
    mockOrderService.deleteOrder.and.returnValue(of(null));

    component.deleteOrder(orderId);
    expect(mockOrderService.deleteOrder).toHaveBeenCalledWith(orderId);
  });
});