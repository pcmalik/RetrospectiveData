import { Injectable } from '@angular/core';
import { Retrospective } from './retrospective.model';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Feedback } from './feedback.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
}

@Injectable({
  providedIn: 'root'
})
export class RetrospectiveService {

  retrospectiveFormData: Retrospective;
  retrospectiveList: Retrospective[];
  feedbackFormData: Feedback;

  baseUrl:string = "https://localhost:44387/api";
  
  constructor(private http: HttpClient) { }

  postRetrospective(retrospectiveFormData: Retrospective){
    return this.http.post(this.baseUrl+"/Retrospectives", retrospectiveFormData);
  }

  postFeedback(retrospectiveName: string, feedbackFormData: Feedback){
    return this.http.post(this.baseUrl+"/Retrospectives/"+retrospectiveName+"/Feedbacks", feedbackFormData, httpOptions);
  }

  refreshList(){   
    this.http.get(this.baseUrl+"/Retrospectives")
      .toPromise().then(res => this.retrospectiveList = res as Retrospective[]);
  }

  filterRetrospectivesList(date: string){   
    this.http.get(this.baseUrl+"/Retrospectives/Filter?date="+date)
      .toPromise().then(res => this.retrospectiveList = res as Retrospective[]);
  }

}
