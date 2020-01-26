import { Injectable, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 

@Injectable({
  providedIn: 'root'
})
export class MessageService implements OnInit {

  constructor() { }

  ngOnInit() {
    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl("https://localhost:44323/notificationhub")
      .build();

    connection.start().then(function () {
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    connection.on("JobStarted", () => {
      //this.getEmployeeData();
    }); 

}
  message: string = '';
  show: boolean = false;
  verbosity: Verbosity;

  display(message: string, verbosity: Verbosity = 0) {
    this.message = message;
    this.show = true;
    this.verbosity = verbosity;
  }

  hide() {
    this.show = false;
  }

  clear() {
    this.message = '';
    this.show = false;
  }

}

export enum Verbosity {
  Info = 0,
  Success = 1,
  Warning = 2,
  Error = 3,
}
