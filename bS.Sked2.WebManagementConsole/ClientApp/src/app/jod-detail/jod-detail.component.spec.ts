import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JodDetailComponent } from './jod-detail.component';

describe('JodDetailComponent', () => {
  let component: JodDetailComponent;
  let fixture: ComponentFixture<JodDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JodDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JodDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
