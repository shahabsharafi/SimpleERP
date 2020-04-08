import { Component, Inject } from '@angular/core';
import { AlertService } from '../../services/alert.service';
import { AlertModel } from '../..';

@Component({
  selector: 'app-root',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent {
  constructor(@Inject('RESOURCE') public resource: any, private alertService: AlertService) {
    this.alertService.alertReceived.subscribe((data: AlertModel) => {
      this.allertShow = true;
      this.allertType = data.type;
      this.allertText = data.text;
      if (this.lastAlertInterval != null)
        clearTimeout(this.lastAlertInterval);
      let me = this;
      this.lastAlertInterval = setTimeout(function () {
        me.allertShow = false;
        me.allertType = '';
        me.allertText = '';
        me.lastAlertInterval = null;
      }, 2000);
    });
  }

  private lastAlertInterval: any;
  public allertType: string;
  public allertText: string;
  public allertShow: boolean = false;
  public collapsed: boolean = true;

  public toggleCollapsed(): void {
    this.collapsed = !this.collapsed;
  }

}
