import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {APIResponse} from "../models/api-response.model";
import {Person} from "../models/person.model";
import {BehaviorSubject, Observable, Subscription} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  constructor(private http: HttpClient) {
  }

  getPersons() {
    return this.http.get<APIResponse<Person[]>>('/api/person');
  }

  getPerson(id: string) {
    return this.http.get<APIResponse<Person>>(`/api/person/${id}`);
  }

  addPerson(person: any) {
    return this.http.post<APIResponse<Person>>('/api/person', person);
  }

  updatePerson(person: any) {
    return this.http.put<APIResponse<Person>>(`/api/person/${person.id}`, person);
  }

  deletePerson(id: string) {
    return this.http.delete<APIResponse<any>>(`/api/person/${id}`);
  }
}
