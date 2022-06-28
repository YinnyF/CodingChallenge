import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map, take, tap } from 'rxjs/operators';
import { Country } from '../country';
import { PaymentsenseCodingChallengeApiService as CountryService } from '../services';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {

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

  onSelect(country: Country): void {
    this.selectedCountry = country;
  }

  getCountries(): void {
    this.countryService.getCountries().pipe(
      take(1),
      tap(countries => {
        this.countries = countries;
        this.updateCountriesToDisplay(1);
      }),
      map(countries => {
        this.numberOfCountries = countries.length;
        return Math.ceil(this.numberOfCountries / this.pageSize);
      }))
      .subscribe(num => {
        this.totalPages = num;
      });
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
