import { Component, Injectable, Pipe, PipeTransform } from '@angular/core';
import { NgbCalendar, NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { LocalStorageService } from '../services/local-storage.service';
import { library, dom } from '@fortawesome/fontawesome-svg-core';
import { fas, faTrashAlt, faCalendarDay, faSave } from '@fortawesome/free-solid-svg-icons';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidatorFn } from '@angular/forms';
import { ToastService } from '../services/toast.service';


@Injectable()
export class CustomDateParserFormatter extends NgbDateParserFormatter {
  readonly DELIMITER = '/';

  parse(value: string): NgbDateStruct | null {
    if (value) {
      let date = value.split(this.DELIMITER);
      return {
        day: parseInt(date[0], 10),
        month: parseInt(date[1], 10),
        year: parseInt(date[2], 10)
      };
    }
    return null;
  }

  format(date: NgbDateStruct | null): string {
    return date ? `${date.day}${this.DELIMITER}${date.month}${this.DELIMITER}${date.year}` : "";
  }
}

@Pipe({ name: 'abs' })
export class AbsPipe implements PipeTransform {
  transform(num: number, args?: any): any {
    return Math.abs(num);
  }
}

@Component({
  selector: 'app-home',
  templateUrl: './days.component.html',
  styleUrls: ['./days.component.css'],
  providers: [{ provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }]
})

export class DaysComponent {

  model: NgbDateStruct;

  form = new FormGroup({ dates: new FormArray([]) });

  constructor(private calendar: NgbCalendar, private localStorageService: LocalStorageService, public toastService: ToastService) {
    library.add(fas, faTrashAlt, faCalendarDay, faSave);
    dom.watch();
  }

  ngOnInit() {
    this.loadDates();
  }

  trackDate(index: number, date: any) {
    return index;
  }

  addDate() {
    (<FormArray>this.form.get('dates')).push(new FormControl(null, [this.dateValidator()]));
  }

  deleteDate(index: number) {
    (<FormArray>this.form.get('dates')).removeAt(index);
    this.form.markAsDirty();
  }

  onSubmit() {
    this.saveDates();
  }

  saveDates() {
    if (!this.form.valid) {
      this.toastService.show("Content not saved, one or more dates are invalid", { classname: 'bg-danger text-light', delay: 10000 })
      return;
    }

    let dates = (<FormArray>this.form.get('dates')).controls.map(item => item.value);
    this.localStorageService.set('dates', dates);
    this.toastService.show("Saved successfully", { classname: 'bg-success text-light', delay: 5000 });
    this.form.markAsPristine();
  }

  loadDates() {
    let dates = this.localStorageService.get('dates') ?? [];
    const form = this.form.get('dates') as FormArray;
    dates.forEach(item => form.push(new FormControl(item, [this.dateValidator()])));
  }

  getDiffDays(date: any) {
    if (date.value == null)
      return;
    const targetDate = <any>new Date(date.value.year, date.value.month - 1, date.value.day);
    const today = <any>new Date();

    return Math.floor((today - targetDate) / (1000 * 60 * 60 * 24));
  }

  getDiffDaysSuffix(date: any) {
    let diffDays = this.getDiffDays(date);

    if (diffDays == 0)
      return "";

    return (diffDays > 0) ? " ago" : " in the future";
  }

  click(index: number) {
    if (this.dateItems[index] === null)
      return;

    const targetDate = <any>new Date(this.dateItems[index].year, this.dateItems[index].month - 1, this.dateItems[index].day);
    const today = <any>new Date();

    let diffDays = Math.floor((today - targetDate) / (1000 * 60 * 60 * 24));

    let diffDaysSuffix = "";

    if (diffDays > 0)
      diffDaysSuffix = " ago"

    if (diffDays < 0) {
      diffDaysSuffix = " in the future"
      diffDays = Math.abs(diffDays);
    }
  }

  clickDate(date: any) {
    const targetDate = <any>new Date(date.value.year, date.value.month - 1, date.value.day);
    const today = <any>new Date();

    let diffDays = Math.floor((today - targetDate) / (1000 * 60 * 60 * 24));

    let diffDaysSuffix = "";

    if (diffDays > 0)
      diffDaysSuffix = " ago"

    if (diffDays < 0) {
      diffDaysSuffix = " in the future"
      diffDays = Math.abs(diffDays);
    }
  }

  dateValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      console.log(`In validator - ${control.value}`);
      if (control.value === null)
        return;
      const targetDate = <any>new Date(control.value.year, control.value.month - 1, control.value.day);
      let date = Date.parse(targetDate);
      return isNaN(date) ? { invalidDate: { value: control.value }} : null;
    }
  }
}
