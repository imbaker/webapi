import { Component, Injectable } from '@angular/core';
import { NgbCalendar, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {

  model: NgbDateStruct;

  dates: NgbDateStruct[] = [null];

  diffDays: number;
  diffDaysSuffix: string;

  constructor(private calendar: NgbCalendar) { }

  addDate() {
    this.dates.push(null);
  }

  deleteDate(value: any) {
    console.log("deleteData fired" + value);
  }

  setCookie(name, value, days) {
  var expires = "";
  if (days) {
    var date = new Date();
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    expires = "; expires=" + date.toUTCString();
  }
  document.cookie = name + "=" + (value || "") + expires + "; path=/";
  }
}
