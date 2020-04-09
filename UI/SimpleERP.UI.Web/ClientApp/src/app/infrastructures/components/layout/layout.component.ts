import { Component, Inject } from '@angular/core';
import { SystemMessage, SystemMessageEventParameters } from '../../services';
@Component({
  selector: 'app-root',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent {
  constructor(@Inject('RESOURCE') public resource: any, private systemMessage: SystemMessage) {
    this.systemMessage.messageReceived.subscribe((data: SystemMessageEventParameters) => { this.handleMessageReceived(data) });    
  }

  private messageInterval: any;
  public message: SystemMessageEventParameters;
  public collapsed: boolean = true;

  public toggleCollapsed(): void {
    this.collapsed = !this.collapsed;
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
