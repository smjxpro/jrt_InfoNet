import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PersonEditForm} from "../../forms/personCreateForm";
import {Skill} from "../../models/skill.model";
import {ICity, ICountry} from "country-state-city";
import {PersonService} from "../../services/person.service";
import {SkillService} from "../../services/skill.service";
import {FileService} from "../../services/file.service";
import {LocationService} from "../../services/location.service";
import {ToastrService} from "ngx-toastr";
import {ActivatedRoute, Router} from "@angular/router";
import {MatCheckboxChange} from "@angular/material/checkbox";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent {
  personEditForm: FormGroup<PersonEditForm>;

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
              private router: Router,
              private route: ActivatedRoute) {

    this.personEditForm = this.fb.group<PersonEditForm>(<PersonEditForm>{
      id: this.fb.nonNullable.control('', [Validators.required]),
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

    this.route.params.subscribe(params => {
      this.getPerson(params['id']);
    });
  }

  getPerson(id: string) {
    this.personService.getPerson(id).subscribe(res => {
      if (res.success) {
        this.personEditForm = this.fb.group<PersonEditForm>(<PersonEditForm>{
          id: this.fb.nonNullable.control(res.data!.id, [Validators.required]),
          name: this.fb.control(res.data!.name, [Validators.required]),
          country: this.fb.control(res.data!.country, [Validators.required]),
          resumeLink: this.fb.control(res.data!.resumeLink, [Validators.required]),
          city: this.fb.control(res.data!.city, [Validators.required]),
          dateOfBirth: this.fb.control(res.data!.dateOfBirth, [Validators.required]),
          skills: this.fb.control(res.data!.skills, [Validators.required]),
        });
      } else {
        this.toastr.error('Something went wrong');
      }
    });
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

  editPerson() {
    console.log(this.personEditForm.value);
    this.personService.updatePerson(this.personEditForm.value).subscribe(res => {
      if (res.success) {
        this.toastr.success('Person edit successfully');
        this.router.navigate(['/']);
      } else {
        this.toastr.error('Something went wrong');
      }
    });
  }

  onSkill($event: MatCheckboxChange) {
    const skill = this.skills.find(s => s.id === $event.source.id);
    if ($event.checked) {
      this.personEditForm.controls.skills.value.push(skill!);
    } else {
      this.personEditForm.controls.skills.value.splice(this.personEditForm.controls.skills.value.indexOf(skill!), 1);
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

          this.personEditForm.controls.resumeLink.setValue(response.data!);

        }
      );
    }
    this.editPerson();

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

  skillExists(id: string): boolean {
    return this.personEditForm.controls.skills.value.some((s: Skill) => s.id === id);
  }

}
