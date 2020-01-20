import { Component, OnInit, Input} from '@angular/core';
import { Job } from '../models/job';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent implements OnInit {

  constructor() { }

  @Input() job: Job;

  ngOnInit() {
  }

}
