import { Component,OnChanges,OnInit, SimpleChanges } from '@angular/core';
import { DateAdapter, NativeDateAdapter } from '@angular/material/core';
import { DatePipe } from '@angular/common';
import { BookingService } from 'src/app/shared/booking/booking.service';
import { Observable, map, skip } from 'rxjs';
import {MatCalendarCellClassFunction, MatCalendarCellCssClasses} from '@angular/material/datepicker';

@Component({
  selector: 'app-view-calendar',
  templateUrl: './view-calendar.component.html',
  styleUrls: ['./view-calendar.component.css'],
  providers: [NativeDateAdapter,DatePipe]
})
export class ViewCalendarComponent implements OnInit{ 
  selectedDate: Date | null = null;
  mealBooked: string = '';
  userId: string="3F28A14B-1A18-4A96-6D5F-08DC7FA558D2";
  bookedDates$: Observable<Date[]> | Date[] = [];
  isBooked: boolean = false;
  myClass: string = '';
  cssClass: string = "";
  
  constructor(public bookingService: BookingService,public datePipe: DatePipe, public dateAdapter: DateAdapter<Date>){ }
  

  ngOnInit(): void 
  {
    console.log("ngoninit");
    this.bookedDates$ = this.fetchBookedDates();
  }

  fetchBookedDates(): Observable<Date[]> | Date [] {
    console.log('fetchBookedDates');
    this.bookingService.getBookedDates(this.userId)
      .subscribe(response => {
        this.bookedDates$ = response;
      }, error => {
        console.error('Error fetching booked dates:', error);
      });
      return this.bookedDates$;
  }

  dateClass = (date:Date): MatCalendarCellCssClasses => 
  {
    console.log(this.bookedDates$);
    const formatDate = this.datePipe.transform(date,'yyyy-MM-dd');
    if( this.bookedDates$ instanceof Observable)
    {
      //Do nothing
    }
    else
    {
      console.log(this.bookedDates$);
      for (const date of this.bookedDates$) 
        {
          
          const formattedDate = this.datePipe.transform(date, 'yyyy-MM-dd');
          console.log(formatDate,formattedDate);
          if (formattedDate === formatDate) 
          {
            this.isBooked = true;
            console.log('true');
            break;
          }
          else if(formattedDate != formatDate)
          {
            this.isBooked = false;
            console.log('false');
          } 
      }
      if(this.isBooked === true)
      {
        this.cssClass = 'booking-dates';
        console.log('booking date');
      }
      else if(this.isBooked === false)
      {
        this.cssClass = '';
        // console.log('NO no booking date');
      }
    }
    return this.cssClass;
  }

  

  onDateChange(event: any): string  {
    if(event) 
    {
      this.selectedDate = event;
      const formatDate = this.datePipe.transform(this.selectedDate,'yyyy-MM-dd');
      if( this.bookedDates$ instanceof Observable)
      {
        //Do nothing
      }
      else
      {
        for (const date of this.bookedDates$) {
          const formattedDate = this.datePipe.transform(date, 'yyyy-MM-dd');
          if (formattedDate === formatDate) 
          {
            this.isBooked = true;
            //console.log('true');
            break;
          }
          else if(formattedDate != formatDate)
          {
            this.isBooked = false;
            //console.log('false');
          } 
        }
        if(this.isBooked === true)
        {
          this.mealBooked = 'Meal is booked'; 
        }
        else if(this.isBooked === false)
        {
          this.mealBooked = 'Meal not found';
        }
      }
    }
    else
    {
      console.error('error');
    }
    return this.mealBooked;
  }

  isSameDay(date1: Date, date2: Date) {
    return date1.getDate() === date2.getDate() &&
           date1.getMonth() === date2.getMonth() &&
           date1.getFullYear() === date2.getFullYear();
  }

  
}

