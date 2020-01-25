import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { JobsService } from '../jobs.service';
import { MessageService, Verbosity } from '../message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { JobEdit } from '../models/jobEdit';

@Component({
  selector: 'app-jod-detail',
  templateUrl: './jod-detail.component.html',
  styleUrls: ['./jod-detail.component.css']
})
export class JodDetailComponent implements OnInit {

  constructor(private jobsService: JobsService, private messageService: MessageService, private spinnerService: NgxSpinnerService) { }

  @Input() jobDetail: JobEdit;
  @Output() jobSaved: EventEmitter<boolean> = new EventEmitter();

  onJobSave(): void {
    this.spinnerService.show();

    if (this.jobDetail.id != null)
      // save edited job
      this.jobsService.saveJob(this.jobDetail)
        .subscribe(result => {
          if(result) this.messageService.display(`Job saved successfully.`, Verbosity.Success);
          else this.messageService.display(`Job not saved.`, Verbosity.Error);
          this.jobSaved.emit(true);
        });
    else {
      // Create new Job
      this.jobsService.createJob(this.jobDetail)
        .subscribe(result => {
          this.messageService.display(`Job created with id: ${result}`, Verbosity.Success);
          this.jobSaved.emit(true);
        });
    }
    
  }

  ngOnInit() {
    
  }
}
