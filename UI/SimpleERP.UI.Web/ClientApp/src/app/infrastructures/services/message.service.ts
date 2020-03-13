import { EventEmitter, Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { MessageModel } from '.';

@Injectable()
export class MessageService {
  messageReceived = new EventEmitter<MessageModel>();
  connectionEstablished = new EventEmitter<Boolean>();

  private _hubConnection: HubConnection;

  constructor(@Inject('IDENTITY_SERVICE_URL') private baseUrl: string) {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  //sendMessageToServer(message: MessageModel) {
  //  this._hubConnection.invoke('sendMessageToServer', message.body);
  //}

  private createConnection() {
    const url = this.baseUrl + 'MessageHub';//'https://localhost:5101/MessageHub';
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(url)
      .build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(() => { this.startConnection(); }, 5000);
      });
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('sendMessageToClient', (data: any) => {
      this.messageReceived.emit(data);
    });
  }
}    
