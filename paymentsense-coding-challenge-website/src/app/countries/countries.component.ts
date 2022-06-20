import { Component, OnInit } from '@angular/core';
import { Country } from '../country';
import { PaymentsenseCodingChallengeApiService as CountryService } from '../services';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {

  countries : Country[] = []

  selectedCountry?: Country;

  // selectedCountryId?: number;

  constructor(
    private countryService: CountryService
  ) { }

  ngOnInit(): void {
    this.getCountries();
  }

  onSelect(country: Country, i: number): void {
    console.log(country.name);
    this.selectedCountry = country;
    // this.selectedCountryId = i;
  }

  getCountries(): void {
    // TODO: replace slice with pagination
    this.countryService.getCountries().subscribe(countries => this.countries = countries.slice(1, 5));
  }

}
