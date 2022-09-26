import type { IGameRound } from "@/domain/IGameRound";
import httpCLient from "@/http-client";
import { BaseService } from "./BaseService";

export class GameRoundService extends BaseService<IGameRound> {
    constructor() {
        super("gameround");
    }
}