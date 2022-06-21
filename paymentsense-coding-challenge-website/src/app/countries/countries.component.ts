import { Component, OnInit } from '@angular/core';
import { Country } from '../country';
import { PaymentsenseCodingChallengeApiService as CountryService } from '../services';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {

  countries : Country[] = [];
  numberOfCountries: number;

  selectedCountry?: Country;
  countriesToDisplay: Country[] = [];
  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number;

  // selectedCountryId?: number;

  constructor(
    private countryService: CountryService
  ) { }

  ngOnInit(): void {
    this.getCountries();
    this.countriesToDisplay = this.paginate(this.currentPage, this.pageSize)
  }

  onSelect(country: Country, i: number): void {
    console.log(country.name);
    this.selectedCountry = country;
    // this.selectedCountryId = i;
  }

  getCountries(): void {
    // TODO: replace slice with pagination
    this.countryService.getCountries().subscribe(countries => {
      this.countries = countries/*.slice(0, 5)*/;
      this.numberOfCountries = countries.length;
      this.totalPages = Math.ceil(this.numberOfCountries / this.pageSize);
    })
  }

  onGoTo(page: number): void {
    this.currentPage = page
    this.countriesToDisplay = this.paginate(this.currentPage, this.pageSize)
  }

  onNext(page: number): void {
    this.currentPage = page + 1;
    this.countriesToDisplay = this.paginate(this.currentPage, this.pageSize)
  }

  onPrevious(page: number): void {
    this.currentPage = page - 1;
    this.countriesToDisplay = this.paginate(this.currentPage, this.pageSize)
  }

  public paginate(currentPage: number, pageSize: number): Country[] {
    return this.countries.slice((currentPage - 1) * pageSize, pageSize)
  }

  unselectCountry() {
    this.selectedCountry = null;
  }
}
