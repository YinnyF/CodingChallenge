import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Country } from '../country';

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {

  private baseUrl = 'https://localhost:65129';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private httpClient: HttpClient
    ) {}

  public getHealth(): Observable<string> {
    return this.httpClient.get(this.baseUrl + '/health', { responseType: 'text' });
  }

  public getCountries(): Observable<Country[]> {
    return this.httpClient.get<Country[]>(this.baseUrl + '/api/PaymentsenseCodingChallenge');
  }

}
