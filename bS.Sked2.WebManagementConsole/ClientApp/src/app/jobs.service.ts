import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Job } from './models/job';
import { JobEdit } from './models/jobEdit';
import { Task } from './models/task';
import { BaseService } from './baseService';

@Injectable({
  providedIn: 'root'
})

export class JobsService extends BaseService  {

  constructor(private http: HttpClient) {
      super();
  }

  getJobsUrl: string = 'api/engine/getjobs';
  getJobEditUrl: string = 'api/engine/getjobedit';
  getJobCreateUrl: string = 'api/engine/getjobcreate';
  createJobUrl: string = 'api/engine/createjob';
  saveJobUrl: string = 'api/engine/savejob';
  getJobTasksUrl: string = 'api/engine/getjobtasks';

  getJob(id:string): Observable<Job> {
    return this.http.get<Job>(this.getJobEditUrl + '/' + id);
  }

  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.getJobsUrl);
  }

  getJobCreate(): Observable<JobEdit> {
    return this.http.get<JobEdit>(this.getJobCreateUrl);
  }

  createJob(job: JobEdit): Observable<string> {
    return this.http.put<string>(this.createJobUrl, job, this.httpOptions);
  }

  saveJob(job: JobEdit): Observable<boolean> {
    return this.http.put<boolean>(this.saveJobUrl, job, this.httpOptions);
  }

  getJobEdit(id: string): Observable<JobEdit> {
    return this.http.get<JobEdit>(this.getJobEditUrl + '/' + id);
  }

  getJobTasks(jobId: string): Observable<Task[]> {
    return this.http.get<Task[]>(this.getJobTasksUrl + '/' + jobId);
  }

}
