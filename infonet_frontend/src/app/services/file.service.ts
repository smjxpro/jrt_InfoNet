import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {APIResponse} from "../models/api-response.model";

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http:HttpClient) { }

  uploadFile(file:File): Observable<APIResponse<string>>{
    const formData = new FormData();
    formData.append('file',file);
    return this.http.post<APIResponse<string>>('/api/file',formData);
  }
}
