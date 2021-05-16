import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { DaysComponent } from './days/days.component';
import { AbsPipe } from './days/abs.pipe';
import { SpringsteenComponent } from './springsteen/springsteen.component';
import { HomeComponent } from './home/home.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DateComponent } from './components/date.component';
import { NgbdToastGlobal } from './services/toast-global.component';
import { ToastsContainer } from './services/toast-container.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SpringsteenComponent,
    DaysComponent,
    DateComponent,
    AbsPipe,
    NgbdToastGlobal,
    ToastsContainer
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'springsteen', component: SpringsteenComponent },
      { path: 'days', component: DaysComponent },
    ]),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
