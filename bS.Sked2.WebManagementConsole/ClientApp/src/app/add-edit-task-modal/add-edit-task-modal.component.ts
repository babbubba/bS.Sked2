import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { FormGroup, FormControl } from '@angular/forms';
import { Task } from '../models/task';

@Component({
  selector: 'app-add-edit-task-modal',
  templateUrl: './add-edit-task-modal.component.html',
  styleUrls: ['./add-edit-task-modal.component.css']
})
export class AddEditTaskModalComponent implements OnInit {

  @Input() task: Task;
  actionType: string;
  form: FormGroup;
  submitted = false;
  

  constructor(public activeModal: NgbActiveModal, private fb: FormBuilder) {
    this.actionType = "Add";
  }

  ngOnInit() {
    if (this.task.id != null) {
      this.actionType = "Edit";
    }

    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.maxLength(250)]],
      isEnabled: [''],
      failIfAnyElementHasError: [''],
      failIfAnyElementHasWarning: [''],
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  saveOrUpdate() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
      console.warn(this.f.name.errors)
      return;
    }
    this.activeModal.close('Success');
  }

}
