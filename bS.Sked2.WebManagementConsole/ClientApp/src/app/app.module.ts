import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxSpinnerModule } from "ngx-spinner";  

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ElementComponent } from './element/element.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MonitorComponent } from './monitor/monitor.component';
import { JobComponent } from './job/job.component';
import { JodDetailComponent } from './jod-detail/jod-detail.component';
import { MessageComponent } from './message/message.component';
import { JobPageComponent } from './job-page/job-page.component';

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
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    NgxSpinnerModule,
    RouterModule.forRoot([
      { path: '', component: DashboardComponent, pathMatch: 'full' },
      { path: 'monitor', component: MonitorComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
