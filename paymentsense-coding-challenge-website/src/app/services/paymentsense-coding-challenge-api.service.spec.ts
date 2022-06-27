import { PaymentsenseCodingChallengeApiService as CountryService } from './paymentsense-coding-challenge-api.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { MOCKCOUNTRIES } from '../testing/mock-countries';

describe('CountryService', () => {
  let countryService: CountryService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;
  // https://angular.io/guide/testing-services#testing-http-services

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', [
      'get'
    ]);
    countryService = new CountryService(httpClientSpy);
  });

  it('can return health status (HttpClient called once)', (done: DoneFn) => {
    const expectedString = 'Healthy';
    httpClientSpy.get.and.returnValue(of(expectedString));

    countryService.getHealth().subscribe({
      next: res => {
        expect(res).toEqual(expectedString);
        done();
      },
      error: done.fail
    });

    expect(httpClientSpy.get.calls.count()).toBe(1);
  });

  it('can return expected countries (HttpClient called once)', (done: DoneFn) => {
    httpClientSpy.get.and.returnValue(of(MOCKCOUNTRIES));

    countryService.getCountries().subscribe({
      next: countries => {
        expect(countries).toEqual(MOCKCOUNTRIES);
        done();
      },
      error: done.fail
    });

    expect(httpClientSpy.get.calls.count()).toBe(1);
  });

  it('should return an error when the server returns a 404', (done: DoneFn) => {

    const errorResponse = new HttpErrorResponse({
      error: 'test 404 error',
      status: 404,
      statusText: 'Not Found'
    });

    // TODO: https://stackoverflow.com/a/59325387
    httpClientSpy.get.and.returnValue(throwError(errorResponse));

    countryService.getCountries().subscribe({
      next: countries => done.fail('expected an error, not countries'),
      error: error  => {
        expect(error.message).toContain(errorResponse.message);
        done();
      }
    });

  });
});
