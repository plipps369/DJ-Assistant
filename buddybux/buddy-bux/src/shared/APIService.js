import axios from 'axios';
import auth from "@/shared/auth";

export class APIService {
    constructor() {
    }

    async login(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/login`;
        let res = await axios.post(url, data);
        if (res.status === 401) {
            throw "Your username and/or password is invalid";
        }
        return res.data;
    }

    async register(data) {
        const url = `${process.env.VUE_APP_REMOTE_API}/account/register`;
        let res = await axios.post(url, data);
        if (res.status === 400) {
            throw res.data.message;
        }
        return res.data;
    }

    getStore(username) {
        const url = `${process.env.VUE_APP_REMOTE_API}/storefront/${username}`;
        return axios.get(url, {
            headers: {
                Authorization: "Bearer " + auth.getToken()
            }
        }).then(response => response.data);
    }

    // getPlayers() {
    //     const url = `${API_URL}/api/players`;
    //     return axios.get(url).then(response => response.data);
    // }

    // addPlayer(playerName) {
    //     const url = `${API_URL}/api/player`;
    //     const player = {};
    //     player.name = playerName;
    //     return axios.post(url, player).then(response => response.data);        
    // }
}