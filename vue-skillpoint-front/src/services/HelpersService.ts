import type { IGameRound } from "@/domain/IGameRound";
import type { IMatch } from "@/domain/IMatch";
import type { IUserRoundResult } from "@/domain/IUserRoundResult";
import type { IUsers } from "@/domain/IUsers";
import httpCLient from "@/http-client";
import { useIdentityStore } from "@/stores/identity";
import type { AxiosError } from "axios";
import { CookieService } from "./CookieService";
import { IdentityService } from "./IdentityService";
import type { IServiceResult } from "./IServiceResult";

export class HelpersService {
    identityStore = useIdentityStore();
    path = "helpers";

    async joinMatch(token: string): Promise<IMatch | null> {
        try {
            let response = await httpCLient.get(`/${this.path}/joinmatch/${token}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            let res = response.data as IMatch;
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
                
                let response = await httpCLient.get(`/${this.path}/joinmatch/${token}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as IMatch;
            }
        }
        return null;
    }

    async GetUsersByUserInMatch(id: string): Promise<IUsers | null> {
        try {
            let response = await httpCLient.get(`/${this.path}/GetUsersByUserInMatch/${id}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            let res = response.data as IUsers;
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
                
                let response = await httpCLient.get(`/${this.path}/GetUsersByUserInMatch/${id}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as IUsers;
            }
        }
        return null;
    }

    async GetOpenedRound(matchId: string): Promise<IGameRound | null> {
        try {
            let response = await httpCLient.get(`/${this.path}/GetOpenedRound/${matchId}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            let res = response.data as IGameRound;
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
                
                let response = await httpCLient.get(`/${this.path}/GetOpenedRound/${matchId}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as IGameRound;
            }
        }
        return null;
    }

    async GetUserEmailByRoundId(roundId: string): Promise<IUsers | null> {
        try {
            let response = await httpCLient.get(`/${this.path}/GetUserEmailByRoundResultId/${roundId}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            let res = response.data as IUsers;
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
                
                let response = await httpCLient.get(`/${this.path}/GetUserEmailByRoundResultId/${roundId}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as IUsers;
            }
        }
        return null;
    }

    async GetUserRoundResultByRound(roundId: string): Promise<IUserRoundResult[] | null> {
        try {
            let response = await httpCLient.get(`/${this.path}/GetUserRoundResultByRound/${roundId}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            let res = response.data as IUserRoundResult[];
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
                
                let response = await httpCLient.get(`/${this.path}/GetUserRoundResultByRound/${roundId}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });

                console.log(response);
                return response.data as IUserRoundResult[];
            }
        }
        return null;
    }
}