import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, retryWhen, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class ContactMeansPeopleService {
  private readonly apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  deleteContactMeansPeople(contactMeansPeopleId: number){
    return this.http.delete<ApiResponse<number>>(`${this.apiUrl}/ContactMeansPerson?contactMeansPeopleId=${contactMeansPeopleId}`)
    .pipe(
      retryWhen(errors => errors.pipe(delay(100), take(3)))
    );
  }
}
