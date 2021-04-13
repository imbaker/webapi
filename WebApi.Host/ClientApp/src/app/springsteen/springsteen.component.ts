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
    tour: string
  }[] = [
      { date: new Date(1985, 6, 3), location: "Wembley Stadium, London", tour: "Born in the USA" },
      { date: new Date(1988, 5, 25), location: "Wembley Stadium, London", tour: "Tunnel of Love Express" },
      { date: new Date(1992, 6, 13), location: "Wembley Arena, London", tour: "World Tour" },
      { date: new Date(1993, 4, 22), location: "National Bowl, Milton Keynes", tour: "World Tour" },
      { date: new Date(1996, 3, 16), location: "Royal Albert Hall, London", tour: "Ghost of Tom Joad Tour" },
      { date: new Date(1999, 4, 18), location: "Earl's Court, London", tour: "Reunion Tour" },
      { date: new Date(1999, 4, 19), location: "Earl's Court, London", tour: "Reunion Tour" },
      { date: new Date(2002, 9, 27), location: "Wembley Arena, London", tour: "Rising Tour" },
      { date: new Date(2003, 4, 26), location: "Crystal Palace Sports Ground, London", tour: "Rising Tour" },
      { date: new Date(2003, 4, 27), location: "Crystal Palace Sports Ground, London", tour: "Rising Tour" },
      { date: new Date(2003, 4, 29), location: "Old Trafford Cricket Ground, Manchester", tour: "Rising Tour" },
      { date: new Date(2005, 4, 28), location: "Royal Albert Hall, London", tour: "Devils & Dust Tour" },
      { date: new Date(2006, 4, 7), location: "MEN Arena, Manchester", tour: "Seeger Sessions Tour" },
      { date: new Date(2006, 4, 8), location: "Hammersmith Apollo, London", tour: "Seeger Sessions Tour" },
      { date: new Date(2006, 10, 9), location: "NEC Arena, Birmingham", tour: "Seeger Sessions Tour" },
      { date: new Date(2006, 10, 11), location: "Wembley Arena, London", tour: "Seeger Sessions Tour" },
      { date: new Date(2006, 10, 14), location: "Hallam FM Arena, Sheffield", tour: "Seeger Sessions Tour" },
      { date: new Date(2007, 11, 19), location: "O2 Arena, London", tour: "Magic Tour" },
      { date: new Date(2008, 4, 30), location: "Emirates Stadium, London", tour: "Magic Tour" },
      { date: new Date(2008, 4, 31), location: "Emirates Stadium, London", tour: "Magic Tour" },
      { date: new Date(2008, 5, 14), location: "Millenium Stadium, Cardiff", tour: "Magic Tour" },
      { date: new Date(2009, 5, 28), location: "Hyde Park, London", tour: "Working on a Dream Tour" },
      { date: new Date(2012, 6, 14), location: "Hyde Park, London", tour: "Wrecking Ball Tour" },
      { date: new Date(2013, 6, 15), location: "Wembley Stadium, London", tour: "Wrecking Ball Tour" },
      { date: new Date(2013, 5, 30), location: "Olympic Park, London", tour: "Wrecking Ball Tour" },
      { date: new Date(2016, 5, 3), location: "Ricoh Arena, Coventry", tour: "The River Tour" },
      { date: new Date(2016, 5, 5), location: "Wembley Stadium, London", tour: "The River Tour" },
    ];

  isBirthday(value: Date) {
    var today = new Date();
    return (value.getMonth() == today.getMonth() && value.getDate() == today.getDate());
  }
}
