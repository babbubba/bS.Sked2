import { Component, Input } from '@angular/core';
import { JobDetail } from '../models/jobDetail';
import { JobsService } from '../jobs.service';

@Component({
  selector: 'app-jod-detail',
  templateUrl: './jod-detail.component.html',
  styleUrls: ['./jod-detail.component.css']
})
export class JodDetailComponent {

  constructor(private jobsService: JobsService) { }

  @Input() jobDetail: JobDetail;

  onJobSave(): void {
    if (this.jobDetail.id != null)
      this.jobsService.saveJobDetail(this.jobDetail);
    else
      this.jobsService.createJobDetail(this.jobDetail);
  }

}
