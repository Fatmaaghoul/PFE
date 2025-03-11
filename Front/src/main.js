import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import Orion from '@orion.ui/orion'; // Import de la librairie Orion
import '@orion.ui/orion/dist/style.css'; // Import des styles
import '@orion.ui/orion/dist/monkey-patching';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css'; // Import Bootstrap Icons
import 'bootstrap';
import '@fortawesome/fontawesome-free/css/all.css';
import store from './Store';  // Import du store

// Définir l'URL de base pour Axios
axios.defaults.baseURL = "https://localhost:7055";

// Add a request interceptor
axios.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
      config.headers.Accept = 'application/json';
      config.headers['Content-Type'] = 'application/json';
    }
    console.log('Request Config:', {
      url: config.url,
      method: config.method,
      headers: config.headers,
      baseURL: config.baseURL
    });
    return config;
  },
  error => {
    console.error('Request Error:', error);
    return Promise.reject(error);
  }
);

// Add a response interceptor
axios.interceptors.response.use(
  response => {
    console.log('API Response:', {
      url: response.config.url,
      method: response.config.method,
      status: response.status,
      data: response.data,
      headers: response.config.headers
    });
    return response;
  },
  error => {
    console.error('API Error:', {
      url: error.config?.url,
      method: error.config?.method,
      status: error.response?.status,
      data: error.response?.data,
      message: error.message,
      headers: error.config?.headers,
      baseURL: error.config?.baseURL
    });
    return Promise.reject(error);
  }
);

// Créer l'application Vue
const app = createApp(App);

// Utiliser les plugins et le store
app.use(router);
app.use(Orion);
app.use(store);

// Vérifier le statut de l'utilisateur au démarrage de l'application
store.dispatch('checkUserStatus');

app.mount('#app');
