import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Person } from '../models/person.model';
import { ApiResponse } from '../models/api-response.model';
import { delay, retryWhen, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private readonly apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getPersons(){
    return this.http.get<ApiResponse<Person[]>>(`${this.apiUrl}/Persons`)
    .pipe(
      retryWhen(errors => errors.pipe(delay(100), take(3)))
    );
  }

  getPersonById(personId: number){
    return this.http.get<ApiResponse<Person>>(`${this.apiUrl}/Persons/${personId}`)
    .pipe(
      retryWhen(errors => errors.pipe(delay(100), take(3)))
    );
  }

  postPerson(person: Person){
    return this.http.post<ApiResponse<number>>(`${this.apiUrl}/Persons`, person)
    .pipe(
      retryWhen(errors => errors.pipe(delay(100), take(3)))
    );
  }

  updatePerson(person: Person){
    return this.http.put<ApiResponse<number>>(`${this.apiUrl}/Persons`, person)
    .pipe(
      retryWhen(errors => errors.pipe(delay(100), take(3)))
    );
  }

  deletePerson(personId: number){
    return this.http.delete<ApiResponse<number>>(`${this.apiUrl}/Persons/?PersonId=${personId}`)
    .pipe(
      retryWhen(errors => errors.pipe(delay(100), take(3)))
    );
  }

}