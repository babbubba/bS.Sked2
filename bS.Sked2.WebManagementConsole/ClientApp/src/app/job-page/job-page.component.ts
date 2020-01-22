import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-job-page',
  templateUrl: './job-page.component.html',
  styleUrls: ['./job-page.component.css']
})
export class JobPageComponent implements OnInit {

  constructor(private routeParams: ActivatedRoute) { }

  jobId: string;

  ngOnInit() {
    this.routeParams.params.subscribe(params => {
      this.jobId = params['id'];
    });
  }

}
