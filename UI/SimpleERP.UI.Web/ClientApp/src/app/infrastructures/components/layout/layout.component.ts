import { Component, Inject } from '@angular/core';
import { SystemMessage, SystemMessageEventParameters } from '../../services';
@Component({
  selector: 'app-root',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent {
  constructor(@Inject('RESOURCE') public resource: any, private systemMessage: SystemMessage) {
    this.systemMessage.messageReceived.subscribe((params: SystemMessageEventParameters) => { this.handleMessageReceived(params) });
    this.systemMessage.notificationReceived.subscribe((text: string) => { this.handleNotificationReceived(text) });
  }

  private messageInterval: any;
  public message: SystemMessageEventParameters = null;
  private notificationsInterval: any;
  public notifications: string[] = null;
  public collapsed: boolean = true;

  public toggleCollapsed(): void {
    this.collapsed = !this.collapsed;
  }

  private handleNotificationReceived(text: string): void {
    if (this.notifications == null)
      this.notifications = [];
    this.notifications.unshift(text);
    if (this.notifications.length > 4)
      this.notifications.pop();
    if (this.notificationsInterval == null) {
      let me = this;
      this.notificationsInterval = setInterval(function () {
        if (me.notifications != null && me.notifications.length > 0) {
          me.notifications.pop();
        }
        if (me.notifications == null || me.notifications.length < 1) {
          me.notifications = null;
          clearTimeout(me.notificationsInterval);
          me.notificationsInterval = null;
        }
      }, 2000);
    }              
  }

  private handleMessageReceived(params: SystemMessageEventParameters): void {
    this.message = params;
    if (params.type !== 'confirm' && params.type !== 'alert') {
      if (this.messageInterval != null)
        clearTimeout(this.messageInterval);
      let me = this;
      this.messageInterval = setTimeout(function () {
        me.message = null;
        me.messageInterval = null;
      }, 2000);
    }    
  }

  public messageCallback(isSuccess: boolean): void {
    this.message.callback.emit(isSuccess);
    this.message = null;
  }
}
