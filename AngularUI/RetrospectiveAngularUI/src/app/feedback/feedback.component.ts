import { Component, OnInit, Input } from '@angular/core';
import { Retrospective } from '../shared/retrospective.model';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {
  @Input() retroItem: Retrospective;

  constructor() { }

  ngOnInit() {
  }

}
