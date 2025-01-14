import { Component, Input } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CountriesComponent } from './countries.component';
import { PaymentsenseCodingChallengeApiService as CountryService} from '../services';
import { MockPaymentsenseCodingChallengeApiService as MockCountryService } from '../testing/mock-paymentsense-coding-challenge-api.service';
import { Country } from '../country';
import { MatExpansionModule } from '@angular/material';

@Component({ selector: 'app-countries-description', template: '' })
class CountriesDescriptionStubComponent {
}

@Component({ selector: 'app-pagination', template: '' })
class PaginationStubComponent {
  @Input()
  public totalPages: number;
}

@Component({ selector: 'app-country-detail', template: ''})
class CountryDetailStubComponent {
  @Input() country?: Country;
}

describe('CountriesComponent', () => {
  let component: CountriesComponent;
  let fixture: ComponentFixture<CountriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        CountriesComponent,
        CountriesDescriptionStubComponent,
        PaginationStubComponent,
        CountryDetailStubComponent,
       ],
       imports: [
         BrowserAnimationsModule,
         MatExpansionModule,
       ],
       providers: [
        { provide: CountryService, useClass: MockCountryService}
       ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display "Countries (5)" as headline', () => {
    // TODO: don't hardcode the number
    expect(fixture.nativeElement.querySelector('h2').textContent).toEqual('Countries (5)');
  });

  it('should display the country name', () => {
    // TODO: don't hardcode the first country name
    expect(fixture.debugElement.query(By.css('#country-0')).nativeElement.textContent).toContain('Afghanistan');
  });

  xit('should raise selected event when clicked', () => {

  });
});
