import { Component, OnInit, Input } from '@angular/core';
import { Task } from '../models/task';

@Component({
  selector: 'app-task-preview',
  templateUrl: './task-preview.component.html',
  styleUrls: ['./task-preview.component.css']
})
export class TaskPreviewComponent implements OnInit {

  constructor() { }

  @Input() task: Task;


  ngOnInit() {
  }

}
