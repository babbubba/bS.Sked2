import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Job } from '../models/job';
import { Task } from '../models/task';
import { JobsService } from '../jobs.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-job-page',
  templateUrl: './job-page.component.html',
  styleUrls: ['./job-page.component.css']
})
export class JobPageComponent implements OnInit {

  constructor(private routeParams: ActivatedRoute, private jobService: JobsService, private spinnerService: NgxSpinnerService) { }

  job: Job;
  tasks: Task[];

  ngOnInit() {
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

    // Load job data
  }

}
