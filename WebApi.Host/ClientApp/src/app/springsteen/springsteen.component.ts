import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-springsteen-component',
  templateUrl: './springsteen.component.html'
})

export class SpringsteenComponent {
  concerts: {
    date: Date,
    location: string,
    tour: string,
    tracks?: any[],
    expanded: boolean
  }[] = [
      { date: new Date(1985, 6, 3), location: "Wembley Stadium, London", tour: "Born in the USA", tracks: [{ id: 1, title: "Born in the USA" }, { id: 2, title: "Badlands" }, { id: 3, title: "Darlington County" }, { id: 4, title: "Seeds" }, { id: 5, title: "Johnny 99" }, { id: 6, title: "Atlantic City" }, { id: 7, title: "The River" }, { id: 8, title: "Working On The Highway" }, { id: 9, title: "Trapped" }, { id: 10, title: "Out In The Street" }, { id: 11, title: "Glory Days" }, { id: 12, title: "The Promised Land" }, { id: 13, title: "My Hometown" }, { id: 14, title: "Thunder Road" }, { id: 15, title: "Cover Me" }, { id: 16, title: "Dancing In The Dark" }, { id: 17, title: "Hungry Heart" }, { id: 18, title: "Cadillac Ranch" }, { id: 19, title: "Downbound Train" }, { id: 20, title: "I'm On Fire" }, { id: 21, title: "Because The Night" }, { id: 22, title: "Rosalita (Come Out Tonight)" }, { id: 23, title: "Can't Help Falling In Love" }, { id: 24, title: "Born To Run" }, { id: 25, title: "Bobby Jean" }, { id: 26, title: "Two Hearts" }, { id: 27, title: "Ramrod" }, { id: 28, title: "Twist And Shout" }], expanded: false },
      { date: new Date(1988, 5, 25), location: "Wembley Stadium, London", tour: "Tunnel of Love Express", expanded: false },
      { date: new Date(1992, 6, 13), location: "Wembley Arena, London", tour: "World Tour", expanded: false },
      { date: new Date(1993, 4, 22), location: "National Bowl, Milton Keynes", tour: "World Tour", expanded: false },
      { date: new Date(1996, 3, 16), location: "Royal Albert Hall, London", tour: "Ghost of Tom Joad Tour", expanded: false },
      { date: new Date(1999, 4, 18), location: "Earl's Court, London", tour: "Reunion Tour", expanded: false },
      { date: new Date(1999, 4, 19), location: "Earl's Court, London", tour: "Reunion Tour", expanded: false },
      { date: new Date(2002, 9, 27), location: "Wembley Arena, London", tour: "Rising Tour", expanded: false },
      { date: new Date(2003, 4, 26), location: "Crystal Palace Sports Ground, London", tour: "Rising Tour", expanded: false },
      { date: new Date(2003, 4, 27), location: "Crystal Palace Sports Ground, London", tour: "Rising Tour", expanded: false },
      { date: new Date(2003, 4, 29), location: "Old Trafford Cricket Ground, Manchester", tour: "Rising Tour", expanded: false },
      { date: new Date(2005, 4, 28), location: "Royal Albert Hall, London", tour: "Devils & Dust Tour", expanded: false },
      { date: new Date(2006, 4, 7), location: "MEN Arena, Manchester", tour: "Seeger Sessions Tour", expanded: false },
      { date: new Date(2006, 4, 8), location: "Hammersmith Apollo, London", tour: "Seeger Sessions Tour", expanded: false },
      { date: new Date(2006, 10, 9), location: "NEC Arena, Birmingham", tour: "Seeger Sessions Tour", expanded: false },
      { date: new Date(2006, 10, 11), location: "Wembley Arena, London", tour: "Seeger Sessions Tour", expanded: false },
      { date: new Date(2006, 10, 14), location: "Hallam FM Arena, Sheffield", tour: "Seeger Sessions Tour", expanded: false },
      { date: new Date(2007, 11, 19), location: "O2 Arena, London", tour: "Magic Tour", expanded: false },
      { date: new Date(2008, 4, 30), location: "Emirates Stadium, London", tour: "Magic Tour", expanded: false },
      { date: new Date(2008, 4, 31), location: "Emirates Stadium, London", tour: "Magic Tour", expanded: false },
      { date: new Date(2008, 5, 14), location: "Millenium Stadium, Cardiff", tour: "Magic Tour", expanded: false },
      { date: new Date(2009, 5, 28), location: "Hyde Park, London", tour: "Working on a Dream Tour", expanded: false },
      { date: new Date(2012, 6, 14), location: "Hyde Park, London", tour: "Wrecking Ball Tour", expanded: false },
      { date: new Date(2013, 6, 15), location: "Wembley Stadium, London", tour: "Wrecking Ball Tour", expanded: false },
      { date: new Date(2013, 5, 30), location: "Olympic Park, London", tour: "Wrecking Ball Tour", expanded: false },
      { date: new Date(2016, 5, 3), location: "Ricoh Arena, Coventry", tour: "The River Tour", expanded: false },
      { date: new Date(2016, 5, 5), location: "Wembley Stadium, London", tour: "The River Tour", expanded: false },
    ];

  isBirthday(value: Date) {
    var today = new Date();
    return (value.getMonth() == today.getMonth() && value.getDate() == today.getDate());
  }

  toggleTracklist(row: number) {
    if (this.concerts[row - 1].tracks?.length == 0 || typeof (this.concerts[row - 1].tracks) == "undefined")
      return;
    this.concerts[row - 1].expanded = !this.concerts[row - 1].expanded;
  }
}
