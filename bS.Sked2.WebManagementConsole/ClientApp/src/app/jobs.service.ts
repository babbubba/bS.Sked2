import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Job } from './models/job';
import { JobEdit } from './models/jobEdit';
import { MessageService, Verbosity } from './message.service';
import { Task } from './models/task';

@Injectable({
  providedIn: 'root'
})

export class JobsService {

  constructor(private http: HttpClient, private messageService: MessageService) { }

  getJobsUrl: string = 'api/engine/getjobs';
  getJobEditUrl: string = 'api/engine/getjobedit';
  getJobCreateUrl: string = 'api/engine/getjobcreate';
  createJobUrl: string = 'api/engine/createjob';
  saveJobUrl: string = 'api/engine/savejob';
  getJobTasksUrl: string = 'api/engine/getjobtasks';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  getJob(id:string): Observable<Job> {
    return this.http.get<Job>(this.getJobEditUrl + '/' + id)
      .pipe(
        catchError(this.handleError<Job>('getJob'))
      );
  }

  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.getJobsUrl)
      .pipe(
        catchError(this.handleError<Job[]>('getJobs', []))
      );
  }

  getJobCreate(): Observable<JobEdit> {
    return this.http.get<JobEdit>(this.getJobCreateUrl)
      .pipe(
        catchError(this.handleError<JobEdit>('getJobCreate'))
      );
  }

  createJob(job: JobEdit): Observable<string> {
    return this.http.put<string>(this.createJobUrl, job, this.httpOptions)
      .pipe(
        catchError(this.handleError<string>('createJob'))
      );
  }

  saveJob(job: JobEdit): Observable<boolean> {
    return this.http.put<boolean>(this.saveJobUrl, job, this.httpOptions)
      .pipe(
        catchError(this.handleError<boolean>('saveJob'))
      );
  }

  getJobEdit(id: string): Observable<JobEdit> {
    return this.http.get<JobEdit>(this.getJobEditUrl + '/' + id)
      .pipe(
        catchError(this.handleError<JobEdit>('getJobEdit'))
      );
  }

  getJobTasks(jobId: string): Observable<Task[]> {
    return this.http.get<Task[]>(this.getJobTasksUrl + '/' + jobId)
      .pipe(
        catchError(this.handleError<Task[]>('getJobEdit'))
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
