import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpResponse,
    HttpErrorResponse
} from '@angular/common/http';
import { from, Observable, throwError } from 'rxjs';
import { map, catchError, switchMap } from 'rxjs/operators';
import { DebugElement, Inject, Injectable } from '@angular/core';
import { TokenStorageService } from '../services/token-storage.service';
import { Utils } from '../helpers/utils';
import HttpStatusCode from '../models/enums/HttpStatusCode';

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {
    private baseAdress: string = "";
    constructor(private tokenStorage: TokenStorageService,@Inject('BASE_URL') baseUrl: string) {
        this.baseAdress = baseUrl;
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.tokenStorage.getToken();
        let newRequest: HttpRequest<any>;

        if (request.url != this.baseAdress + "user/login") {
            newRequest = request.clone({
                headers: request.headers.set("Authorization", `Bearer ${token}`)
            });
        } else
            newRequest = request.clone();

        if(request.method == 'POST')
        {
            newRequest = newRequest.clone({
                headers: newRequest.headers.set('Content-Type', 'application/json')
            });
        }

        return next.handle(newRequest).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    console.log('event--->>>', event);
                }
                return event;
            }),
            catchError((httpErrorResponse) => {
                console.log(httpErrorResponse);

                let errorMessage: string;

                if (httpErrorResponse.status == HttpStatusCode.UNAUTHORIZED) {
                    errorMessage = 'İşlemi gerçekleştirmek için lütfen giriş yapınız.';
                }
                else if (httpErrorResponse.status == HttpStatusCode.BAD_REQUEST && !Utils.stringIsEmpty(httpErrorResponse.error)) {
                    errorMessage = httpErrorResponse.error;
                }
                else {
                    errorMessage = 'Bir hata oluştu lütfen daha sonra tekrar deneyiniz.';
                }

                return throwError(errorMessage);
            }))
    };
}