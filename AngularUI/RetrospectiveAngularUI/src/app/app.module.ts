import { BrowserModule } from '@angular/platform-browser';
import { NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { RetrospectivesComponent } from './retrospectives/retrospectives.component';
import { RetrospectiveComponent } from './retrospectives/retrospective/retrospective.component';
import { RetrospectiveListComponent } from './retrospectives/retrospective-list/retrospective-list.component';
import { RetrospectiveService } from './shared/retrospective.service';
import { HttpClientModule } from '@angular/common/http';
import { FeedbackItemComponent } from './retrospectives/retrospective-list/feedback-item/feedback-item.component';
import { MatDatepickerModule, MatNativeDateModule, MatInputModule, MAT_DATE_LOCALE } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    RetrospectivesComponent,
    RetrospectiveComponent,
    RetrospectiveListComponent,
    FeedbackItemComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    BrowserAnimationsModule
  ],
  providers: [RetrospectiveService, { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
