import axios from 'axios';
const API_URL = 'https://localhost:44313';

export class APIService{
    constructor(){
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