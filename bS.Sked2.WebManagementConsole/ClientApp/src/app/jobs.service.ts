import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
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
  getEmptyJobDetailUrl: string = 'api/engine/getemptyjobcreate';

  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.getJobsUrl)
      .pipe(
        tap(_ => console.log('fetched jobs')),
        catchError(this.handleError<Job[]>('getJobs', []))
      );
  }

  getJobCreate(): Observable<JobDetail> {
    return this.http.get<JobDetail>(this.getEmptyJobDetailUrl)
      .pipe(
        tap(_ => console.log('fetched empty job')),
        catchError(this.handleError<JobDetail>('getJobCreate'))
      );
  }

  createJobDetail(job: JobDetail): void {
    //TODO: Implementa logica per la creazione
  }

  saveJobDetail(job: JobDetail): void {
    //TODO: Implementa logica per il salvataggio
  }

  getJobDetail(id: string): Observable<JobDetail> {
    return this.http.get<JobDetail>(this.getJobDetailUrl + '/' + id)
      .pipe(
        tap(_ => console.log('fetched job')),
        catchError(this.handleError<JobDetail>('getJobDetail'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error('${operation}: ${error}'); // log to console
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
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
