import { Component, Injectable } from '@angular/core';
import { NgbCalendar, NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

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

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [{ provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }]
})
export class HomeComponent {

  model: NgbDateStruct;

  date: { year: number, month: number };

  diffDays: number;
  diffDaysSuffix: string;

  constructor(private calendar: NgbCalendar) { }

  click() {
    const targetDate = <any>new Date(this.model.year, this.model.month - 1, this.model.day);
    const today = <any>new Date(this.calendar.getToday().year, this.calendar.getToday().month - 1, this.calendar.getToday().day)

    this.diffDays = (today - targetDate) / (1000 * 60 * 60 * 24);

    if (this.diffDays > 0)
      this.diffDaysSuffix = " ago"

    if (this.diffDays < 0) {
      this.diffDaysSuffix = " in the future"
      this.diffDays = Math.abs(this.diffDays);
    }
  }
}
