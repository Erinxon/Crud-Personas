import { TestBed } from '@angular/core/testing';

import { ContactMeansPeopleService } from './contact-means-people.service';

describe('ContactMeansPeopleService', () => {
  let service: ContactMeansPeopleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContactMeansPeopleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
