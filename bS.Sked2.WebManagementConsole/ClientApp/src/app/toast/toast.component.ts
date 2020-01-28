import { Component, TemplateRef } from '@angular/core';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-toasts',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css'],
  host: { '[class.ngb-toasts]': 'true' }
})
export class ToastComponent {
  constructor(private messageService: MessageService) { }

  isTemplate(toast) { return toast.textOrTpl instanceof TemplateRef; }
}
