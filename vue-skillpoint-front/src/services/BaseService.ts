import httpCLient from "@/http-client";
import { useIdentityStore } from "@/stores/identity";
import type { AxiosError } from "axios";
import { CookieService } from "./CookieService";
import { IdentityService } from "./IdentityService";
import type { IServiceResult } from "./IServiceResult";

export class BaseService<TEntity> {
    identityStore = useIdentityStore();

    constructor(protected path: string) {
    }

    async getAll(): Promise<TEntity[] | null> {
        console.log("getAll");

        try {
            //anime/kaguya
            let response = await httpCLient.get(`/${this.path}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            let res = response.data as TEntity[];
            return res;

        } catch (e) {

            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;
                
                if (!this.identityStore.$state.jwt) {
                    return null;
                };
                
                let response = await httpCLient.get(`/${this.path}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as TEntity[];
            }
        }
        return null;
    }

    async get(id: string): Promise<TEntity | null> {
        try {
            let response = await httpCLient.get(`/${this.path}/${id}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            let res = response.data as TEntity;
            return res;

        } catch (e) {

            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;
                
                if (!this.identityStore.$state.jwt) {
                    return null;
                };
                
                let response = await httpCLient.get(`/${this.path}/${id}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as TEntity;
            }
        }
        return null;
    }

    async add(entity: TEntity): Promise<TEntity | null> {
        console.log("add");

        try {
            let response = await httpCLient.post(`/${this.path}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

            let res = response.data as TEntity;
            return res;

        } catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;
                
                if (!this.identityStore.$state.jwt) {
                    return null;
                };
                
                let response = await httpCLient.post(`/${this.path}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as TEntity;
            }

        return null;
        }
    }
}