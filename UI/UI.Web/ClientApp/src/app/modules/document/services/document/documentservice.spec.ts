import { async } from '@angular/core/testing';
import { ContractService } from './contract.service';
import { ApiResult } from '../../../../infrastructures';
import { ContractModel } from '../../models';
import { of, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

class RestFackService extends HttpClient {

  constructor() { super(null) }

  private obj: any = new ApiResult(true, 200, '');

  get<T>(url: string, options?: any): Observable<T> {
    return of(null);
  }

  post<T>(url: string, body: any | null, options?: any): Observable<T> {
    return of(this.obj as T);
  }

  put<T>(url: string, body: any | null, options?: any): Observable<T> {
    return of(this.obj as T);
  }

  delete<T>(url: string, options?: any): Observable<T> {
    return of(this.obj as T);
  }
}

describe('ContractService', () => {

  let restService: RestFackService;

  beforeEach(async(() => {
    restService = new RestFackService();
  }));

  it(`check getAll`, () => {
    const get_Spy = spyOn(restService, 'get').and.callThrough();
    const contractService = new ContractService(restService, '');
    contractService.getAll(null);
    expect(get_Spy).toHaveBeenCalled();
  });

  it(`check get`, () => {
    const get_Spy = spyOn(restService, 'get').and.callThrough();
    const contractService = new ContractService(restService, '');
    contractService.get(0);
    expect(get_Spy).toHaveBeenCalled();
  });

  it(`check insert`, () => {
    const post_Spy = spyOn(restService, 'post').and.callThrough();
    const contractService = new ContractService(restService, '');
    contractService.save(new ContractModel(null, null, '', '', false, new Date()));
    expect(post_Spy).toHaveBeenCalled();
  });

  it(`check update`, () => {
    const put_Spy = spyOn(restService, 'put').and.callThrough()
    const contractService = new ContractService(restService, '');
    contractService.save(new ContractModel(null, 1, '', '', false, new Date()));
    expect(put_Spy).toHaveBeenCalled();    
  });

  it(`check delete`, () => {
    const delete_Spy = spyOn(restService, 'delete').and.callThrough()
    const contractService = new ContractService(restService, '');
    contractService.delete(0);
    expect(delete_Spy).toHaveBeenCalled();
  });
});
