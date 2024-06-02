import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotiificationsComponent } from './notiifications.component';

describe('NotiificationsComponent', () => {
  let component: NotiificationsComponent;
  let fixture: ComponentFixture<NotiificationsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NotiificationsComponent]
    });
    fixture = TestBed.createComponent(NotiificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
