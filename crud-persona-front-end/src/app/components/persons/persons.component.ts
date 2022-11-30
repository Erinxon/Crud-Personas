import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Person } from 'src/app/models/person.model';
import { PersonService } from 'src/app/services/person.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { PersonComponent } from './person/person.component';

@Component({
  selector: 'app-persons',
  templateUrl: './persons.component.html',
  styleUrls: ['./persons.component.css']
})
export class PersonsComponent implements OnInit {
  persons: Person[] = [];
  currentPage: number = 1;

  constructor(private dialog: MatDialog, private personService: PersonService, private snackBar: SnackBarService){
  }

  ngOnInit(): void {
    this.getPersons();
  }

  getPersons(){
    this.personService.getPersons().subscribe({
      next: res => {
        this.persons = res.data;
      }
    })
  }

  openModalPerson(person: Person = null!, add: boolean = true, edit: boolean = false, view: boolean = false){
    this.dialog.open(PersonComponent, {
      width: '50%',
      data: {
        item: person,
        add: add,
        edit: edit,
        view: view
      }
    }).afterClosed().subscribe({
      next: value => {
        if(value?.change){
          this.getPersons();
        }
      }
    })
  }

  deletePerson(person: Person){
    this.dialog.open(ConfirmDialogComponent).afterClosed().subscribe({
      next: value => {
        if(value?.confirm){
          this.personService.deletePerson(person?.personId).subscribe({
            next: res => {
              this.getPersons();
              this.snackBar.success('Persona elimina correctamente');
            },
            error: error => {
              this.snackBar.error('Ocurrió un error al eliminar la persona');
            }
          })
        }
      }
    })
  }

}