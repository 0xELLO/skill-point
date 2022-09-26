import type { IGame } from "@/domain/IGame";
import httpCLient from "@/http-client";
import { BaseService } from "./BaseService";

export class GameService extends BaseService<IGame> {
    constructor() {
        super("game");
    }
}