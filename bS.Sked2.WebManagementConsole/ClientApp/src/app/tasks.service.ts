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

  getTaskCreateUrl: string = 'api/engine/gettaskcreate';
  getJobTasksUrl: string = 'api/engine/getjobtasks';
  createTaskUrl: string = 'api/engine/createtask';
  editTaskUrl: string = 'api/engine/edittask';

  getTaskCreate(): Observable<Task> {
    return this.http.get<Task>(this.getTaskCreateUrl);
  }

  getJobTasks(jobId: string): Observable<Task[]> {
    return this.http.get<Task[]>(this.getJobTasksUrl + '/' + jobId);
  }

  createTask(task: Task): Observable<string> {
    return this.http.put<string>(this.createTaskUrl, task, this.httpOptions);
  }

  editTask(task: Task): Observable<boolean> {
    return this.http.put<boolean>(this.editTaskUrl, task, this.httpOptions);
  }
}
