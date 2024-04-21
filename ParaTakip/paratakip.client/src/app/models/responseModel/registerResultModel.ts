import {AppUser} from '../entities/appUser'
import {AppUserRole} from '../entities/appUserRole'

export interface RegisterResultModel {
    token: string;
    returnUrl: string;
    expireDate: string;
    appUser: AppUser;
    appUserRole: AppUserRole;
    isSuccess:boolean;
    message:string;
}