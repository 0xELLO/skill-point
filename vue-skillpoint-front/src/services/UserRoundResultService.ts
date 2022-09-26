import type { IUserRoundResult } from "@/domain/IUserRoundResult";
import { BaseService } from "./BaseService";

export class UserRoundResultService extends BaseService<IUserRoundResult> {
    constructor() {
        super("userroundresult");
    }
}