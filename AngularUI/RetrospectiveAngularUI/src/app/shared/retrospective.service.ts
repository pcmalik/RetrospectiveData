import { Injectable } from '@angular/core';
import { Retrospective } from './retrospective.model';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class RetrospectiveService {

  formData: Retrospective;
  retrospectiveList: Retrospective[];

  baseUrl:string = "https://localhost:44387/api";
  
  constructor(private http: HttpClient) { }

  postRetrospective(formData: Retrospective){
    return this.http.post(this.baseUrl+"/Retrospectives", formData);
  }

  refreshList(){
    this.http.get(this.baseUrl+"/Retrospectives")
      .toPromise().then(res => this.retrospectiveList = res as Retrospective[]);
  }
}
