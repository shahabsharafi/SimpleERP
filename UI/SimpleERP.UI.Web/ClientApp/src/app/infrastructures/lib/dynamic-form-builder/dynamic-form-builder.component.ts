import { Component, Input, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { CustomValidators } from './validator/custom-validator';

@Component({
  selector: 'dynamic-form-builder',
  template:`
    <form (ngSubmit)="onSubmit.emit(this.form.value)" [formGroup]="form" class="form-horizontal">
      <div *ngFor="let field of fields">
          <field-builder [field]="field" [form]="form"></field-builder>
      </div>
      <div class="form-row"></div>
      <div class="form-group row">
        <div class="col-md-3"></div>
        <div class="col-md-9">
          <button type="submit" [disabled]="!form.valid" class="btn btn-primary">{{resource.default.save}}</button>
        </div>
      </div>
    </form>
  `
})
export class DynamicFormBuilderComponent implements OnInit {
  @Output() onSubmit = new EventEmitter();
  form: FormGroup;
  constructor(@Inject('RESOURCE') public resource: any) { }

  ngOnInit() {

  }

  private _fields: any[] = [];

  @Input() set fields(value: any[]) {

    this._fields = value;
    let fieldsCtrls = {};
    for (let f of this._fields) {
      let ctl: any;
      if (f.type != 'checkbox') {
        if (f.required === 'true')
          ctl = new FormControl(f.value || '', Validators.required);
        else
          ctl = new FormControl(f.value || '');
      } else {
        let opts = {};
        for (let opt of f.options) {
          opts[opt.key] = new FormControl(opt.value);
        }
        ctl = new FormGroup(opts, CustomValidators.multipleCheckboxRequireOne);
      }
      fieldsCtrls[f.name] = ctl;
    }
    this.form = new FormGroup(fieldsCtrls);
  }

  get fields(): any[] {

    return this._fields;

  }
}
