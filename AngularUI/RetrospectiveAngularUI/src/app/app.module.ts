import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { RetrospectivesComponent } from './retrospectives/retrospectives.component';
import { RetrospectiveComponent } from './retrospectives/retrospective/retrospective.component';
import { RetrospectiveListComponent } from './retrospectives/retrospective-list/retrospective-list.component';
import { RetrospectiveService } from './shared/retrospective.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    RetrospectivesComponent,
    RetrospectiveComponent,
    RetrospectiveListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [RetrospectiveService],
  bootstrap: [AppComponent]
})
export class AppModule { }
