import type { IJWTResponse } from "@/domain/IJWTResponse";
import httpCLient from "@/http-client";
import { useIdentityStore } from "@/stores/identity";
import type { AxiosError } from "axios";
import type { IServiceResult } from "./IServiceResult";
import { CookieService } from "./CookieService";
import type { IUsers } from "@/domain/IUsers";
import router from '@/router';

export class IdentityService {
    identityStore = useIdentityStore();
    cookieService = new CookieService();

    async login(email: string, password: string): Promise<IServiceResult<IJWTResponse>> {
        try {
            let loginInfo = {
                email,
                password
            }
            console.log(loginInfo);
            let response = await httpCLient.post("/identity/Account/LogIn", loginInfo);
            this.cookieService.setTokenCookies(response.data as IJWTResponse);

            return {
                status: response.status,
                data: response.data as IJWTResponse
            };

        } catch (e) {
            let response = {
                status: (e as AxiosError).response!.status,
                errorMsg: (e as AxiosError).response!.data as string ,
            }

            console.log(response);

            console.log((e as AxiosError).response);

            return response;
        }
    }

    async register(email: string, password: string): Promise<IServiceResult<IJWTResponse>> {
        try {
            let loginInfo = {
                email,
                password
            }
            console.log(loginInfo);
            let response = await httpCLient.post("/identity/Account/Register", loginInfo);
            this.cookieService.setTokenCookies(response.data as IJWTResponse);
            return {
                status: response.status,
                data: response.data as IJWTResponse
            };

        } catch (e) {
            let response = {
                status: (e as AxiosError).response!.status,
                errorMsg: (e as AxiosError).response!.data as string ,
            }

            console.log(response);

            console.log((e as AxiosError).response);

            return response;
        }
    }

    async refreshIdentity(): Promise<IServiceResult<IJWTResponse>> {
        console.log("token refreshed")
        try {
            console.log(this.identityStore.$state.jwt);

            let response = await httpCLient.post("/identity/account/refreshtoken",
                {
                    jwt: this.identityStore.$state.jwt?.token,
                    refreshToken: this.identityStore.$state.jwt?.refreshToken
                }
            );
            this.cookieService.setTokenCookies(response.data as IJWTResponse);
            return {
                status: response.status,
                data: response.data as IJWTResponse
            };

        } catch (e) {
            let response = {
                status: (e as AxiosError).response!.status,
                errorMsg: (e as AxiosError).response!.data as string,
            }

            let cookieService = new CookieService();
            cookieService.deleteTokenCookies();
            
            router.push({name: 'login'});
            

            console.log(response);
            console.log((e as AxiosError).response);

            return response;
        }
    }
}