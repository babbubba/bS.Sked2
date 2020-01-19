import { Component, OnInit } from '@angular/core';
import { JobsService } from '../jobs.service';
import { Job } from '../models/Job';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private jobsService: JobsService) { }

  jobs: Job[];

  ngOnInit() {
    this.loadJobs();
  }

  loadJobs() {
    this.jobs = this.jobsService.getJobs();
  }

}
