import { Component, Injectable, Input, Output, EventEmitter } from '@angular/core';
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
  selector: 'app-date',
  templateUrl: './date.component.html',
  providers: [{ provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }]
})

export class DateComponent {

  @Input() date: NgbDateStruct;
  @Input() example: string;
  @Output() deleteEvent: EventEmitter<any> = new EventEmitter<any>();

  diffDays: number;
  diffDaysSuffix: string;

  constructor(private calendar: NgbCalendar) { }

  ngOnInit() {
    console.log(this.date);
    console.log(this.example);
    this.click();
  }

  click() {
    if (this.date === null)
      return;

    const targetDate = <any>new Date(this.date.year, this.date.month - 1, this.date.day);
    const today = <any>new Date(this.calendar.getToday().year, this.calendar.getToday().month - 1, this.calendar.getToday().day)

    this.diffDays = (today - targetDate) / (1000 * 60 * 60 * 24);

    if (this.diffDays > 0)
      this.diffDaysSuffix = " ago"

    if (this.diffDays < 0) {
      this.diffDaysSuffix = " in the future"
      this.diffDays = Math.abs(this.diffDays);
    }
  }

  delete() {
    console.log("delete pressed");
    this.deleteEvent.emit();
  }
}
