import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Task } from '../models/task';
import { AddEditTaskModalComponent } from '../add-edit-task-modal/add-edit-task-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-task-preview',
  templateUrl: './task-preview.component.html',
  styleUrls: ['./task-preview.component.css']
})
export class TaskPreviewComponent {

  constructor(private modalService: NgbModal) { }

  @Input() task: Task;

  editTask() {
    const modalAddNewTask = this.modalService.open(AddEditTaskModalComponent);
    modalAddNewTask.componentInstance.task = this.task;
    modalAddNewTask.result
      .then(res => {
        //save or create success then reload data
      })
      .catch(err => { });
  }

}
