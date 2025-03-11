import { createStore } from 'vuex'; 

export default createStore({
  state: {
    authenticated: false,
    user: null
  },
  mutations: {
    SET_AUTH(state, { authenticated, user }) {
      state.authenticated = authenticated;
      state.user = user;
    },
    LOGOUT(state) {
      state.authenticated = false;
      state.user = null;
    }
  },
  actions: {
    login({ commit }, user) {
      commit('SET_AUTH', { authenticated: true, user });
    },
    logout({ commit }) {
      localStorage.removeItem('token');
      commit('LOGOUT');
    },
    checkUserStatus({ commit }) {
      const token = localStorage.getItem('token');
      if (token) {
        try {
          // Decode the JWT token
          const base64Url = token.split('.')[1];
          const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
          const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
          }).join(''));
          const userData = JSON.parse(jsonPayload);
          
          commit('SET_AUTH', { 
            authenticated: true, 
            user: {
              email: userData.email,
              role: userData.role
            }
          });
        } catch (error) {
          console.error('Error decoding token:', error);
          localStorage.removeItem('token');
          commit('LOGOUT');
        }
      } else {
        commit('SET_AUTH', { authenticated: false, user: null });
      }
    }
  },
  getters: {
    isAuthenticated(state) {
      return state.authenticated;
    },
    user(state) {
      return state.user;
    }
  }
});