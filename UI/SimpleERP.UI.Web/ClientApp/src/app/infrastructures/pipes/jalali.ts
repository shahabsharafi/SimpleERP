import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'jalali-moment';

/**
 * convert to jalali calandar
 * Takes an date argument.
 * Usage:
 *   date | jalali
 * Example:
 *   {{ new Date() | jalali }}
*/

@Pipe({
  name: 'jalali'
})
export class JalaliPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    let MomentDate = moment(value, 'YYYY/MM/DD');
    return MomentDate.locale('fa').format('YYYY/M/D');
  }
}

export class ConvertDate {

  public static toGeregorian(d: string): string {
    let m = moment(d, 'jYYYY-jMM-jDD');
    return m.locale('en').format('YYYY-MM-DD');
  }

  public static toJalali(d: string): string {
    d = d.replace(/[ap]m$/i, '');
    let m = moment(new Date(d));
    return m.locale('fa').format('YYYY-MM-DD');
  }
}
