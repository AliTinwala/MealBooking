import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { Notiification } from './notification.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private myHttp:HttpClient) { } 

  notificationUrl = "https://localhost:7108/api/Notification";
  notificationList:Notification[] = [];
  notificationData = new Notiification();

  async getMessages(user_id: string): Promise<Observable<string[]>>
  {
    return await this.myHttp.get<string[]>(`${this.notificationUrl}/${user_id}`);
  }
  async setReadMessage(notification_id: string, notification_data:any): Promise<any>
  {
    try
    {
      return await this.myHttp.put<any>(`${this.notificationUrl}/${notification_id}`,notification_data);
    }
    catch(error)
    {
      console.error('Error fetching notification:', error);
      throw error;
    }
    
  }
  async addNotification(notification_data:any): Promise<Observable<any>>
  {
    try
    {
      const user_id: string = "FC6DA930-9CCB-4A1B-B6BD-08DC8130704B";
      const requestData = {...notification_data}
      return await this.myHttp.post<any>(`${this.notificationUrl}/${user_id}`,requestData);
    }
    catch(error)
    {
      throw error;
    }
  }
}
