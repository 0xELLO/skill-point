import type { IJWTResponse } from "@/domain/IJWTResponse";
import { IdentityService } from "./IdentityService";


export class CookieService {  
    getCookie(cname: string){
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for(var i = 0; i <ca.length; i++) {
          var c = ca[i];
          while (c.charAt(0) == ' ') {
            c = c.substring(1);
          }
          if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
          }
        }
        return "";
    }

    checkCookie(cname: string) : boolean {
        if (this.getCookie(cname) == "") return false;
        return true;
      }

    deleteTokenCookies(){
        document.cookie = "token" +'=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        document.cookie = "refreshToken" +'=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }

    setTokenCookies(jwt: IJWTResponse){
        document.cookie =`token=${jwt.token}`;
        document.cookie =`refreshToken=${jwt.refreshToken}`;
    }
}