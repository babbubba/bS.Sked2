import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TriggerPreviewComponent } from './trigger-preview.component';

describe('TriggerPreviewComponent', () => {
  let component: TriggerPreviewComponent;
  let fixture: ComponentFixture<TriggerPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TriggerPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TriggerPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
