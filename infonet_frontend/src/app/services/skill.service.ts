import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {APIResponse} from "../models/api-response.model";
import {Skill} from "../models/skill.model";

@Injectable({
  providedIn: 'root'
})
export class SkillService {

  constructor(private http:HttpClient) { }

  getSkills() {
    return this.http.get<APIResponse<Skill[]>>('/api/skill');
  }
}
