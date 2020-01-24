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
export class TasksService {

  constructor(private http: HttpClient, private messageService: MessageService) {

  }
}
