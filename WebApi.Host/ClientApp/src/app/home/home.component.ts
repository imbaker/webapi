import { Component, Injectable } from '@angular/core';
import { NgbCalendar, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {

  model: NgbDateStruct;

  dates: NgbDateStruct[] = [];

  diffDays: number;
  diffDaysSuffix: string;

  constructor(private calendar: NgbCalendar) { }

  ngOnInit() {
    this.loadDates();
  }

  trackDate(index, date) {
    console.log(index, date);
    return index;
  }

  addDate() {
    this.dates.push(null);
  }

  updateDate(value: any) {
    this.dates[value.id] = value.date;
  }

  deleteDate(value: any) {
    console.log("deleteData fired" + value);
    this.dates.splice(value, 1);
    console.log(this.dates);
  }

  saveDates() {
    for (var i = 0; i < this.dates.length; i++)
    {
      console.log("Busy doing nothing" + this.dates[i].toString());
      localStorage.setItem(`date${i}`, JSON.stringify(this.dates[i]));
    }
    this.setCookie("datesCookie", this.dates.toString(), 7);
  }

  loadDates() {
    var i = 0;
    while (localStorage.getItem(`date${i}`) !== null) {
      this.dates.push(JSON.parse(localStorage.getItem(`date${i}`)));
      i++;
    }
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
