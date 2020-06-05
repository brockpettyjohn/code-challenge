import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Account } from './Account';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll() {
    return this.http.get<Account[]>(this.baseUrl + 'weatherforecast').pipe(
      tap(r => this.checkForErrorInResponse(r)),
      catchError(this.handleError<Account[]>('getAccount'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }

  checkForErrorInResponse(response): void {
    console.log('response: ', response);
    var errorMessage = response && response['errorMessage'] || null;
    throw new Error(errorMessage);
  }


  createAccount() {
    this.http.post<Account[]>(this.baseUrl + 'weatherforecast', {}).subscribe(result => console.log(result));
  }

  getHttpOptions(args: { [param: string]: string | string[]; } = undefined) {
    return {
      params: this.filterObject(args, (k, v) => v !== undefined),
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
  }

  filterObject(obj: any, filter: (key: string, value: any) => boolean) {
    return obj === null || obj === undefined ? obj
      : Object.entries(obj)
        .filter(e => filter(e[0], e[1]))
        .reduce((o, v) => { o[v[0]] = v[1]; return o; }, {});
  }
}
