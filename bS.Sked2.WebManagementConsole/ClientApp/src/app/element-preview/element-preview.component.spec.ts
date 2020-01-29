import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ElementPreviewComponent } from './element-preview.component';

describe('ElementPreviewComponent', () => {
  let component: ElementPreviewComponent;
  let fixture: ComponentFixture<ElementPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ElementPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ElementPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
