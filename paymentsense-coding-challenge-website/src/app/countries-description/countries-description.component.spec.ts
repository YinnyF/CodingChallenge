import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CountriesDescriptionComponent } from './countries-description.component';
import { MatCardModule } from '@angular/material';

describe('CountriesDescriptionComponent', () => {
  let component: CountriesDescriptionComponent;
  let fixture: ComponentFixture<CountriesDescriptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        CountriesDescriptionComponent
      ],
      imports: [
        MatCardModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountriesDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  it('should display countries description', () => {
    expect(fixture.nativeElement.querySelector('p').textContent)
      .toEqual('Districts and small settlements outside large urban areas or the capital.');
  });

  it('should display a link', () => {
    expect(fixture.nativeElement.querySelectorAll('a').length).toEqual(1);
  });
});
