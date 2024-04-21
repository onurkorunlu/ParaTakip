import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class HttpService {

  private baseAdress: string = "";

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseAdress = baseUrl;
  }

  getExternal<T>(path: string) {
    return this.httpClient.get<T>(path);
  }

  get<T>(path: string) {
    return this.httpClient.get<T>(this.baseAdress + path);
  }

  getVoid(path: string) {
    return this.httpClient.get(this.baseAdress + path);
  }

  post<T>(path: string, model: any) {
    return this.httpClient.post<T>(this.baseAdress + path, JSON.stringify(model));
  }

  delete<T>(path: string) {
    return this.httpClient.delete<T>(this.baseAdress + path);
  }
}
