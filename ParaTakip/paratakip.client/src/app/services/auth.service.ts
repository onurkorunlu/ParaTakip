import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';

const AUTH_API = 'http://localhost:8080/api/auth/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpService: HttpService) { }

  login(loginModel:any): Observable<any> {
    return this.httpService.post("appuser/login", {
      username: loginModel.username,
      password: loginModel.password
    });
  }
}