import { Injectable, OnInit } from '@angular/core';
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
    this.notifyMessage = notifyMessage;
    this.showNotify = true;
    this.notifyVerbosity = verbosity;
  }

  hideNotify() {
    this.showNotify = false;
  }

  clearNotify() {
    this.notifyMessage = '';
    this.showNotify = false;
  }
}

export enum Verbosity {
  Debug = 0,
  Info = 10,
  Warning = 20,
  Error = 30,
  Success = 40,
}
