import { Component, OnInit } from '@angular/core';
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
  currentPage = 1;
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
    this.countryService.getCountries().subscribe(countries => {
      this.countries = countries;
      this.numberOfCountries = countries.length;
      this.totalPages = Math.ceil(this.numberOfCountries / this.pageSize);
      this.countriesToDisplay = this.getCountriesToDisplay();
    });
  }

  onGoTo(page: number): void {
    this.currentPage = page;
    // console.log(this.currentPage)
    this.countriesToDisplay = this.getCountriesToDisplay();
  }

  onNext(page: number): void {
    this.currentPage = page + 1;
    // console.log(this.currentPage)
    this.countriesToDisplay = this.getCountriesToDisplay();
  }

  onPrevious(page: number): void {
    this.currentPage = page - 1;
    // console.log(this.currentPage)
    this.countriesToDisplay = this.getCountriesToDisplay();
  }

  getCountriesToDisplay(): Country[] {
    const pageStart = (this.currentPage - 1) * this.pageSize;
    return this.countries.slice(pageStart, pageStart + this.pageSize);
  }

  unselectCountry() {
    this.selectedCountry = null;
  }
}
