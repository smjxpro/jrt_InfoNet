import {Component, EventEmitter, Inject, Input, OnInit, Output} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {PersonService} from "../../services/person.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.scss']
})
export class DeleteDialogComponent implements OnInit {
  id: string | undefined;


  constructor(public dialogRef: MatDialogRef<DeleteDialogComponent>, private personService: PersonService, @Inject(MAT_DIALOG_DATA) public data: any,private toastr:ToastrService) {

  }


  ngOnInit(): void {
    this.id = this.data.id;
  }

  delete(id: string) {
    this.personService.deletePerson(id).subscribe(res => {
      if (res.success) {
        this.toastr.success('Person deleted successfully');
        this.dialogRef.close();
      }else {
        this.toastr.error('Something went wrong');
        this.dialogRef.close();
      }
    });
  }
}


