import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Job } from './models/job';
import { JobDetail } from './models/jobDetail';
import { MessageService, Verbosity } from './message.service';

@Injectable({
  providedIn: 'root'
})

export class JobsService {

  constructor(private http: HttpClient, private messageService: MessageService) { }

  getJobsUrl: string = 'api/engine/getjobs';
  getJobDetailUrl: string = 'api/engine/getjob';
  getEmptyJobDetailUrl: string = 'api/engine/getemptyjobcreate';
  createJobDetailUrl: string = 'api/engine/createjob';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.getJobsUrl)
      .pipe(
        //tap(_ => console.log('fetched jobs')),
        catchError(this.handleError<Job[]>('getJobs', []))
      );
  }

  getJobCreate(): Observable<JobDetail> {
    return this.http.get<JobDetail>(this.getEmptyJobDetailUrl)
      .pipe(
        //tap(_ => console.log('fetched empty job')),
        catchError(this.handleError<JobDetail>('getJobCreate'))
      );
  }

  createJobDetail(job: JobDetail): Observable<string> {
    return this.http.put<string>(this.createJobDetailUrl, job, this.httpOptions)
      .pipe(
        //tap(_ => console.log('created job')),
        catchError(this.handleError<string>('createJobDetail'))
      );
  }

  saveJobDetail(job: JobDetail): void {
    //TODO: Implementa logica per il salvataggio
  }

  getJobDetail(id: string): Observable<JobDetail> {
    return this.http.get<JobDetail>(this.getJobDetailUrl + '/' + id)
      .pipe(
        //tap(_ => console.log('fetched job')),
        catchError(this.handleError<JobDetail>('getJobDetail'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.messageService.display(error.message, Verbosity.Error);
      console.error(error);
      return of(result as T);
    };
  }
}
