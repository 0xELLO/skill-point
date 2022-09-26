import { MatchService } from "./MatchService";
import { GameRoundService } from "./GameRoundService";
import { useMatchStore } from "@/stores/match";
import { useGameRoundStore } from "@/stores/gameRound";
import type { IMatch } from "@/domain/IMatch";
import { GameInMatchService } from "./GameInMatchService";
import { UserInMatchService } from "./UserInMatchService";
import type { IGame } from "@/domain/IGame";
import { GameContentService } from "./GameContentService";
import { useGameContentStore } from "@/stores/gameContent";
import { UserRoundResultService } from "./UserRoundResultService";
import type { IUserRoundResult } from "@/domain/IUserRoundResult";
import type { IGameRound } from "@/domain/IGameRound";
import { IdentityService } from "./IdentityService";
import { HelpersService } from "./HelpersService";
import { useGameStore } from "@/stores/games";

export class GameInitiolizerService{
    matchService = new MatchService();
    useMatchStore = useMatchStore();
    gameInMatchService = new GameInMatchService();
    gameRoundService = new GameRoundService();
    useGameRoundStore = useGameRoundStore();
    userInMatchService = new UserInMatchService();
    gameContentService = new GameContentService();
    useGameContentStore = useGameContentStore();
    userRoundResultService = new UserRoundResultService();
    identityService = new IdentityService();
    helpersService = new HelpersService();
    useGameStore = useGameStore();

    async initiolizeSinglePlayerGame(game: IGame){
        let match = await this.matchService.add({
            maxPlayers: 1,
            openedToJoin: false,
            matchType: "singleplayer",
        })
        this.useMatchStore.$state.match = match as IMatch;
        await this.InitGame(game, match as IMatch)
    }

    async initiolizeMultyPlayerGame(game: IGame) : Promise<string>{
        let match = await this.matchService.add({
            maxPlayers: 5,
            openedToJoin: true,
            matchType: "multiplayer",
        })
        this.useMatchStore.$state.match = match as IMatch;
        this.useGameStore.currentGame = game as IGame;
        await this.InitGame(game, match as IMatch)
        return match?.matchToken!;
    }

    async getMatchPlayers(){
        return await this.helpersService.GetUsersByUserInMatch(this.useMatchStore.match?.id!); 
    }

    async joinInMatch(token: string){
        let match = await this.helpersService.joinMatch(token);
        this.useMatchStore.$state.match = match as IMatch;
        let gameRound = await this.helpersService.GetOpenedRound(match?.id!);
        this.useGameRoundStore.$state.gameRound = gameRound as IGameRound;
        let game = this.useGameStore.findById(gameRound?.gameId!);
        console.log("game in join: " + game)
        console.log("round: " + gameRound?.id)
        this.useGameStore.currentGame = game as IGame;

        if (game!.logoUrl == "keyboard"){
            console.log("getting game content");
            let gameContent = await this.gameContentService.get(gameRound!.gameContentId!);
            this.useGameContentStore.$state.gameContent = gameContent;
        }
    }

    async createNewRound(){
        
    }

    private async InitGame(game: IGame, match: IMatch){

        await this.userInMatchService.add({
            matchId: match?.id as string,
        })

        await this.gameInMatchService.add({
            matchId: match!.id as string,
            gameId: game!.id as string,
            roundAmount: 1
        })

        let gameRound = await this.gameRoundService.add({
            gameId: game!.id as string,
            matchId: match!.id as string,
        });

        this.useGameRoundStore.$state.gameRound = gameRound as IGameRound;
        console.log(this.useGameRoundStore.$state.gameRound!.id as string)

        if (game.logoUrl == "keyboard"){
            console.log("getting game content");
            let gameContent = await this.gameContentService.get(gameRound!.gameContentId!);
            this.useGameContentStore.$state.gameContent = gameContent;
        }
    }

    async finishGame(result: string){
        console.log(this.useGameRoundStore.$state.gameRound!.id as string)
        await this.userRoundResultService.add({
            gameRoundId: this.useGameRoundStore.$state.gameRound!.id as string,
            result: result
        })
    }

    async getAllUserResultForGame(game: IGame) : Promise<IUserRoundResult[]>{
        console.log("getting user res")
        let gameRounds = await this.gameRoundService.getAll();
        let result : IUserRoundResult[] = [];
        if (gameRounds == null) return [{gameRoundId: "noId", result: "No results"}];

        gameRounds = gameRounds?.filter(gr => gr.gameId == game.id)
        let userRoundResults = await this.userRoundResultService.getAll();

        if (userRoundResults == null) return result;
        if (userRoundResults.length == 0) return [{gameRoundId: "noId", result: "No results"}];

        gameRounds.forEach(gameRound => {
            userRoundResults!.forEach(userRoundResult => {
                if (gameRound.id == userRoundResult.gameRoundId) {
                    result.push(userRoundResult);
                }
            });

        });
        return result;
    };

    async GetAllUsersRoundResults(game: IGame) : Promise<IUserRoundResult[]>{
        console.log("getting user res")
        let gameRounds = await this.gameRoundService.getAll();
        let result : IUserRoundResult[] = [];
        if (gameRounds == null) return result;

        gameRounds = gameRounds?.filter(gr => gr.gameId == game.id)
        let userRoundResults = await this.userRoundResultService.getAll();

        if (userRoundResults == null) return result;

        gameRounds.forEach(gameRound => {
            userRoundResults!.forEach(userRoundResult => {
                if (gameRound.id == userRoundResult.gameRoundId) {
                    result.push(userRoundResult);
                }
            });

        });
        return result;
    };
}