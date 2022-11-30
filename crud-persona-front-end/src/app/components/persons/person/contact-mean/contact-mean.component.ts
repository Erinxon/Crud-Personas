import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ArgumentsModal } from 'src/app/models/arguments.modal.model';
import { ContactMeans, ContactMeansPeople } from 'src/app/models/person.model';
import { ContactMeansService } from 'src/app/services/contact-means.service';
import { PersonService } from 'src/app/services/person.service';

@Component({
  selector: 'app-contact-mean',
  templateUrl: './contact-mean.component.html',
  styleUrls: ['./contact-mean.component.css']
})
export class ContactMeanComponent implements OnInit {
  form!: FormGroup;
  formInvalid: boolean = false;
  contactMeans: ContactMeans[] = [];
  
  constructor(public dialogRef: MatDialogRef<ContactMeanComponent>,
    private fb: FormBuilder,
    private contactMeansService: ContactMeansService,
    @Inject(MAT_DIALOG_DATA) public data: ArgumentsModal<ContactMeansPeople>) {
    this.dialogRef.disableClose = true;
    this.buildForm();
    if(this.data?.item){
      this.form.patchValue({
        ...this.data?.item
      });
    }
    if(this.data.view){
      this.form.disable();
    }
  }

  ngOnInit(): void {
    this.getContactMeans();
  }

  getContactMeans(){
    this.contactMeansService.getContactMeans().subscribe({
      next: res => {
        this.contactMeans = res?.data.filter(d => {
          if(this.data?.add && this.data?.itemList?.some(c => c?.contactMeansId === d?.contactMeansId)){
            return;
          }
          return d;
        })
      }
    })
  }

  buildForm() {
    this.form = this.fb.group({
      id: [0],
      contactMeansId: [null, Validators.required],
      personId: [0],
      contact: ['', [Validators.required, Validators.maxLength(100)]],
    });
  }

  save(){
    this.formInvalid = this.form.invalid;
    if(this.formInvalid) return;

    const contactMeansPeople: ContactMeansPeople = this.form?.value;
    contactMeansPeople.contactMeans = this.contactMeans?.find(c => c?.contactMeansId === contactMeansPeople?.contactMeansId);
    this.dialogRef.close({
      contactMeansPerson: contactMeansPeople
    });
  }

  close(){
    this.dialogRef.close(null);
  }

  getError(name: string) {
    if (!this.formInvalid) {
      return '';
    }
    const field = this.form.get(name);
    if (field?.valid) return;
    if (field?.errors!['required']) {
      return 'Este campo es obligatorio';
    }
    if (field?.errors!['maxlength']) {
      return `Este campo sólo admite un máximo de ${field?.errors['maxlength']?.requiredLength} caracteres`;
    }
    return '';
  }
  
}