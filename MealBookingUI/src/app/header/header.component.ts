import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { NotificationService } from 'src/app/shared/notification/notification.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  constructor(public notificationService: NotificationService) { }

  dataSource: any = [];
  count : number = 0;
  i:number = 0;
  unread:number = 0;
  user_id: string = "FC6DA930-9CCB-4A1B-B6BD-08DC8130704B";
  displayedColumns: string[] = ['Notification Messages', 'Is Read'];

  async ngOnInit(): Promise<void> {
    (await this.notificationService.getMessages(this.user_id)).subscribe(data =>
      {
        this.dataSource = data;
        this.count = this.dataSource.length;
        for(this.i=0;this.i<this.count;this.i++)
        {
          if(this.dataSource[this.i].isRead == false)
          {
            this.unread += 1;
          }
        }
      });
  }
  
}
