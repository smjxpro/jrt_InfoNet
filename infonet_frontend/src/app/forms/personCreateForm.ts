import {FormControl} from "@angular/forms";
import {Skill} from "../models/skill.model";

export interface PersonCreateForm {
  name: FormControl<string>;
  country: FormControl<string>;
  resumeLink: FormControl<string>;
  city: FormControl<string>;
  dateOfBirth: FormControl<Date>;
  skills: FormControl<Skill[]>;
}

export interface PersonEditForm {
  id: FormControl<string>;
  name: FormControl<string>;
  country: FormControl<string>;
  resumeLink: FormControl<string>;
  city: FormControl<string>;
  dateOfBirth: FormControl<Date>;
  skills: FormControl<Skill[]>;
}
