import { Component, OnInit, Input } from '@angular/core';
import { TaskElement } from '../models/taskElement';

@Component({
  selector: 'app-element-preview',
  templateUrl: './element-preview.component.html',
  styleUrls: ['./element-preview.component.css']
})
export class ElementPreviewComponent implements OnInit {

  constructor() { }

  @Input() element: TaskElement;

  ngOnInit() {
  }

}
