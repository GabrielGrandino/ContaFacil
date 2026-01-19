import axios from "axios";

export const http = axios.create({
  baseURL: "https://localhost:5026", // porta da sua API
  headers: {
    "Content-Type": "application/json",
  },
});
