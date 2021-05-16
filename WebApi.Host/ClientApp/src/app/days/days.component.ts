import { Component } from '@angular/core';
import { NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { LocalStorageService } from '../services/local-storage.service';
import { library, dom } from '@fortawesome/fontawesome-svg-core';
import { fas, faTrashAlt, faCalendarDay, faSave } from '@fortawesome/free-solid-svg-icons';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidatorFn } from '@angular/forms';
import { ToastService } from '../services/toast.service';
import { CustomDateParserFormatter } from '../services/custom-date-parser-formatter.component';


@Component({
  selector: 'app-home',
  templateUrl: './days.component.html',
  styleUrls: ['./days.component.css'],
  providers: [{ provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }]
})

export class DaysComponent {

  model: NgbDateStruct;

  form = new FormGroup({ dates: new FormArray([]) });

  constructor(private localStorageService: LocalStorageService, private toastService: ToastService) {
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
    const targetDate = <any>new Date(date.value.year, date.value.month - 1, date.value.day);
    const today = <any>new Date();

    if (targetDate == today)
      return "";

    return (targetDate < today) ? " ago" : " in the future";
  }

  clickDate(date: any) {
    this.getDiffDaysSuffix(date);
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
