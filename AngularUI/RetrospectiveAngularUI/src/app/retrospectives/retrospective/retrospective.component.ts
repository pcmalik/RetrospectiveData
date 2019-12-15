import { Component, OnInit } from '@angular/core';
import { RetrospectiveService } from 'src/app/shared/retrospective.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-retrospective',
  templateUrl: './retrospective.component.html',
  styleUrls: ['./retrospective.component.css']
})
export class RetrospectiveComponent implements OnInit {

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
      this.service.postRetrospective(form.value).subscribe(res =>{
        alert("Inserted retrospective data successfully...");
        this.resetForm(form);
      });
  }

}
