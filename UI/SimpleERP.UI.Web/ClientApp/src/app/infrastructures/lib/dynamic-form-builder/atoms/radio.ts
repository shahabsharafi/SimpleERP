import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'radio',
    template: `
      <div [formGroup]="form">
        <div class="form-check" *ngFor="let opt of field.options">
          <input class="form-check-input" type="radio" [value]="opt.key" [formControlName]="field.name" [(ngModel)]="field.value">
          <label class="form-check-label">
            {{opt.label}}
          </label>
        </div>
      </div>
    `,
    styles: [
      `
      .form-check-input[type=radio] {
        margin-right: 0;
      }
      `
    ]
})
export class RadioComponent {
    @Input() field:any = {};
    @Input() form:FormGroup;
}
