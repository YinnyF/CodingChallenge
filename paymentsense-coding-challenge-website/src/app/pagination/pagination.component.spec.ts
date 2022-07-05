import { Component, EventEmitter, Input, Output } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Observable, of } from 'rxjs';
import { first } from 'rxjs/operators';

import { PaginationComponent } from './pagination.component';

@Component({
  selector: 'app-mock-host-component',
  template: `<app-pagination
    [totalPages]="totalPages"
    ></app-pagination>`
})
class MockHostComponent {
  @Input() totalPages: number;
}

describe('PaginationComponent', () => {
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
    mockHostComponent.totalPages = 25;
    mockHostFixture.detectChanges();
  }));

  it('should create', () => {
    expect(mockHostComponent).toBeTruthy();
  });

  xit('should display the correct number of pages', () => {
    // TODO: figure out why pages links are not showing in the test runner
    // console.log(mockHostComponent.totalPages);
    // setTimeout(() => {console.log('first')}, 1000);
    // expect(mockHostFixture.nativeElement.querySelectorAll('a').length).toEqual(25);
  });

  xit('should emit page number', () => {
    // const expectedPage = 5;

    // mockHostComponent.goTo
    //   .pipe(first())
    //   .subscribe(page =>
    //     expect(page).toBe(expectedPage)
    //   )
    //   .unsubscribe;

    // mockHostComponent.onGoTo(expectedPage);

  });

});
