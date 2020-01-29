import { Injectable } from '@angular/core';
import { TaskElement } from './models/taskElement';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ElementService {
  getTaskElementsUrl: string ='api/engine/gettaskelements';

  constructor(private http: HttpClient) { }

  getTaskElements(taskId: string): Observable<TaskElement[]> {
    return this.http.get<TaskElement[]>(this.getTaskElementsUrl + '/' + taskId);
  }
}
