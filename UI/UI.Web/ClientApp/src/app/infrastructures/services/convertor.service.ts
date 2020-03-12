import { Injectable, Inject } from "@angular/core";
import * as moment from 'jalali-moment';

@Injectable()
export class ConvertorService {

  constructor(@Inject('RESOURCE') public resource: any) { }

  public toBoolean(value: boolean, yes: string = 'yes', no: string = 'no'): string {
    return value ? this.resource.default[yes] : this.resource.default[no];
  }

  public toJalali(value: any) {
    let MomentDate = moment(value, 'YYYY/MM/DD');
    return MomentDate.locale('fa').format('YYYY/M/D');
  }
}
