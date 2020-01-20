import { Injectable } from '@angular/core';
import { Job } from './models/job';
import { JobDetail } from './models/jobDetail';

@Injectable({
  providedIn: 'root'
})
export class JobsService {

  constructor() { }

  getJobs(): Job[] {
    return JOBS;
  }

  getJobDetail(id: string): JobDetail {
    return JOBDETAIL;
  }
}

const JOBS: Job[] = [
  { id: '1', name: 'Job 1', description: 'Job Fake 1' },
  { id: '2', name: 'Job 2', description: 'Job Fake 2' },
];

const JOBDETAIL: JobDetail = {
  id: '3',
  name: 'Job dettaglio',
  description: 'Descrizione nel dettaglio di un job',
  failIfAnyTaskHasError: true,
  failIfAnyTaskHasWarning: false,
  isEnabled: true,
  creationDate: new Date,
  lastUpdateDate: new Date

};
