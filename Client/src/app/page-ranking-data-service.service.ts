import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class PageRankingDataServiceService {

  constructor(private httpClient: HttpClient) { }

  getPageRankings(url: string, keyboard: string, searchEngineType: string): Observable<number[]> {
    var baseUrl = environment.pageRankUrl;

    if(url.startsWith('www')){
      url = 'http://' + url;
    }
    let httpParams = new HttpParams()
      .append("url", url)
      .append("SearchEngineType", searchEngineType)
      .append("TitleSearch", keyboard)

    return this.httpClient.get<number[]>(`${baseUrl}/api/v1/pagerank`, {
      params: httpParams
    })
      .pipe(
        tap(data => console.log('rankings: ', JSON.stringify(data))),
        // map(response => {
        //   positions: response
        // }),
        catchError(this.handleError)
      );
  }

  private handleError(err: any) {
    console.log('handleError', err);
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
