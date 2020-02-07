import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxSpinnerModule } from "ngx-spinner";
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ElementComponent } from './element/element.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MonitorComponent } from './monitor/monitor.component';
import { JobComponent } from './job/job.component';
import { JodDetailComponent } from './jod-detail/jod-detail.component';
import { MessageComponent } from './message/message.component';
import { JobPageComponent } from './job-page/job-page.component';
import { TaskPreviewComponent } from './task-preview/task-preview.component';
import { TriggerPreviewComponent } from './trigger-preview/trigger-preview.component';
import { AddEditTaskModalComponent } from './add-edit-task-modal/add-edit-task-modal.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ReactiveFormsModule } from '@angular/forms';
import { ErrorIntercept } from './error.interceptor';
import { ToastComponent } from './toast/toast.component';
import { TaskPageComponent } from './task-page/task-page.component';
import { ElementPreviewComponent } from './element-preview/element-preview.component';
import { AddElementModalComponent } from './add-element-modal/add-element-modal.component';
import { EditElementModalComponent } from './edit-element-modal/edit-element-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ElementComponent,
    DashboardComponent,
    MonitorComponent,
    JobComponent,
    JodDetailComponent,
    MessageComponent,
    JobPageComponent,
    TaskPreviewComponent,
    TriggerPreviewComponent,
    AddEditTaskModalComponent,
    ToastComponent,
    TaskPageComponent,
    ElementPreviewComponent,
    AddElementModalComponent,
    EditElementModalComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    NgxSpinnerModule,
    NgxGraphModule,
    NgxChartsModule,
    NgbModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: DashboardComponent, pathMatch: 'full' },
      { path: 'monitor', component: MonitorComponent },
      { path: 'job/:id', component: JobPageComponent },
      { path: 'task/:id', component: TaskPageComponent },
    ])
  ],
  entryComponents: [
    AddEditTaskModalComponent,
    AddElementModalComponent,
    EditElementModalComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorIntercept,
      multi: true
    }
    
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
