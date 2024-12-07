import axios from "axios";

const instance = axios.create({
  baseURL: "https://localhost:7032/api",
  // Note: The `httpsAgent` configuration is not applicable in the browser environment.
  // This configuration is typically used in Node.js environments.
});

instance.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default instance;
