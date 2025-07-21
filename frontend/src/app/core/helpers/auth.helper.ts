import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { LoginResponse } from "../models/login-response.model";

@Injectable({
    providedIn: 'root'
})
export class AuthHelper {
    localStorageUserAuthenticated = 'authenticated';


    constructor(
        private router: Router
    ) { }

    setUserAuthenticated(data: LoginResponse) {
        localStorage.setItem(this.localStorageUserAuthenticated, JSON.stringify(data));
    }

    getUserAuthenticated(): LoginResponse | null {
        const authenticated = localStorage.getItem(this.localStorageUserAuthenticated);

        if (authenticated) {
            return JSON.parse(authenticated);
        }

        return null;
    }

    logout() {
        localStorage.clear();
        this.router.navigate(['/login']);
    }

    isAuthenticated(): boolean {
        return !!this.getUserAuthenticated();
    }

    getToken(): string | null {
        var authenticated = this.getUserAuthenticated();
        return authenticated ? authenticated.accessToken : null;
    }

    getName(): string | null {
        var authenticated = this.getUserAuthenticated();
        return authenticated ? authenticated.user.name : null;
    }
}