import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Job } from './models/job';
import { JobDetail } from './models/jobDetail';

@Injectable({
  providedIn: 'root'
})
export class JobsService {

  constructor(private http: HttpClient) { }

  getJobsUrl: string = 'api/engine/getjobs';
  getJobDetailUrl: string = 'api/engine/getjob';

  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.getJobsUrl)
  }

  getJobCreate(): Observable<JobDetail> {
    return of(JOBDETAILNEW);
  }

  createJobDetail(job: JobDetail): void {
    //TODO: Implementa logica per la creazione
  }

  saveJobDetail(job: JobDetail): void {
    //TODO: Implementa logica per il salvataggio
  }

  getJobDetail(id: string): Observable<JobDetail> {
    return this.http.get<JobDetail>(this.getJobDetailUrl + '/' + id);
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

const JOBDETAILNEW: JobDetail = {
  id: null,
  name: 'Job vuoto',
  description: 'Descrizione vuota',
  failIfAnyTaskHasError: false,
  failIfAnyTaskHasWarning: false,
  isEnabled: true,
  creationDate: new Date,
  lastUpdateDate: new Date

};
