import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Component, Input } from '@angular/core';
import { By } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { LayoutModule } from '@angular/cdk/layout';

import { MainNavComponent } from './main-nav.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'mock-host-component',
  template: `<app-main-nav [title]='title'></app-main-nav>`
})
class MockHostComponent {
  @Input()
  public title: string;
}

describe('MainNavComponent', () => {
  let mockHostComponent: MockHostComponent;
  let mockHostFixture: ComponentFixture<MockHostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        MainNavComponent,
        MockHostComponent
      ],
      imports: [
        NoopAnimationsModule,
        LayoutModule,
        MatSidenavModule,
        MatToolbarModule,
        MatListModule,
        MatIconModule,
        MatButtonModule,
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    mockHostFixture = TestBed.createComponent(MockHostComponent);
    mockHostComponent = mockHostFixture.componentInstance;
    mockHostComponent.title = "Fake Title";
    mockHostFixture.detectChanges();
  });

  it('should have a title', () => {
    let titleDe = mockHostFixture.debugElement.query(By.css('#nav-title'));
    let titleEl = titleDe.nativeElement;
    expect(titleEl.textContent).toEqual(mockHostComponent.title);
  })

  it('should display 2 links', () => {
    let navDe = mockHostFixture.debugElement;
    let navEl = navDe.nativeElement;

    expect(navEl.querySelectorAll('a').length).toEqual(2);
  })

});
