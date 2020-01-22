import { Component, OnInit } from '@angular/core';
import { MessageService, Verbosity } from '../message.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  constructor(private messageService: MessageService) { }

  ngOnInit() {
  }

  getAlertClass() : string {
    switch (this.messageService.verbosity) {
      case Verbosity.Info:
        return 'alert-primary';
      case Verbosity.Success:
        return 'alert-success';
      case Verbosity.Warning:
        return 'alert-warning';
      case Verbosity.Error:
        return 'alert-danger';
    }
  }

  getAlertTitle(): string {
    switch (this.messageService.verbosity) {
      case Verbosity.Info:
        return 'Info';
      case Verbosity.Success:
        return 'Success';
      case Verbosity.Warning:
        return 'Warning';
      case Verbosity.Error:
        return 'Error';
    }
  }

}
