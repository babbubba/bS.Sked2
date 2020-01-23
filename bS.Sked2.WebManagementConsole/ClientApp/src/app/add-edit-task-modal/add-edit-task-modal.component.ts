import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { Task } from '../models/task';

@Component({
  selector: 'app-add-edit-task-modal',
  templateUrl: './add-edit-task-modal.component.html',
  styleUrls: ['./add-edit-task-modal.component.css']
})
export class AddEditTaskModalComponent implements OnInit {

  constructor(public activeModal: NgbActiveModal) { }

  @Input() task: Task;
  isEditing: boolean;
  isAdding: boolean;

  ngOnInit() {
    if (this.task.id == null) {
      this.isAdding = true;
      this.isEditing = false;
    }
    else {
      this.isAdding = false;
      this.isEditing = true;
    }
  }

}
