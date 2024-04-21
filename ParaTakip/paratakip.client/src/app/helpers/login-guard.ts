import {  CanActivate,  ActivatedRouteSnapshot,  RouterStateSnapshot,  Router} from "@angular/router";
import { Injectable } from "@angular/core";
import { TokenStorageService } from "../services/token-storage.service";

@Injectable()
export class LoginGuard implements CanActivate {
  constructor(private router: Router, private tokenStorageService:TokenStorageService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    
    let logged = !!this.tokenStorageService.getToken();

    if (logged) {
      return true;
    }
    this.router.navigate(["login"]);
    // this.alertify.error("Sayfaya erişim için sisteme giriş yapmalısınız!");
    return false;
  }
}