import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { map, take, tap } from 'rxjs/operators';
import { Country } from '../country';
import { PaymentsenseCodingChallengeApiService as CountryService } from '../services';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit, OnDestroy {
  
  countriesSubscription: Subscription;
  countries: Country[] = [];
  numberOfCountries: number;

  selectedCountry?: Country;
  countriesToDisplay: Country[] = [];

  pageSize = 10;
  totalPages: number;
  
  // selectedCountryId?: number;

  constructor(
    private countryService: CountryService
  ) { }

  ngOnInit(): void {
    this.getCountries();
  }

  ngOnDestroy(): void {
    this.countriesSubscription.unsubscribe();
  }

  onSelect(country: Country): void {
    this.selectedCountry = country;
  }

  getCountries(): void {
    this.countriesSubscription = this.countryService.getCountries().pipe(
      take(1),
      tap(countries => {
        this.countries = countries;
        this.updateCountriesToDisplay(1);
      }),
      map(countries => {
        this.numberOfCountries = countries.length;
        return Math.ceil(this.numberOfCountries / this.pageSize);
      }),
      tap(num => this.totalPages = num)
    ).subscribe();
  }

  onGoTo(page: number): void {
    this.updateCountriesToDisplay(page);
  }

  updateCountriesToDisplay(currentPage: number): void {
    const pageStart = (currentPage - 1) * this.pageSize;
    this.countriesToDisplay = this.countries.slice(pageStart, pageStart + this.pageSize);
  }

  unselectCountry() {
    this.selectedCountry = null;
  }
}
