import * as resource from '../resource.json';

export class QueryString {

  private static _serialize(obj: any, prefix: any | null): string {
    let str = [], p;
    for (p in obj) {
      if (obj.hasOwnProperty(p)) {
        const k = prefix ? prefix + "[" + p + "]" : p,
          v = obj[p];
        const r = (v !== null && typeof v === "object")
          ? QueryString._serialize(v, k)
          : encodeURIComponent(k) + "=" + encodeURIComponent(v);
        if (r)
          str.push(r);
      }
    }
    return str.join("&");
  }
  static serialize(obj: any): string {
    return QueryString._serialize(obj, null);
  }
}
