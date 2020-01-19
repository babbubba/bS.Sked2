import { Injectable } from '@angular/core';
import { Job } from './models/Job';

@Injectable({
  providedIn: 'root'
})
export class JobsService {

  constructor() { }

  getJobs(): Job[] {
    return JOBS;
  }
}

const JOBS: Job[] = [
  { id: '1', name: 'Job 1', description: 'Job Fake 1' },
  { id: '2', name: 'Job 2', description: 'Job Fake 2' },
];
