import { EventEmitter, Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { AlertModel } from '.';

/*
this.alertReceived.messageReceived.subscribe((message: MessageModel) => {
    alert(message.body);
});
*/

@Injectable()
export class AlertService {
  alertReceived = new EventEmitter<AlertModel>();

  constructor() { }

  show(data: AlertModel) {
    this.alertReceived.emit(data);
  }  
}    
