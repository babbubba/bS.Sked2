import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TasksService } from '../tasks.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Task } from '../models/task';
import { ElementService } from '../element.service';
import { TaskElement } from '../models/taskElement';
import * as shape from 'd3-shape';
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddElementModalComponent } from '../add-element-modal/add-element-modal.component';

@Component({
  selector: 'app-task-page',
  templateUrl: './task-page.component.html',
  styleUrls: ['./task-page.component.css']
})
export class TaskPageComponent implements OnInit {

  constructor(
    private routeParams: ActivatedRoute,
    private taskService: TasksService,
    private elementService: ElementService,
    private spinnerService: NgxSpinnerService,
    private modalService: NgbModal
  ) { }

  task: Task;
  elements: TaskElement[];
  hierarchialGraph = { nodes: [], links: [] };
  curve = shape.curveLinear;
  update$: Subject<boolean> = new Subject();
  disableAddElementButton: boolean;

  ngOnInit() {
    this.loadData();
  }

  private loadData() {
    this.spinnerService.show();
    this.routeParams.params.subscribe(
      params => {
        let taskId = params['id'];
        this.taskService.getTask(taskId)
          .subscribe(result => {
            this.task = result;
            this.elementService.getTaskElements(taskId)
              .subscribe(result => {
                this.elements = result;
                this.drawFlow();
                this.spinnerService.hide();
              });
          });
      },
      error => {
        this.spinnerService.hide();
        console.error(error);
      });
  }

  drawFlow() {
    var nodes = [];
    var links = [];
    this.elements.forEach(function (element) {
      if (element.type.key == 'ElementsLink') {
        let link = {
          source: element.previousId,
          target: element.nextId,
          label: element.name,
          elementId: element.id
        };
        links.push(link);
      }
      else {
        let node = {
          id: element.id,
          label: element.name,
          description: element.description,
          position: element.position,
          color: 'white',
          textColor: 'black',
          strokeWidth: '4',
          strokeColor: 'green'
        };
        nodes.push(node);
      }
    });
    this.hierarchialGraph.nodes = nodes;
    this.hierarchialGraph.links = links;
  }

  updateGraph() {
    this.update$.next(true)
  }

  onNodeClick(elementId) {
    var element = this.elements.find(x => x.id == elementId)
    alert(element.name);
  }

  onLinkClick(linkId) {
    var element = this.elements.find(x => x.id == linkId)
    alert(element.name);
  }

  addNewElement() {
    this.elementService.getElementCreate()
      .subscribe(
        result => {
          result.parentTaskId = this.task.id;
          const modalAddNewElement = this.modalService.open(AddElementModalComponent);
          modalAddNewElement.componentInstance.element = result;
          modalAddNewElement.result
            .then(res => {
              if (res === "Success") {
                //save or create success then reload data
                this.loadData();
              }
            })
            .catch(err => { });
        },
        error => { });
  }
}
