import type { IUserGameStatistics } from "@/domain/IUserGameStatistics";
import { BaseService } from "./BaseService";

export class UserGameStatisticsSerivce extends BaseService<IUserGameStatistics> {
    constructor() {
        super("usergamestatistics");
    }

    
}
