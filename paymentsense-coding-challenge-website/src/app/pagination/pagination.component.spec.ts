import { Component, Input } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';

import { PaginationComponent } from './pagination.component';

@Component({
  selector: 'app-mock-host-component',
  template: `<app-pagination
    [totalPages$]="totalPages$"
    ></app-pagination>`
})
class MockHostComponent {
  public totalPages$: Observable<number> = new Observable<number>();
}

fdescribe('PaginationComponent', () => {
  // let component: PaginationComponent;
  // let fixture: ComponentFixture<PaginationComponent>;

  let mockHostComponent: MockHostComponent;
  let mockHostFixture: ComponentFixture<MockHostComponent>;
  // let eventEmitter: jasmine.SpyObj<EventEmitter<number>>;

  beforeEach(async(() => {
    // eventEmitter = jasmine.createSpyObj('EventEmitter', ['emit']);

    TestBed.configureTestingModule({
      declarations: [
        PaginationComponent,
        MockHostComponent,
      ]
    })
      .compileComponents();

    mockHostFixture = TestBed.createComponent(MockHostComponent);
    mockHostComponent = mockHostFixture.componentInstance;
    mockHostComponent.totalPages$ = of(25);
    mockHostFixture.detectChanges();
  }));


  it('should create', () => {
    expect(mockHostComponent).toBeTruthy();
  });

  it('should display the correct number of pages', () => {
    expect(mockHostFixture.nativeElement.querySelectorAll('a').length).toEqual(25);
  });

  // it('#onGoTo should emit current page number', () => {
  //   const page = 2;

  //   component.goTo.subscribe((res) => {
  //     expect(res).toBe(page);
  //   });

  //   component.onGoTo(page);
  // });

  // it('#onNext should emit current page number', () => {
  //   component.currentPage = 2;
  //   fixture.detectChanges();

  //   // TODO: test needs to wait - google: async tests in jasmine.
  //   component.next.subscribe((res) => {
  //     expect(res).toBe(component.currentPage);
  //   });

  //   component.onNext();
  // });

  // it('#onPrevious should emit current page number', () => {
  //   component.currentPage = 2;
  //   fixture.detectChanges();

  //   component.previous.subscribe((res) => {
  //     expect(res).toBe(component.currentPage);
  //   });

  //   component.onPrevious();
  // });


});
