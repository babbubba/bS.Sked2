import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Job } from '../models/job';
import { Task } from '../models/task';
import { JobsService } from '../jobs.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Trigger } from '../models/trigger';
import { AddEditTaskModalComponent } from '../add-edit-task-modal/add-edit-task-modal.component';
import { TasksService } from '../tasks.service';
import { MessageService, Verbosity } from '../message.service';

@Component({
  selector: 'app-job-page',
  templateUrl: './job-page.component.html',
  styleUrls: ['./job-page.component.css']
})
export class JobPageComponent implements OnInit {

  constructor(
    private routeParams: ActivatedRoute,
    private messageService: MessageService,
    private jobService: JobsService,
    private taskService: TasksService,
    private spinnerService: NgxSpinnerService,
    private modalService: NgbModal) { }

  job: Job;
  tasks: Task[];

  selectedTask: Task;
  selectedTrigger: Trigger;

  disableAddTaskButton: boolean = false;
  disableAddTriggerButton: boolean = false;

  ngOnInit() {
    this.loadData();
  }

  private loadData() {
    this.spinnerService.show();
    this.routeParams.params.subscribe(
      params => {
        let jobId = params['id'];
        this.jobService.getJob(jobId)
          .subscribe(result => {
            this.job = result;
            this.taskService.getJobTasks(jobId)
              .subscribe(result => {
                this.tasks = result;
                this.spinnerService.hide();
              });
          });
      },
      error => {
        this.messageService.display("Invalid url", Verbosity.Error);
      });
  }

  taskSelect(task: Task) {

  }

  triggerSelect(trigger: Trigger) {

  }

  addNewTask() {
    let t = new Task();
    t.name = "pippo";
    t.description = "pluto";
    t.failIfAnyElementHasError = true;
    t.failIfAnyElementHasWarning = false;
    t.isEnabled = true;
    const modalAddNewTask = this.modalService.open(AddEditTaskModalComponent);
    modalAddNewTask.componentInstance.task = t;
    modalAddNewTask.result
      .then(res => {
        //save or create
        console.log(res);
      })
      .catch(err => {
        //dismiss
        console.error(err);
      });
  }

  addNewTrigger() {

  }

}
