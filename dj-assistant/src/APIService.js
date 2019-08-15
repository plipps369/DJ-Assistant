import axios from 'axios';
const API_URL = 'http://localhost:50992';

export class APIService {
    constructor() {
    }

    async login(data) {
        const url = `${API_URL}/api/DJ/login`;
        let res = await axios.post(url, data);
        if (res.status === 401) {
            throw "Your username and/or password is invalid";
        }
        return res.data;
    }




    createParty(party) {
        const url = `${API_URL}/api/party`;
        return axios.post(url, party).then(response => response.data);
    }
    getParties() {
        const url = `${API_URL}/api/party`;
        return axios.get(url).then(response => response.data);
    }
    getParty(id) {
        const url = `${API_URL}/api/party/${id}`;
        return axios.get(url).then(response => response.data);
    }
}