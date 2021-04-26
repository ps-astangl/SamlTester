import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import {Observable, of} from 'rxjs';

import {SamlLaunchConfiguration} from '../interfaces/LaunchSetting';


@Injectable({
  providedIn: 'root'
})
export class LaunchingService {
  private launchUrl = '/api/launch';

  constructor(private http: HttpClient) {
  }

  /** POST: Send Launch to Server */
  sendLaunch(launchConfig: SamlLaunchConfiguration) {
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
      .then(resp => { return resp;})
      .catch(error => { this.handleError(error) });
  }

  /** Log a message with the MessageService */
  private static log(message: string) {
    console.log(`LaunchService: ${message}`)
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead

      LaunchingService.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}
