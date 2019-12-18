import { Component, OnInit, Input } from '@angular/core';
import { Retrospective } from '../../../shared/retrospective.model';
import { RetrospectiveService } from '../../../shared/retrospective.service';
import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Feedback } from '../../../shared/feedback.model';

enum FeedbackType {
  Positive,
  Negative,
  Idea
}

@Component({
  selector: 'app-feedback-item',
  templateUrl: './feedback-item.component.html',
  styleUrls: ['./feedback-item.component.css']
})
export class FeedbackItemComponent implements OnInit {
  @Input() retroItem: Retrospective;
  feedback: Feedback;
  
  feedbacks : string[];
  FeedbackType : typeof FeedbackType = FeedbackType;

  constructor(private service: RetrospectiveService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm){
    if (form != null)
      form.resetForm();

    this.feedback = {
        name: '',
        body: '',
        feedbackType: ''
      }
  
    var feedbacks = Object.keys(FeedbackType);
    this.feedbacks = feedbacks.slice(feedbacks.length / 2); //remove the enum id's
  }

  onSubmit(form: NgForm){
      this.insertRecord(form);
  }

  insertRecord(form: NgForm){
    console.log(form.value);
      this.service.postFeedback(this.retroItem.name, this.feedback).subscribe(
        res =>{
                alert("Inserted feedback data successfully...");
                this.resetForm(form);
              },
        err =>{
                alert((err as HttpErrorResponse).error);
              } );
  }

}
