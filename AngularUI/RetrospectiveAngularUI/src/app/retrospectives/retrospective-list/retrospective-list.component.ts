import { Component, OnInit } from '@angular/core';
import { RetrospectiveService } from 'src/app/shared/retrospective.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-retrospective-list',
  templateUrl: './retrospective-list.component.html',
  styleUrls: ['./retrospective-list.component.css']
})
export class RetrospectiveListComponent implements OnInit {
  retroDateFilter: Date;
  constructor(private service : RetrospectiveService) { }

  ngOnInit() {
    this.service.refreshList();
  }

  retroDateFilterChange() {  
    const format = 'dd/MM/yyyy';
    const locale = 'en-GB';
    const formattedRetroDateFilter = formatDate(this.retroDateFilter, format, locale);

    this.service.filterRetrospectivesList(formattedRetroDateFilter);
  }

}
