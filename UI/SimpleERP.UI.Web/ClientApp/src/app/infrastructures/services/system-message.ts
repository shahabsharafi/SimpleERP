import { EventEmitter, Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
/*
this.messageReceived.messageReceived.subscribe((message: MessageModel) => {
    alert(message.body);
});
*/

@Injectable()
export class SystemMessage {
  public messageReceived = new EventEmitter<SystemMessageEventParameters>();

  constructor() { }

  public success(text: string): void {
    const params = new SystemMessageEventParameters('success', text);
    this.messageReceived.emit(params);
  }

  public comfirm(text: string, successed: any, failed: any = null): void {
    const params = new SystemMessageEventParameters('confirm', text);
    params.callback.subscribe((isSuccess: boolean) => {
      if (isSuccess) {
        if (successed != null) successed();
      } else {
        if (failed != null) failed();
      }
    })
    this.messageReceived.emit(params);
  }
}

export class SystemMessageEventParameters {
  constructor(
    public type: string,
    public text: string
  ) { }
  public callback = new EventEmitter<boolean>();
}
