import {Skill} from "./skill.model";

export interface Person {
  id: string | undefined | null;
  name: string;
  country: string;
  city: string;
  dateOfBirth: Date;
  resumeLink: string;
  skills: Skill[];
}

