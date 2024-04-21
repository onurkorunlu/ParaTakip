import { Injectable } from '@angular/core';
import { AppUser } from '../models/entities/appUser';
import { AppUserRole } from '../models/entities/appUserRole';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';
const ROLE_KEY = 'auth-user-role';

@Injectable({
    providedIn: 'root'
})
export class TokenStorageService {

    constructor() { }

    signOut() {
        window.sessionStorage.clear();
    }

    public saveToken(token: string) {
        window.sessionStorage.removeItem(TOKEN_KEY);
        window.sessionStorage.setItem(TOKEN_KEY, token);
    }

    public getToken(): string | null {
        return sessionStorage.getItem(TOKEN_KEY);
    }

    public saveUser(user:AppUser) {
        window.sessionStorage.removeItem(USER_KEY);
        window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
    }

    public getUser() : AppUser {
        const userJson = sessionStorage.getItem(USER_KEY);
        return userJson !== null ? JSON.parse(userJson) : null;
    }

    public saveRole(userRole:AppUserRole) {
        window.sessionStorage.removeItem(ROLE_KEY);
        window.sessionStorage.setItem(ROLE_KEY, JSON.stringify(userRole));
    }

    public getRole() : AppUserRole {
        const userJson = sessionStorage.getItem(ROLE_KEY);
        return userJson !== null ? JSON.parse(userJson) : null;
    }
}