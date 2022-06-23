import { PaymentsenseCodingChallengeApiService as CountryService } from "./paymentsense-coding-challenge-api.service";
import { Country } from '../country';
import { HttpClient } from '@angular/common/http';
import { of } from "rxjs";
import { MOCKCOUNTRIES } from '../testing/mock-countries';

fdescribe('CountryService', () => {
  let countryService: CountryService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', [
      'get'
    ]);
    countryService = new CountryService(httpClientSpy);
  })

  it('can return health status (HttpClient called once)', (done: DoneFn) => {
    const expectedString: string = "Healthy";
    httpClientSpy.get.and.returnValue(of(expectedString));

    countryService.getHealth().subscribe({
      next: res => {
        expect(res).toEqual(expectedString);
        done();
      },
      error: done.fail
    })

    expect(httpClientSpy.get.calls.count()).toBe(1);
  })

  it('can return expected countries (HttpClient called once)', (done: DoneFn) => {
    httpClientSpy.get.and.returnValue(of(MOCKCOUNTRIES));

    countryService.getCountries().subscribe({
      next: res => {
        expect(res).toEqual(MOCKCOUNTRIES);
        done();
      },
      error: done.fail
    })

    expect(httpClientSpy.get.calls.count()).toBe(1);
  })
})