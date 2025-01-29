import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductComponent } from './product.component';
import { ProductService } from './../../shared/service/product.service';
import { of } from 'rxjs';

describe('ProductComponent', () => {
  let component: ProductComponent;
  let fixture: ComponentFixture<ProductComponent>;
  let mockProductService: jasmine.SpyObj<ProductService>;

  beforeEach(async () => {
    mockProductService = jasmine.createSpyObj('ProductService', ['getProducts', 'createProduct']);

    await TestBed.configureTestingModule({
      declarations: [ProductComponent],
      providers: [{ provide: ProductService, useValue: mockProductService }],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load products on init', () => {
    const mockProducts = [{ id: '1', name: 'Laptop', price: 2000 }];
    mockProductService.getProducts.and.returnValue(of(mockProducts));

    component.ngOnInit();
    expect(component.products).toEqual(mockProducts);
  });

  it('should save a product', () => {
    const newProduct = { id: '', name: 'Mouse', price: 50 };
    mockProductService.createProduct.and.returnValue(of(newProduct));

    component.saveProduct();
    expect(mockProductService.createProduct).toHaveBeenCalled();
  });
});