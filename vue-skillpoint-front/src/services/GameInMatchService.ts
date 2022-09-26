import type { IGameInMatch } from "@/domain/IGameInMatch";
import type { IGameRound } from "@/domain/IGameRound";
import httpCLient from "@/http-client";
import { BaseService } from "./BaseService";

export class GameInMatchService extends BaseService<IGameInMatch> {
    constructor() {
        super("gameinmatch");
    }
}