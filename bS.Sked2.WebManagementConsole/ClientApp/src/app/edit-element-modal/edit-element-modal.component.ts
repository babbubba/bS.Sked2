import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskElement } from '../models/taskElement';
import { ElementService } from '../element.service';
import { MessageService, Verbosity } from '../message.service';

@Component({
  selector: 'app-edit-element-modal',
  templateUrl: './edit-element-modal.component.html',
  styleUrls: ['./edit-element-modal.component.css']
})
export class EditElementModalComponent implements OnInit {

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

    this.elementService.updateElement(this.element)
      .subscribe(
        result => {
          //success
          this.activeModal.close('Success');
        },
        error => {
          this.messageService.display(`Element was not updated: ${error}`, Verbosity.Error);
          return;
        });
    this.messageService.display(`The Element '${this.element.name}' has been saved.`);
  }

  open() {

  }

  delete() {
    const ans = confirm('Do you really want to delete this element');
    if (ans) {
      this.elementService.deleteElement(this.element.id)
        .subscribe(
          result => {
            //success
            this.activeModal.close('Deleted');
          },
          error => {
            this.messageService.display(`Element was not deleted: ${error}`, Verbosity.Error);
            return;
          });
      this.messageService.display(`The Element '${this.element.name}' has been deleted.`);
    }
  }
}
