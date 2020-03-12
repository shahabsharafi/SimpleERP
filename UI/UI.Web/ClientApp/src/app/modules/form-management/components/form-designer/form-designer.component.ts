import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  templateUrl: './form-designer.component.html',
  styleUrls: ['./form-designer.component.css']
})
export class FormDesignerComponent {
  public selectedItem: any;
  public form: FormGroup;
  unsubcribe: any;
  properties: any;

  public fields: any[] = [
    {
      name: 'firstName',
      label: 'First Name',
      type: 'text',
      value: '',
      required: 'true',
    },
    {
      name: 'lastName',
      label: 'Last Name',
      type: 'text',
      value: '',
      required: 'false',
    },
    {
      name: 'email',
      label: 'Email',
      type: 'text',
      value: '',
      required: 'false',
    },

    {
      name: 'picture',
      label: 'Picture',
      type: 'file',
      required: 'false',
      onUpload: this.onUpload.bind(this)
    },
    {
      name: 'country',
      label: 'Country',
      type: 'dropdown',
      value: 'us',
      required: 'false',
      options: [
        { key: 'in', label: 'India' },
        { key: 'us', label: 'USA' }
      ]
    },
    {
      name: 'jender',
      label: 'Jender',
      type: 'radio',
      value: '',
      required: 'true',
      options: [
        { key: 'm', label: 'Male' },
        { key: 'f', label: 'Female' }
      ]
    },
    {
      name: 'hobby',
      label: 'Hobby',
      type: 'checkbox',
      value: 'f',
      required: 'true',
      options: [
        { key: 'f', label: 'Fishing' },
        { key: 'c', label: 'Cooking' }
      ]
    }
  ];

  constructor() {
    this.properties = null;
    this.form = new FormGroup({
      fields: new FormControl(JSON.stringify(this.fields))
    })
    this.unsubcribe = this.form.valueChanges.subscribe((update) => {
      console.log(update);
      this.fields = JSON.parse(update.fields);
    });
  }

  onUpload(e) {
    console.log(e);

  }

  getFields() {
    return this.fields;
  }

  getProperties() {
    return this.properties;
  }

  ngDistroy() {
    this.unsubcribe();
  }

  listClick(event: any, item: any): void {
    this.selectedItem = item;
    this.properties = [
      {
        name: 'type',
        label: 'Type',
        type: 'dropdown',
        value: item.type,
        required: 'true',
        options: [
          { key: 'text', label: 'text' },
          { key: 'radio', label: 'radio' },
          { key: 'checkbox', label: 'checkbox' },
          { key: 'dropdown', label: 'dropdown' },
          { key: 'file', label: 'file' }
        ]
      },
      {
        name: 'name',
        label: 'Name',
        type: 'text',
        value: item.name,
        required: 'true'
      },
      {
        name: 'label',
        label: 'Label',
        type: 'text',
        value: item.label,
        required: 'true'
      },
      {
        name: 'value',
        label: 'Value',
        type: 'text',
        value: item.value,
        required: 'false'
      },
      {
        name: 'required',
        label: 'Required',
        type: 'radio',
        value: item.required,
        required: 'true',
        options: [
          { key: 'true', label: 'Is required' },
          { key: 'false', label: 'Is not required' }
        ]
      },
      {
        name: 'options',
        label: 'Options',
        type: 'text',
        value: JSON.stringify(item.options) || '',
        required: 'false',
        multiline: 'true',        
      }
    ];
  }
}
