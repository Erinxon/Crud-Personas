import { TestBed } from '@angular/core/testing';

import { ContactMeansService } from './contact-means.service';

describe('ContactMeansService', () => {
  let service: ContactMeansService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContactMeansService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
