import type { IGame } from "@/domain/IGame";
import type { IGameContent } from "@/domain/IGameContent";
import httpCLient from "@/http-client";
import { BaseService } from "./BaseService";
import type { AxiosError } from "axios";
import { IdentityService } from "./IdentityService";


export class GameContentService extends BaseService<IGameContent> {
    constructor() {
        super("gamecontent");
    }

    async getRandomTypingGame() : Promise<IGameContent>{
        console.log("getting random game");
        try {
            let response = await httpCLient.get(`/${this.path}/typing/random`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            console.log(response);

            let res = response.data as IGameContent;
            return res;
        } catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if ( !this.identityStore.$state.jwt) return {
                    id: "adf",
                    content: "fasdf",
                    gameId: "fasdf"
                };;
                

                let response = await httpCLient.get(`/${this.path}/typing/random`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });
                console.log(response);
    
                let res = response.data as IGameContent;
                return res;

            }

            return {
                id: "adf",
                content: "fasdf",
                gameId: "fasdf"
            };;

        }
    }
}