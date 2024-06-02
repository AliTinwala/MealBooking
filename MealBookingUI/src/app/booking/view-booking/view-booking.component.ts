import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/shared/booking/booking.service';

@Component({
  selector: 'app-view-booking',
  templateUrl: './view-booking.component.html',
  styleUrls: ['./view-booking.component.css'],
})
export class ViewBookingComponent implements OnInit{
  constructor(public bookingService: BookingService) { }
  dataSource: any = [];
  displayedColumns: string[] = ['Booking Type', 'Booking Date', 'Booking For Date'];
  async ngOnInit(): Promise<void> {
    (await this.bookingService.getBookings()).subscribe(data =>
    {
      this.dataSource = data;
    });
  }
}
