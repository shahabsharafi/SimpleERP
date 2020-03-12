import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'checkbox',
    template: `
      <div [formGroup]="form">
        <div [formGroupName]="field.name" >
          <div *ngFor="let opt of field.options" class="form-check form-check">
          <label class="form-check-label">
             <input [formControlName]="opt.key" class="form-check-input" type="checkbox" [value]="opt.key" [checked]="field.value.indexOf(opt.key) > -1" (change)="this.toggleSelection(opt.key)" />
             {{opt.label}}</label>
          </div>
        </div>

      </div> 
    `
})
export class CheckBoxComponent {
    @Input() field:any = {};
    @Input() form:FormGroup;
    get isValid() { return this.form.controls[this.field.name].valid; }
    get isDirty() { return this.form.controls[this.field.name].dirty; }

  toggleSelection(key) {
    let val = this.field.value.split(',') || [];

    let idx = val.indexOf(key);

    // Is currently selected
    if (idx > -1) {
      val.splice(idx, 1);
    }

    // Is newly selected
    else {
      val.push(key);
    }
    this.field.value = val.length === 0 ? '' : (val.length === 1 ? val[0] : val.join(','));
  };
}
