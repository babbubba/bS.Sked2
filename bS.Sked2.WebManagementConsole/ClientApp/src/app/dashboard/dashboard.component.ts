import { Component, OnInit} from '@angular/core';
import { JobsService } from '../jobs.service';
import { Job } from '../models/job';
import { NgxSpinnerService } from 'ngx-spinner';
import { JobEdit } from '../models/jobEdit';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(private jobsService: JobsService, private spinnerService: NgxSpinnerService) { }

  jobs: Job[];
  selectedJob: Job;
  selectedJobEdit: JobEdit;
  showJobDetail: boolean = false;
  disableAddJobButton: boolean = false;

  ngOnInit() {
    this.loadJobs();
  }

  loadJobs() {
    this.spinnerService.show(); 
    this.jobsService.getJobs()
      .subscribe(jobs => {
        this.jobs = jobs;
        this.spinnerService.hide(); 
      });
  }

  onJobSelect(job: Job) {
    this.spinnerService.show(); 

    this.selectedJob = job;
    if (this.selectedJob != null) {
      this.disableAddJobButton = false;
      this.jobsService.getJobEdit(this.selectedJob.id)
        .subscribe(result => {
          this.selectedJobEdit = result;
          this.showJobDetail = true;
          this.spinnerService.hide(); 
        });
    }
    else {
      this.disableAddJobButton = true;
      this.jobsService.getJobCreate()
        .subscribe(result => {
          this.selectedJobEdit = result;
          this.showJobDetail = true;
          this.spinnerService.hide(); 
        });
    }
  }

  jobDetailSaved($event) {
    if ($event) {
      this.showJobDetail = false;
      this.loadJobs();
    }
  }

}
