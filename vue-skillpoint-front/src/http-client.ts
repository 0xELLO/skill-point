import axios from "axios";

export const httpCLient = axios.create({
    baseURL: "https://localhost:7028/api/v1",
    headers: {
        "Content-type": "application/json"
    }
});

export default httpCLient;
