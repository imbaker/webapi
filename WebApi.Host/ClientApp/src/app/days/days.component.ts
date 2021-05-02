import { Component, Injectable, Pipe, PipeTransform } from '@angular/core';
import { NgbCalendar, NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { LocalStorageService } from '../services/local-storage.service';
import { library, dom } from '@fortawesome/fontawesome-svg-core';
import { fas, faTrashAlt, faCalendarDay, faSave } from '@fortawesome/free-solid-svg-icons';


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
  providers: [{ provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }]
})

export class DaysComponent {

  model: NgbDateStruct;

  dateItems: { date: NgbDateStruct, diffDays: number, diffDaysSuffix: string }[] = [];

  constructor(private calendar: NgbCalendar, private localStorageService: LocalStorageService) {
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
    this.dateItems.push({ date: null, diffDays: null, diffDaysSuffix: null });
  }

  deleteDate(value: any) {
    this.dateItems.splice(value, 1);
  }

  saveDates() {
    let dates = this.dateItems.map(item => ({ year: item.date.year, month: item.date.month, day: item.date.day }));
    this.localStorageService.set('dates', dates);
  }

  loadDates() {
    let dates = this.localStorageService.get('dates') ?? [{ date: null, diffDays: null, diffDaysSuffix: null }];
    this.dateItems = dates.map(item => ({ date: { year: item.year, month: item.month, day: item.day }, diffDays: Math.abs(this.getDiffDays(item)), diffDaysSuffix: "--" }));
  }

  getDiffDays(date: any) {
    const targetDate = <any>new Date(date.year, date.month - 1, date.day);
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

    const targetDate = <any>new Date(this.dateItems[index].date.year, this.dateItems[index].date.month - 1, this.dateItems[index].date.day);
    const today = <any>new Date();

    let diffDays = Math.floor((today - targetDate) / (1000 * 60 * 60 * 24));

    let diffDaysSuffix = "";

    if (diffDays > 0)
      diffDaysSuffix = " ago"

    if (diffDays < 0) {
      diffDaysSuffix = " in the future"
      diffDays = Math.abs(diffDays);
    }

    this.dateItems[index].diffDaysSuffix = diffDaysSuffix;
    this.dateItems[index].diffDays = diffDays;
  }
}
