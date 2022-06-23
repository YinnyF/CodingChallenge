import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Country } from '../country';

@Injectable({
  providedIn: 'root'
})
export class MockPaymentsenseCodingChallengeApiService {
  public getHealth(): Observable<string> {
    return of('Healthy');
  }

  public getCountries(): Observable<Country[]> {
    return of(
      [
        { name: "United Kingdom of Great Britain and Northern Ireland", alpha2Code: "GB" },
        { name: "Peru", alpha2Code: "PE" },
        { name: "United States of America", alpha2Code: "US"},
      ] as Country[]
    );
  }
}
