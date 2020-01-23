import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Job } from '../models/job';
import { Task } from '../models/task';
import { JobsService } from '../jobs.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Trigger } from '../models/trigger';
import { AddEditTaskModalComponent } from '../add-edit-task-modal/add-edit-task-modal.component';

@Component({
  selector: 'app-job-page',
  templateUrl: './job-page.component.html',
  styleUrls: ['./job-page.component.css']
})
export class JobPageComponent implements OnInit {

  constructor(private routeParams: ActivatedRoute, private jobService: JobsService, private spinnerService: NgxSpinnerService, private modalService: NgbModal) { }

  job: Job;
  tasks: Task[];

  selectedTask: Task;
  selectedTrigger: Trigger;

  disableAddTaskButton: boolean = false;
  disableAddTriggerButton: boolean = false;

  //AddEditTaskModalComponent


  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.spinnerService.show();
    this.routeParams.params.subscribe(params => {
      let jobId = params['id'];
      this.jobService.getJob(jobId)
        .subscribe(result => {
          this.job = result;
          this.jobService.getJobTasks(jobId)
            .subscribe(result => {
              this.tasks = result;
              this.spinnerService.hide();
            });
        });
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
