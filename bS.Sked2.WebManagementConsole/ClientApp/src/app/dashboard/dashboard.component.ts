import { Component, OnInit} from '@angular/core';
import { JobsService } from '../jobs.service';
import { Job } from '../models/job';
import { NgxSpinnerService } from 'ngx-spinner';
import { JobEdit } from '../models/jobEdit';
import { MessageService, Verbosity } from '../message.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(private jobsService: JobsService, private spinnerService: NgxSpinnerService, private messageService: MessageService) { }

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
      .subscribe(result => {
        this.jobs = result;
        this.spinnerService.hide(); 
      },
        error => {
          this.spinnerService.hide();
          this.messageService.display(`Error loading jobs.\n${error}`, Verbosity.Error)
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
