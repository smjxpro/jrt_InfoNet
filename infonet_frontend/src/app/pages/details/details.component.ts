import {Component, OnInit} from '@angular/core';
import {PersonService} from "../../services/person.service";
import {Person} from "../../models/person.model";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  person: Person | null | undefined;

  constructor(private personService: PersonService, private route: ActivatedRoute) {

  }


  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.getPerson(params['id']);
    });
  }

  getPerson(id: string) {
    this.personService.getPerson(id).subscribe(response => {
      this.person = response.data;
    });
  }

}
