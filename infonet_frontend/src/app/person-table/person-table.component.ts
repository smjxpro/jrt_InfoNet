import {Component, OnInit} from '@angular/core';
import {PersonService} from "../services/person.service";
import {Person} from "../models/person.model";
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../components/delete-dialog/delete-dialog.component";

@Component({
  selector: 'app-person-table',
  templateUrl: './person-table.component.html',
  styleUrls: ['./person-table.component.scss']
})
export class PersonTableComponent implements OnInit {
  data: Person[] = [];

  constructor(private personService: PersonService, public dialog: MatDialog) {
  }


  ngOnInit(): void {
    this.getPersons();
  }

  getPersons() {
    this.personService.getPersons().subscribe(res => {
      this.data = res.data ?? [];
    });
  }


  downloadFile(resume: string) {
    window.open(window.location.origin + resume);
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string, id: string): void {
    this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {id: id}
    });

    this.dialog.afterAllClosed.subscribe(res => {
      console.log(res);
      this.getPersons();
    });
  }
}
