import { Component, OnInit } from '@angular/core';
import { RetrospectiveService } from 'src/app/shared/retrospective.service';
import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-retrospective',
  templateUrl: './retrospective.component.html',
  styleUrls: ['./retrospective.component.css']
})
export class RetrospectiveComponent implements OnInit {
  participants: string;

  constructor(private service: RetrospectiveService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm){
    if (form != null)
      form.resetForm();

    this.service.retrospectiveFormData = {
      name: '',
      summary: '',
      date: '',
      participants: null,
      feedback: null
    }
  }

  onSubmit(form: NgForm){
      this.insertRecord(form);
  }

  insertRecord(form: NgForm){
    
      this.service.retrospectiveFormData.participants = this.participants.split(",");

      this.service.postRetrospective(this.service.retrospectiveFormData).subscribe(
      res =>{
              alert("Inserted retrospective data successfully...");
              this.resetForm(form);
              this.service.refreshList();
            },
      err =>{
              alert((err as HttpErrorResponse).error);
            });

  }

}
