import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';

import { CountryDetailComponent } from './country-detail.component';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Component, Input } from '@angular/core';
import { Country, Currency, Language } from '../country';
import { componentFactoryName } from '@angular/compiler';

const getCountry = () => <Country>{
  name: "Peru",
  alpha2Code: "PE",
  capital: "Lima",
  population: "32971846" as unknown as bigint,
  numericCode: 5,
  currencies: [{ code: "",name: "Peruvian sol", symbol: "S/." }] as Currency[],
  languages: [{ name: "Spanish" }] as Language[],
  flag: "https://flagcdn.com/pe.svg",
}
@Component({
  selector: 'mock-host-component',
  template: `<app-country-detail [country]="country"></app-country-detail>`
})
class MockHostComponent {
  @Input()
  public country: Country;
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
    let countryDe = mockHostFixture.debugElement.query(By.css('#countryName'));
    let countryEl = countryDe.nativeElement;

    expect(countryEl.textContent).toBe(mockHostComponent.country.name);
  });

  xit('#closeCountryDetail should emit onClose event', () => {
    
  })

  it('should display the flag', () => {
    let actual = mockHostFixture.debugElement.nativeElement.querySelector('img').src;

    expect(actual).toContain("https://flagcdn.com/pe.svg");
  })

  it('should display the capital', () => {
    let countryDe = mockHostFixture.debugElement.query(By.css('#capital'));
    let countryEl = countryDe.nativeElement;

    expect(countryEl.textContent).toBe(mockHostComponent.country.capital);
  });

  it('should display the population', () => {
    let countryDe = mockHostFixture.debugElement.query(By.css('#population'));
    let countryEl = countryDe.nativeElement;

    expect(countryEl.textContent).toBe("32,971,846");
  });

  it('should display a language', () => {
    let countryDe = mockHostFixture.debugElement.query(By.css('#language-0'));
    let countryEl = countryDe.nativeElement;

    let expectedLanguage = mockHostComponent.country.languages[0];

    expect(countryEl.textContent).toBe(expectedLanguage.name);
  });

  it('should display a currency', () => {
    let countryDe = mockHostFixture.debugElement.query(By.css('#currency-0'));
    let countryEl = countryDe.nativeElement;

    let expectedCurrency = mockHostComponent.country.currencies[0];
    let expected = expectedCurrency.name + " (" + expectedCurrency.symbol + ")"; 

    expect(countryEl.textContent).toBe(expected);
  });
});
