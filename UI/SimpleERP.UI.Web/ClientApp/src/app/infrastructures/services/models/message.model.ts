export class IMessageModel {
  sender: string;
  reciver: string;
  title: string;
  body: string;
}


export class MessageModel implements IMessageModel {
  constructor(
    public sender: string,
    public reciver: string,
    public title: string,
    public body: string
  ) { }
}
