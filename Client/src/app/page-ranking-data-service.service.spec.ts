import { TestBed } from '@angular/core/testing';

import { PageRankingDataServiceService } from './page-ranking-data-service.service';

describe('PageRankingDataServiceService', () => {
  let service: PageRankingDataServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageRankingDataServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
