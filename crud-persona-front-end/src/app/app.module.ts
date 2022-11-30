import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatSliderModule } from '@angular/material/slider';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { PersonsComponent } from './components/persons/persons.component';
import { PersonComponent } from './components/persons/person/person.component';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ContactMeanComponent } from './components/persons/person/contact-mean/contact-mean.component';
import { ConfirmDialogComponent } from './shared/confirm-dialog/confirm-dialog.component';
import { ngxLoadingAnimationTypes, NgxLoadingModule } from 'ngx-loading';
import { LoandingInterceptorInterceptor } from './services/LoandingInterceptor.Interceptor';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {NgxPaginationModule} from 'ngx-pagination'; 
@NgModule({
  declarations: [
    AppComponent,
    PersonsComponent,
    PersonComponent,
    ContactMeanComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatSliderModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.circleSwish,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)', 
      backdropBorderRadius: '4px',
      primaryColour: '#00000', 
      secondaryColour: '#00000', 
      tertiaryColour: '#00000'
  }),
  MatSnackBarModule,
  MatTooltipModule,
  NgxPaginationModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: LoandingInterceptorInterceptor, multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
