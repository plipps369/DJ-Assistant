import axios from 'axios';
const API_URL = 'http://192.168.51.43:50260';

export class APIService{
    constructor(){
    }
    
    getParties(DJid) {
        const url = `${API_URL}/api/parties/${DJid}`;
        return axios.get(url).then(response => response.data);
    }
    getParty(id) {
        const url = `${API_URL}/api/party/${id}`;
        return axios.get(url).then(response => response.data);
    }
}