import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CountriesDescriptionComponent } from './countries-description.component';

describe('CountriesDescriptionComponent', () => {
  let component: CountriesDescriptionComponent;
  let fixture: ComponentFixture<CountriesDescriptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountriesDescriptionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountriesDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
