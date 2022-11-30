import { formatDate } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ArgumentsModal } from 'src/app/models/arguments.modal.model';
import { ContactMeans, ContactMeansPeople, Person } from 'src/app/models/person.model';
import { ContactMeansPeopleService } from 'src/app/services/contact-means-people.service';
import { PersonService } from 'src/app/services/person.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { ContactMeanComponent } from './contact-mean/contact-mean.component';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.css']
})
export class PersonComponent implements OnInit {
  form!: FormGroup;
  formInvalid: boolean = false;

  constructor(public dialogRef: MatDialogRef<PersonComponent>,
    private fb: FormBuilder, private personService: PersonService,
    private contactMeansPeopleService: ContactMeansPeopleService,
    private dialog: MatDialog, private snackBarService: SnackBarService,
    @Inject(MAT_DIALOG_DATA) public data: ArgumentsModal<Person>) {
    this.dialogRef.disableClose = true; 
    this.buildForm();
    if(this.data?.item){
      this.form.patchValue({
        ...this.data?.item,
        birth: formatDate(this.data?.item.birth, 'yyyy-MM-dd', 'en')
      });
    }
    if(this.data.view){
      this.form.disable();
    }
  }

  ngOnInit(): void {
  }

  buildForm() {
    this.form = this.fb.group({
      personId: [0],
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      identification: ['', [Validators.required, Validators.maxLength(11)]],
      birth: [null, [Validators.required]],
      contactMeansPeople: [[], [Validators.required]],
    });
  }

  close() {
    this.dialogRef.close(null);
  }

  save() {
    this.formInvalid = this.form?.invalid;
    if(this.formInvalid) return;
    const person: Person = {...this.form?.value};
    person.contactMeansPeople = person?.contactMeansPeople?.map(c => {
      return {
        id: c?.id,
        contact: c?.contact,
        contactMeansId: c?.contactMeansId,
        personId: c?.personId
      }
    });
    if(!person?.personId){
      this.postPerson(person);
    }else{
      this.updatePerson(person)
    }
  }

  updatePerson(person: Person){
    this.personService.updatePerson(person).subscribe({
      next: res => {
        if(res.succeeded){
          this.dialogRef.close({
            change: true
          });
          this.snackBarService.success('Persona editada correctamente');
        }
      }, error: error => {
        this.snackBarService.error('Ocurrió un error editando la persona');
      }
    });
  }

  postPerson(person: Person){
    this.personService.postPerson(person).subscribe({
      next: res => {
        if(res.succeeded){
          this.dialogRef.close({
            change: true
          });
          this.snackBarService.success('Persona creada correctamente');
        }
      }, error: error => {
        this.snackBarService.error('Ocurrió un error creando la persona');
      }
    });
  }

  getError(name: string) {
    if (!this.formInvalid) {
      return '';
    }
    const field = this.form.get(name)
    if (field?.valid) return;
    if (field?.errors!['required']) {
      return 'Este campo es obligatorio';
    }
    if (field?.errors!['maxlength']) {
      return `Este campo sólo admite un máximo de ${field?.errors['maxlength']?.requiredLength} caracteres`;
    }
    return '';
  }

  openContactMean(contactMeansPeople: ContactMeansPeople = null!, add: boolean = true, edit: boolean = false, view: boolean = false){
    this.dialog.open(ContactMeanComponent, {
      width: '50%',
      data: {
        item: contactMeansPeople,
        itemList: this.contactMeans,
        add: add,
        edit: edit,
        view: view
      }
    }).afterClosed().subscribe({
      next: value => {
        if(value?.contactMeansPerson){
          const contactMeansPerson: ContactMeansPeople = value?.contactMeansPerson;
          let contactMeansPersons: ContactMeansPeople[] =  this.form?.get('contactMeansPeople')?.value ?? [];
          if(contactMeansPerson?.id > 0){
            contactMeansPersons = contactMeansPersons.map(contact => {
              if(contact.id === contactMeansPerson.id){
                contact = contactMeansPerson
              }
              return contact;
            })
          }else{
            contactMeansPersons = [...contactMeansPersons, contactMeansPerson];
          }
          this.form.get('contactMeansPeople')?.setValue(contactMeansPersons);
        }
      }
    });
  }

  get contactMeans(){
    return this.form?.value?.contactMeansPeople as ContactMeansPeople[];
  }

  deleteContactMeansPeople(contactMeansPeople: ContactMeansPeople, index: number){
    if(contactMeansPeople?.id === 0){
      this.deleteByIndex(index);
      return;
    }
    this.dialog.open(ConfirmDialogComponent).afterClosed().subscribe({
      next: value => {
        if(value?.confirm){
          this.contactMeansPeopleService.deleteContactMeansPeople(contactMeansPeople?.id).subscribe({
            next: res => {
              if(res?.succeeded){
                this.deleteByIndex(index);
                this.snackBarService.success('Registro eliminado correctamente');
              }
            }, error: error => {
              this.snackBarService.error('Ocurrió un error eliminando el registro');
            }
          })
        }
      }
    })
  }

  private deleteByIndex(index: number){
    let contactMeansPersons: ContactMeansPeople[] =  this.form?.get('contactMeansPeople')?.value;
    contactMeansPersons.splice(index, 1);
    this.form.get('contactMeansPeople')?.setValue(contactMeansPersons);
  }

}