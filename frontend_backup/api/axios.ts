import axios from "axios";

const axiosInstance = axios.create({
    baseURL: 'http://localhost:8082',
});

export {axiosInstance}
