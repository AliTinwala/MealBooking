import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddBookingComponent } from './booking/add-booking/add-booking.component';
import { ViewBookingComponent } from './booking/view-booking/view-booking.component';
import { ViewCalendarComponent } from './calendar/view-calendar/view-calendar.component';
import { SidenavComponent } from './shared/sidenav/sidenav.component';
import { NotiificationsComponent } from './user/notiifications/notiifications.component';

const routes: Routes = [
  { path: '', component: ViewCalendarComponent},
  { path: 'view-calendar', component: ViewCalendarComponent},
  { path: 'view-booking', component: ViewBookingComponent},
  { path: 'notifications', component:NotiificationsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const ArrayOfComponents = 
[
  ViewBookingComponent,
  AddBookingComponent,
  ViewCalendarComponent,
  SidenavComponent,
  NotiificationsComponent
]