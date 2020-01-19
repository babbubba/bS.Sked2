import { Component, OnInit, Input} from '@angular/core';
import { Job } from '../models/Job';

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
