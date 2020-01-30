import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddElementModalComponent } from './add-element-modal.component';

describe('AddElementModalComponent', () => {
  let component: AddElementModalComponent;
  let fixture: ComponentFixture<AddElementModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddElementModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddElementModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
