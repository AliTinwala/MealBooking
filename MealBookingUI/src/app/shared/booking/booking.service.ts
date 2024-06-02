import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Booking } from './booking.model';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private myHttp:HttpClient) { }
  
  bookingUrl:string = "https://localhost:7108/api/Booking";
  bookingList:Booking[]=[];
  bookingData:Booking = new Booking();

  saveBooking()
  {
    return this.myHttp.post(this.bookingUrl,this.bookingData);
  }
  updateBooking()
  {
    return this.myHttp.put(`${this.bookingUrl}/${this.bookingData.bookingId}`,this.bookingData);
  }
  async getBookings(): Promise<Observable<Booking[]>>
  {
    return await this.myHttp.get<Booking[]>(this.bookingUrl);
  }
  deleteBooking(userId:string)
  {
    return this.myHttp.delete(`${this.bookingUrl}/${userId}`);
  }
  getBookedDates(user_id: string): Observable<Date[]>
  {
    return this.myHttp.get<Date[]>(`${this.bookingUrl}/${user_id}`);
  }
}
