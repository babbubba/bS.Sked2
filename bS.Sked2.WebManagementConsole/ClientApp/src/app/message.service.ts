import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor() { }

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
