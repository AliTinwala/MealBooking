import { isFakeMousedownFromScreenReader } from '@angular/cdk/a11y';
import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/shared/notification/notification.service';


@Component({
  selector: 'app-notiifications',
  templateUrl: './notiifications.component.html',
  styleUrls: ['./notiifications.component.css']
})
export class NotiificationsComponent implements OnInit{
  constructor(public notificationService: NotificationService) { }
  dataSource: any = [];
  count : number = 0;
  i:number = 0;
  unread:number = 0;
  user_id: string = "FC6DA930-9CCB-4A1B-B6BD-08DC8130704B";
  displayedColumns: string[] = ['Notification Messages', 'Is Read', 'Action'];
  async ngOnInit(): Promise<void> {
    (await this.notificationService.getMessages(this.user_id)).subscribe(data =>
    {
      this.dataSource = data;
    });
  }

  async readNotification(element:any): Promise<void>
  {
    const id = element.notificationId;
    (await this.notificationService.setReadMessage(id,element)).subscribe(
      (response:string) => {
        console.log('Response: ',response);
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  async onChangePassword(): Promise<void>
  {
    const data = {
      message: 'Password Changed Successfully',
      isRead: false
    };
    (await this.notificationService.addNotification(data)).subscribe(
      (response:string) => {
        console.log('Response: ',response);
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
