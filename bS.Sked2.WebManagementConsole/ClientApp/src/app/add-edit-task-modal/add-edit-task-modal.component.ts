import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Task } from '../models/task';
import { TasksService } from '../tasks.service';
import { MessageService, Verbosity } from '../message.service';

@Component({
  selector: 'app-add-edit-task-modal',
  templateUrl: './add-edit-task-modal.component.html',
  styleUrls: ['./add-edit-task-modal.component.css']
})
export class AddEditTaskModalComponent implements OnInit {
  @Input() task: Task;
  actionType: string;
  submitted: boolean;

  constructor(
    public activeModal: NgbActiveModal,
    private tasksService: TasksService,
    private messageService: MessageService
  ) {
    this.actionType = "Add";
    this.submitted = false;
  }

  ngOnInit() {
    if (this.task.id != null) {
      this.actionType = "Edit";
    }
  }

  saveOrUpdate(form) {
    this.submitted = true;
    // stop here if form is invalid
    if (form.invalid) {
      return;
    }

    if (this.actionType == "Add") {
      this.tasksService.createTask(this.task)
        .subscribe(
          result => {
            //success
            this.activeModal.close('Success');
          },
          error => {
            this.messageService.display(`Task was not created: ${error}`, Verbosity.Error);
            return;
          });
      this.messageService.display(`The Task '${this.task.name}' has been created for this Job.`);
    }
    else if (this.actionType == "Edit") {
      this.tasksService.editTask(this.task)
        .subscribe(
          result => {
            //success
            this.activeModal.close('Success');
          },
          error => {
            this.messageService.display(`Task was not updated: ${error}`, Verbosity.Error);
            return;
          });
      this.messageService.display(`The Task '${this.task.name}' has been updated.`);
    }

  }
}
