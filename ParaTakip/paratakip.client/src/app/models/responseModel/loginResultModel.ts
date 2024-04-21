import {AppUser} from '../entities/appUser'
import {AppUserRole} from '../entities/appUserRole'

export interface LoginResultModel {
    token: string;
    returnUrl: string;
    expireDate: string;
    appUser: AppUser;
    appUserRole: AppUserRole;
    isSuccess:boolean;
    message:string;
}