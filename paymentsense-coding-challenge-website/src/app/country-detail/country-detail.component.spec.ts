import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';

import { CountryDetailComponent } from './country-detail.component';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Component, Input } from '@angular/core';
import { Country, Currency, Language } from '../country';

const getCountry = () => <Country> {
  name: 'Peru',
  alpha2Code: 'PE',
  capital: 'Lima',
  population: '32971846' as unknown as bigint,
  numericCode: 5,
  currencies: [{ code: '', name: 'Peruvian sol', symbol: 'S/.' }] as Currency[],
  languages: [{ name: 'Spanish' }] as Language[],
  flag: 'https://flagcdn.com/pe.svg',
};
@Component({
  selector: 'app-mock-host-component',
  template: `<app-country-detail [country]="country"></app-country-detail>`
})
class MockHostComponent {
  @Input()
  public country: Country;

  // @Output() close = new EventEmitter<boolean>();
}

describe('CountryDetailComponent', () => {
  let mockHostComponent: MockHostComponent;
  let mockHostFixture: ComponentFixture<MockHostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        CountryDetailComponent,
        MockHostComponent,
      ],
      imports: [
        MatCardModule,
        MatIconModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    mockHostFixture = TestBed.createComponent(MockHostComponent);
    mockHostComponent = mockHostFixture.componentInstance;
    mockHostComponent.country = getCountry();
    mockHostFixture.detectChanges();
  });

  // it('mock host component should be created', () => {
  //   expect(mockHostComponent).toBeTruthy();
  // });

  it('should display the country name', () => {
    const countryDe = mockHostFixture.debugElement.query(By.css('#countryName'));
    const countryEl = countryDe.nativeElement;

    expect(countryEl.textContent).toBe(mockHostComponent.country.name);
  });

  xit('#closeCountryDetail should emit close event', () => {
  });

  it('should display the flag', () => {
    const actual = mockHostFixture.debugElement.nativeElement.querySelector('img').src;

    expect(actual).toContain('https://flagcdn.com/pe.svg');
  });

  it('should display the capital', () => {
    const countryDe = mockHostFixture.debugElement.query(By.css('#capital'));
    const countryEl = countryDe.nativeElement;

    expect(countryEl.textContent).toBe(mockHostComponent.country.capital);
  });

  it('should display the population', () => {
    const countryDe = mockHostFixture.debugElement.query(By.css('#population'));
    const countryEl = countryDe.nativeElement;

    expect(countryEl.textContent).toBe('32,971,846');
  });

  it('should display a language', () => {
    const countryDe = mockHostFixture.debugElement.query(By.css('#language-0'));
    const countryEl = countryDe.nativeElement;

    const expectedLanguage = mockHostComponent.country.languages[0];

    expect(countryEl.textContent).toBe(expectedLanguage.name);
  });

  it('should display a currency', () => {
    const countryDe = mockHostFixture.debugElement.query(By.css('#currency-0'));
    const countryEl = countryDe.nativeElement;

    const expectedCurrency = mockHostComponent.country.currencies[0];
    const expected = expectedCurrency.name + ' (' + expectedCurrency.symbol + ')';

    expect(countryEl.textContent).toBe(expected);
  });
});
