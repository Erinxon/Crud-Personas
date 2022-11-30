import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactMeanComponent } from './contact-mean.component';

describe('ContactMeanComponent', () => {
  let component: ContactMeanComponent;
  let fixture: ComponentFixture<ContactMeanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContactMeanComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactMeanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
