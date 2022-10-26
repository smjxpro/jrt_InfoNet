import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PersonCreateForm} from "../../forms/personCreateForm";
import {PersonService} from "../../services/person.service";
import {SkillService} from "../../services/skill.service";
import {Skill} from "../../models/skill.model";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {FileService} from "../../services/file.service";
import {LocationService} from "../../services/location.service";
import {ICity, ICountry} from "country-state-city";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  personCreateForm: FormGroup<PersonCreateForm>;

  file: File | null = null;


  skills: Skill[] = [];
  loading: boolean = false;
  countries: ICountry[] = [];
  cities: ICity[] = [];


  constructor(private personService: PersonService,
              private skillService: SkillService,
              private fileService: FileService,
              private fb: FormBuilder,
              private locationService: LocationService,
              private toastr: ToastrService,
              private router: Router) {

    this.personCreateForm = this.fb.group<PersonCreateForm>(<PersonCreateForm>{
      name: this.fb.nonNullable.control('', [Validators.required]),
      country: this.fb.nonNullable.control('', [Validators.required]),
      resumeLink: this.fb.nonNullable.control('', [Validators.required]),
      city: this.fb.nonNullable.control('', [Validators.required]),
      dateOfBirth: this.fb.nonNullable.control(new Date(), [Validators.required]),
      skills: this.fb.nonNullable.control([], [Validators.required]),
    });
  }


  ngOnInit(): void {
    this.getSkills();
    this.getCountries();
  }

  getCountries() {
    this.countries = this.locationService.getCountries();
  }

  getCities(countryCode: string) {
    this.cities = this.locationService.getCities(countryCode)!;
  }

  getSkills() {
    this.skillService.getSkills().subscribe(res => {
      this.skills = res.data ?? [];
    });
  }

  addPerson() {
    this.personService.addPerson(this.personCreateForm.value).subscribe(res => {
      if (res.success) {
        this.toastr.success('Person added successfully');
        this.router.navigate(['/']);
      } else {
        this.toastr.error('Something went wrong');
      }
    });
  }

  onSkill($event: MatCheckboxChange) {
    const skill = this.skills.find(s => s.id === $event.source.id);
    if ($event.checked) {
      this.personCreateForm.controls.skills.value.push(skill!);
    } else {
      this.personCreateForm.controls.skills.value.splice(this.personCreateForm.controls.skills.value.indexOf(skill!), 1);
    }
  }

  onFileChange(event: any) {
    this.file = event.target.files[0];
  }

  onUpload() {
    this.loading = !this.loading;

    if (this.file) {
      this.fileService.uploadFile(this.file!).subscribe(
        response => {
          try {
            this.personCreateForm.controls.resumeLink.setValue(response.data!);
            this.addPerson();
          } catch (e) {
            this.toastr.error('Something went wrong');
          }
        }
      );
    } else {
      this.toastr.error('Please select a file');
    }
  }

  onSubmit() {
    this.onUpload();
  }


  onCountryChange(country: any) {
    this.cities = [];
    const countryCode = this.countries.find(c => c.name === country)?.isoCode;

    if (countryCode) {
      this.getCities(countryCode);
    }
  }

}



