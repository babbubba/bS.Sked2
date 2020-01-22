import { Component, OnInit} from '@angular/core';
import { JobsService } from '../jobs.service';
import { Job } from '../models/job';
import { JobDetail } from '../models/jobDetail';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(private jobsService: JobsService) { }

  jobs: Job[];
  selectedJob: Job;
  selectedJobDetail: JobDetail;
  showJobDetail: boolean = false;
  disableAddJobButton: boolean = false;

  ngOnInit() {
    this.loadJobs();
  }

  loadJobs() {
    this.jobsService.getJobs()
      .subscribe(jobs => this.jobs = jobs);
  }

  onJobSelect(job: Job) {
    this.selectedJob = job;
    if (this.selectedJob != null) {
      this.disableAddJobButton = false;
      this.jobsService.getJobDetail(this.selectedJob.id)
        .subscribe(jobDetail => {
          this.selectedJobDetail = jobDetail;
          this.showJobDetail = true;
        });
    }
    else {
      this.disableAddJobButton = true;
      this.jobsService.getJobCreate()
        .subscribe(jobCreate => {
          this.selectedJobDetail = jobCreate;
          this.showJobDetail = true;
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
