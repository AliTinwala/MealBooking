import { Component } from '@angular/core';
import { NativeDateAdapter } from '@angular/material/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [NativeDateAdapter]
})
export class AppComponent {
  title = 'MealBookingUI';
}
