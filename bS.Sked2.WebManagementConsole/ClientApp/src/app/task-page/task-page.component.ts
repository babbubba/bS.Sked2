import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TasksService } from '../tasks.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Task } from '../models/task';
import { ElementService } from '../element.service';
import { TaskElement } from '../models/taskElement';

@Component({
  selector: 'app-task-page',
  templateUrl: './task-page.component.html',
  styleUrls: ['./task-page.component.css']
})
export class TaskPageComponent implements OnInit {

  constructor(
    private routeParams: ActivatedRoute,
    private taskService: TasksService,
    private elementService: ElementService,
    private spinnerService: NgxSpinnerService,
  ) { }

  task: Task;
  elements: TaskElement[];

  ngOnInit() {
    this.loadData();
  }

  private loadData() {
    this.spinnerService.show();
    this.routeParams.params.subscribe(
      params => {
        let taskId = params['id'];
        this.taskService.getTask(taskId)
          .subscribe(result => {
            this.task = result;
            this.elementService.getTaskElements(taskId)
              .subscribe(result => {
                this.elements = result;
                this.spinnerService.hide();
              });
          });
      },
      error => {
        this.spinnerService.hide();
        console.error(error);
      });
  }
}
