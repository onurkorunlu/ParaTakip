
import { Injectable } from '@angular/core';
import { Observable, of, scheduled } from 'rxjs';
import { HttpService } from './http.service';
import { LoginModel } from '../models/requestModel/loginModel';
import { LoginResultModel } from '../models/responseModel/loginResultModel';
import { RegisterModel } from '../models/requestModel/registerModel';
import { RegisterResultModel } from '../models/responseModel/registerResultModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(public httpService: HttpService) { }

  login(model:LoginModel): Observable<LoginResultModel> {
    return this.httpService.post<LoginResultModel>("appuser/login", model);
  }

  register(model:RegisterModel): Observable<RegisterResultModel> {
    return this.httpService.post<RegisterResultModel>("appuser/register", model);
  }
}
