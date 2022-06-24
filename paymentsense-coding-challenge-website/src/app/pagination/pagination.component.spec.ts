import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaginationComponent } from './pagination.component';
import { SimpleChange } from '@angular/core';

// @Component({
//   selector: 'mock-host-component',
//   template: `<app-pagination 
//     [currentPage]="'1'"
//     [totalPages]="'5'"
//     (goTo)="onGoTo($event)"
//     (next)="onNext($event)"
//     (previous)="onPrevious($event)"
//     ></app-pagination>`
// })
// class MockHostComponent {
//   @Input()
//   public currentPage: number;

//   @Input()
//   public totalPages: number;

//   goTo() {}

// }

describe('PaginationComponent', () => {
  let component: PaginationComponent;
  let fixture: ComponentFixture<PaginationComponent>;

  // let mockHostComponent: MockHostComponent;
  // let mockHostFixture: ComponentFixture<MockHostComponent>;
  // let eventEmitter: jasmine.SpyObj<EventEmitter<number>>;

  beforeEach(async(() => {
    // eventEmitter = jasmine.createSpyObj('EventEmitter', ['emit']);


    TestBed.configureTestingModule({
      declarations: [
        PaginationComponent,
        // MockHostComponent,
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaginationComponent);
    component = fixture.componentInstance;

    // set input properties
    component.currentPage = 1;
    component.totalPages = 5;
    // set pages
    component.pages = [1, 2, 3, 4, 5];

    // mockHostFixture = TestBed.createComponent(MockHostComponent);
    // mockHostComponent = mockHostFixture.componentInstance;
    // mockHostComponent.totalPages = 5;
  });

  it('should create', () => {
    fixture.detectChanges();

    expect(component).toBeTruthy();
  });

  it('#onGoTo should emit current page number', () => {
    fixture.detectChanges();

    let page = 2;

    component.goTo.subscribe((res) => {
      expect(res).toBe(page);
    })

    component.onGoTo(page);
  })

  it('#onNext should emit current page number', () => {
    component.currentPage = 2;
    fixture.detectChanges();

    component.next.subscribe((res) => {
      expect(res).toBe(component.currentPage);
    })

    component.onNext();
  })

  it('#onPrevious should emit current page number', () => {
    component.currentPage = 2;
    fixture.detectChanges();

    component.previous.subscribe((res) => {
      expect(res).toBe(component.currentPage);
    })

    component.onPrevious();
  })

  it('should display the correct number of pages', () => {
    fixture.detectChanges();

    expect(fixture.nativeElement.querySelectorAll('a').length).toEqual(component.totalPages);
  })

  it('should render correct pages when totalPages changes', () => {
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelectorAll('a').length).toBe(component.totalPages);
    let prevTotalPages = component.totalPages;
    let currentTotalPages = 20;

    component.totalPages = currentTotalPages;
    // trigger ngOnChanges
    component.ngOnChanges({
      totalPages: new SimpleChange(prevTotalPages, currentTotalPages, false),
    });
    
    fixture.detectChanges();
    
    expect(fixture.nativeElement.querySelectorAll('a').length).toBe(currentTotalPages);
  })
});
