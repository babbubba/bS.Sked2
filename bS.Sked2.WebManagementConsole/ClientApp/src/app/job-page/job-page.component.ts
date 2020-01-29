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
    private jobService: JobsService,
    private taskService: TasksService,
    private spinnerService: NgxSpinnerService,
    private modalService: NgbModal) { }

  job: Job;
  tasks: Task[];

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
        this.spinnerService.hide();
        console.error(error);
      });
  }

  openTask() {

  }

  addNewTask() {
    this.taskService.getTaskCreate()
      .subscribe(
        result => {
          result.parentJobId = this.job.id;
          const modalAddNewTask = this.modalService.open(AddEditTaskModalComponent);
          modalAddNewTask.componentInstance.task = result;
          modalAddNewTask.result
            .then(res => {
              if (res === "Success") {
                //save or create success then reload data
                this.loadData();
              }
            })
            .catch(err => { });
        },
        error => { });
  }

  addNewTrigger() {
  }
}
