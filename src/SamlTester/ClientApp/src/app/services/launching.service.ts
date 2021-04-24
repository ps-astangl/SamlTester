import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import {Observable, of} from 'rxjs';
import {catchError, map, tap} from 'rxjs/operators';

import {LaunchSetting} from '../interfaces/LaunchSetting';


@Injectable({
  providedIn: 'root'
})
export class LaunchingService {
  private launchUrl = '/api/launch';

  constructor(private http: HttpClient) {
  }

  /** POST: Send Launch to Server */
  sendLaunch(launchConfig: LaunchSetting) {
    const httpOptions = {
      headers: {'Content-Type': 'application/json', 'Accept': 'application/text'},
      responseType: 'text'
    }
    const options = {
      headers: {'Content-Type': 'application/json', 'Accept': 'application/text'},
      responseType: "text",
      observe: "body"
    }

    return this.http.post(this.launchUrl, launchConfig,
      {
        headers: {
          'Content-Type': 'application/json', 'Accept': 'application/text'
        },
        responseType: "text",
        observe: "body"
      }
    ).toPromise()
      .then(resp => {
        return resp;
      })
      .catch(error => {
        console.error(error)
      });
  }

  /** Log a message with the MessageService */
  private log(message: string) {
    console.log(`LaunchService: ${message}`)
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
