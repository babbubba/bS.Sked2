import { Injectable } from '@angular/core';
import { TaskElement } from './models/taskElement';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './baseService';

@Injectable({
  providedIn: 'root'
})
export class ElementService extends BaseService {

  getTaskElementsUrl: string = 'api/engine/gettaskelements';
  getElementCreateUrl: string = 'api/engine/getelementcreate';
  createElementUrl: string = 'api/engine/createelement';

  constructor(private http: HttpClient) {
    super();
  }


  getTaskElements(taskId: string): Observable<TaskElement[]> {
    return this.http.get<TaskElement[]>(this.getTaskElementsUrl + '/' + taskId);
  }

  getElementCreate(): Observable<TaskElement> {
    return this.http.get<TaskElement>(this.getElementCreateUrl);
  }

  createElement(element: TaskElement): Observable<string> {
    element.elementTypesList = null;
    return this.http.put<string>(this.createElementUrl, element, this.httpOptions);
  }
}
