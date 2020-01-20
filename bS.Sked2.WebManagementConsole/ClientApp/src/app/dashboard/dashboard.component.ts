import { Component, OnInit } from '@angular/core';
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
  selectedJob: JobDetail;

  ngOnInit() {
    this.loadJobs();
  }

  loadJobs() {
    this.jobs = this.jobsService.getJobs();
  }

  onJobSelect(job: Job) {
    this.selectedJob = this.jobsService.getJobDetail(job.id);
  }

}
