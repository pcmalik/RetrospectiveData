import { Injectable } from '@angular/core';
import { Retrospective } from './retrospective.model';
import { HttpClient, HttpHeaders } from "@angular/common/http";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
}
@Injectable({
  providedIn: 'root'
})
export class RetrospectiveService {

  formData: Retrospective;
  baseUrl:string = "https://localhost:44387/api";
  
  constructor(private http: HttpClient) { }

  postRetrospective(formData: Retrospective){
    return this.http.post(this.baseUrl+"/Retrospectives",formData, httpOptions);
  }
}
