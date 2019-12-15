import { Component, OnInit } from '@angular/core';
import { RetrospectiveService } from 'src/app/shared/retrospective.service';
import { Retrospective } from 'src/app/shared/retrospective.model';

@Component({
  selector: 'app-retrospective-list',
  templateUrl: './retrospective-list.component.html',
  styleUrls: ['./retrospective-list.component.css']
})
export class RetrospectiveListComponent implements OnInit {

  constructor(private service : RetrospectiveService) { }

  ngOnInit() {
    this.service.refreshList();
  }

}
