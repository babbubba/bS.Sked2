import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Task } from './models/task';
import { BaseService } from './baseService';

@Injectable({
  providedIn: 'root'
})
export class TasksService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getJobTasksUrl: string = 'api/engine/getjobtasks';

  getJobTasks(jobId: string): Observable<Task[]> {
    return this.http.get<Task[]>(this.getJobTasksUrl + '/' + jobId);
  }
}
