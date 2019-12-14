import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RetrospectivesComponent } from './retrospectives.component';

describe('RetrospectivesComponent', () => {
  let component: RetrospectivesComponent;
  let fixture: ComponentFixture<RetrospectivesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RetrospectivesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RetrospectivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
