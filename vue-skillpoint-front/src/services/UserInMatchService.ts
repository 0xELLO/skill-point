import type { IUserInMatch } from "@/domain/IUserInMatch";
import { BaseService } from "./BaseService";

export class UserInMatchService extends BaseService<IUserInMatch> {
    constructor() {
        super("userinmatch");
    }

    
}