export class CustomValidators {
  static multipleCheckboxRequireOne(fa: any) {
    let valid = false;

    if (fa.value) {
      for (let p in fa.value) {
        if (fa.value[p]) {
          valid = true;
          break;
        }
      }
    }

    return valid ? null : {
      multipleCheckboxRequireOne: true
    };
  }
}
