import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class ToastService {

    constructor(private toastr: ToastrService) { }

    showSuccess(message?:string, title?:string) {
        
        if(message==null || message=='')
        message='İşlem başarı ile gerçekleştirilmişir.'

        if(title==null || title=='')
        title='İşlem Başarılı'

        this.toastr.success(message, title,{ closeButton: false, timeOut: 4000, progressBar: true, enableHtml:true})
    }

    showError(message?:string, title?:string) {
                
        if(message==null || message=='')
        message='İşlem gerçekleştirilirken hata oluştu.'

        if(title==null || title=='')
        title='Hata'

        this.toastr.error(message, title,{ closeButton: false, timeOut: 4000, progressBar: true, enableHtml:true})
    }

    showInfo(message:string, title?:string) {
                
        if(title==null || title=='')
        title='Bilgilendirme'

        this.toastr.info(message, title,{ closeButton: false, timeOut: 4000, progressBar: true, enableHtml:true})
    }

    showWarning(message:string, title?:string) {

        if(title==null || title=='')
        title='Bilgilendirme'

        this.toastr.warning(message, title,{ closeButton: false, timeOut: 4000, progressBar: true, enableHtml:true})
    }
}