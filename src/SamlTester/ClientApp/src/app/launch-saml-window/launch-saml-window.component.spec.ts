import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LaunchSamlWindowComponent } from './launch-saml-window.component';

describe('LaunchSamlWindowComponent', () => {
  let component: LaunchSamlWindowComponent;
  let fixture: ComponentFixture<LaunchSamlWindowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LaunchSamlWindowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LaunchSamlWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
