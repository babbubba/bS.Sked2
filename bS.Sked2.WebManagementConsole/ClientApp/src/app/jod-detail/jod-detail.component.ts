import { Component, Input, Output, EventEmitter } from '@angular/core';
import { JobDetail } from '../models/jobDetail';
import { JobsService } from '../jobs.service';
import { MessageService, Verbosity } from '../message.service';

@Component({
  selector: 'app-jod-detail',
  templateUrl: './jod-detail.component.html',
  styleUrls: ['./jod-detail.component.css']
})
export class JodDetailComponent {

  constructor(private jobsService: JobsService, private messageService: MessageService) { }

  @Input() jobDetail: JobDetail;
  @Output() jobSaved: EventEmitter<boolean> = new EventEmitter();

  onJobSave(): void {
    if (this.jobDetail.id != null)
      this.jobsService.saveJobDetail(this.jobDetail);
    else {
      // Create new Job
      this.jobsService.createJobDetail(this.jobDetail)
        .subscribe(result => {
          this.messageService.display(`Job created with id: ${result}`, Verbosity.Success);
          //this.jobDetail = null;
          this.jobSaved.emit(true);
        });
    }
    
  }

}
