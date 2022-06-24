import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Country } from '../country';
import { MOCKCOUNTRIES } from './mock-countries';

@Injectable({
  providedIn: 'root'
})
export class MockPaymentsenseCodingChallengeApiService {
  public getHealth(): Observable<string> {
    return of('Healthy');
  }

  public getCountries(): Observable<Country[]> {
    return of(MOCKCOUNTRIES);
  }
}
