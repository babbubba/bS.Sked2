import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskElement } from '../models/taskElement';
import { ElementService } from '../element.service';
import { MessageService, Verbosity } from '../message.service';

@Component({
  selector: 'app-add-element-modal',
  templateUrl: './add-element-modal.component.html',
  styleUrls: ['./add-element-modal.component.css']
})
export class AddElementModalComponent implements OnInit {

  @Input() element: TaskElement;
  submitted: boolean;

  constructor(
    public activeModal: NgbActiveModal,
    private elementService: ElementService,
    private messageService: MessageService
  ) { }

  ngOnInit() {
  }

  save(form) {
    this.submitted = true;
    // stop here if form is invalid
    if (form.invalid) {
      return;
    }

    this.elementService.createElement(this.element)
      .subscribe(
        result => {
          //success
          this.activeModal.close('Success');
        },
        error => {
          this.messageService.display(`Element was not created: ${error}`, Verbosity.Error);
          return;
        });
    this.messageService.display(`The Element '${this.element.name}' has been created for this Job.`);
  }
}
