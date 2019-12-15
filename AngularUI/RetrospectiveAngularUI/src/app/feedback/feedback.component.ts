import { Component, OnInit, Input } from '@angular/core';
import { Retrospective } from '../shared/retrospective.model';
import { RetrospectiveService } from '../shared/retrospective.service';
import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

enum FeedbackType {
  Positive,
  Negative,
  Idea
}

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {
  @Input() retroItem: Retrospective;
  
  feedbacks : string[];
  //selectedFeedbackValue: FeedbackType;
  FeedbackType : typeof FeedbackType = FeedbackType;

  constructor(private service: RetrospectiveService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm){
    if (form != null)
      form.resetForm();

    this.service.feedbackFormData = {
      name: '',
      body: '',
      feedbackType: ''
    }

    var feedbacks = Object.keys(FeedbackType);
    this.feedbacks = feedbacks.slice(feedbacks.length / 2); //remove the enum id's
  }

  /* not needing as binding is done within form itself
  parsefeedbackType(value : string) {
    this.selectedFeedbackValue = FeedbackType[value];
  }
  */
  onSubmit(form: NgForm){
      this.insertRecord(form);
  }

  insertRecord(form: NgForm){
    console.log(form.value);
      this.service.postFeedback(this.retroItem.name, form.value).subscribe(
        res =>{
                alert("Inserted feedback data successfully...");
                this.resetForm(form);
              },
        err =>{
                alert("Failed:"+ (err as HttpErrorResponse).error);
              } );
  }

}
