import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, retryWhen, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { ContactMeans } from '../models/person.model';

@Injectable({
  providedIn: 'root'
})
export class ContactMeansService {
  private readonly apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getContactMeans(){
    return this.http.get<ApiResponse<ContactMeans[]>>(`${this.apiUrl}/ContactMeans`)
    .pipe(
      retryWhen(errors => errors.pipe(delay(100), take(3)))
    );
  }
}
