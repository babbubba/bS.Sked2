import { Component, OnInit, Input } from '@angular/core';
import { Trigger } from '../models/trigger';

@Component({
  selector: 'app-trigger-preview',
  templateUrl: './trigger-preview.component.html',
  styleUrls: ['./trigger-preview.component.css']
})
export class TriggerPreviewComponent implements OnInit {

  constructor() { }

  @Input() trigger: Trigger;


  ngOnInit() {
  }

}
