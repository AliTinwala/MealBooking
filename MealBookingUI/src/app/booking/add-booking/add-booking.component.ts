import { Component } from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {FloatLabelType} from '@angular/material/form-field';
import {FormGroup, NgForm} from '@angular/forms';


@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrls: ['./add-booking.component.css']
})
export class AddBookingComponent {

  minDate: Date;
  startDate: Date;
  endDate: Date;
  numberOfDays: number | null = null;
  range: FormGroup;
  addBookingFrom: FormGroup;
  mealType: FormControl

  constructor(private _formBuilder: FormBuilder) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date();
    this.startDate = new Date();
    this.endDate = new Date();
    this.numberOfDays = 0;
    this.mealType = new FormControl();
    this.range = this._formBuilder.group({
      start: [null],
      end: [null]
    });
    this.addBookingFrom = this._formBuilder.group({
      mealType: [null],
      start: [null],
      end: [null]     
    })

    this.range.valueChanges.subscribe(value =>{
      if(value.start && value.end){
        this.startDate = new Date(value.start);
        this.endDate = new Date(value.end);
        this.numberOfDays = this.calculateDaysBetween(this.startDate,this.endDate);
      }
      else
      {
        this.numberOfDays = null;
      }
    });
  }

  calculateDaysBetween(start: Date, end: Date): number {
    let count = 0;
    const current = new Date(start);

    while (current <= end) {
      const day = current.getDay();
      if (day !== 0 && day !== 6) { // Skip Sunday (0) and Saturday (6)
        count++;
      }
      current.setDate(current.getDate() + 1);
    }

    return count;
  }
  
  hideRequiredControl = new FormControl(false);
  floatLabelControl = new FormControl('auto' as FloatLabelType);
  options = this._formBuilder.group({
    hideRequired: this.hideRequiredControl,
    floatLabel: this.floatLabelControl,
  });


  myFilter = (d: Date | null): boolean => {
    const day = (d || new Date()).getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
  };

  getFloatLabelValue(): FloatLabelType {
    return this.floatLabelControl.value || 'auto';
  }
  
  onAddBooking(): void
  {
    console.log(this.addBookingFrom.value.mealType);
    console.log(this.startDate,this.endDate);
  }
}
