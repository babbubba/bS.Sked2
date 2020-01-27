import { Injectable, OnInit, TemplateRef   } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class MessageService implements OnInit {

  constructor() {
    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl("https://localhost:44323/messagenotificationhub")
      .build();

    connection.start().then(function () {
      console.debug('Message Notification Hub connected!');
    }).catch(function (err) {
      return console.error('Error connecting Message Notification Hub: ' + err.toString());
    });

    connection.on("DisplayMessage", (message, severity) => {
      this.display(message, severity);
    });

    connection.on("DisplayNotify", (message, severity) => {
      this.notify(message, severity);
      console.log("Notify: " + message);
    });
  }

  ngOnInit() {

  }

  message: string = '';
  notifyMessage: string = '';
  showMessage: boolean = false;
  showNotify: boolean = false;
  messageVerbosity: Verbosity;
  notifyVerbosity: Verbosity;
  toasts: any[] = [];

  display(message: string, verbosity: Verbosity = 0): void {
    this.message = message;
    this.showMessage = true;
    this.messageVerbosity = verbosity;
  }
  hideMessage() {
    this.showMessage = false;
  }

  clearMessage() {
    this.message = '';
    this.showMessage = false;
  }

  notify(notifyMessage: string, verbosity: Verbosity = 0): void {
    //this.notifyMessage = notifyMessage;
    //this.showNotify = true;
    //this.notifyVerbosity = verbosity;
    let className = '';
    switch (verbosity) {
      case Verbosity.Info:
        className = 'bg-info text-light'
        break;
      case Verbosity.Error:
        className = 'bg-danger text-light'
        break;
      case Verbosity.Warning:
        className = 'bg-warning text-light'
        break;
      case Verbosity.Success:
        className = 'bg-success text-light'
        break;
    }

    this.showToast(notifyMessage, {
      headertext: 'Notify',
      classname: className,
      delay: 6000,
      autohide: true
    });
  }

  hideNotify() {
    this.showNotify = false;
  }

  clearNotify() {
    this.notifyMessage = '';
    this.showNotify = false;
  }

  // Push new Toasts to array with content and options
  showToast(textOrTpl: string | TemplateRef<any>, options: any = {}) {
    this.toasts.push({ textOrTpl, ...options });
  }

  // Callback method to remove Toast DOM element from view
  remove(toast) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}

export enum Verbosity {
  Debug = 0,
  Info = 10,
  Warning = 20,
  Error = 30,
  Success = 40,
}
