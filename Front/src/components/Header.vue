<template>
  <div v-if="!isAdminRoute">
    <nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm fixed-top w-100">
      <div class="container">
        <RouterLink to="/" class="navbar-brand d-flex align-items-center">
          <span class="fw-bold text-dark">DocuVision</span>
        </RouterLink>

        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <RouterLink to="/" class="nav-link">Home</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink to="/about" class="nav-link">About</RouterLink>
            </li>
            <li v-if="authenticated" class="nav-item">
              <RouterLink to="/document" class="nav-link">Documents</RouterLink>
            </li>
            <li v-if="authenticated && user?.role === 'Admin'" class="nav-item">
              <RouterLink to="/admin/dashboard" class="nav-link text-danger fw-bold">
                <i class="bi bi-speedometer2 me-1"></i>
                Admin Dashboard
              </RouterLink>
            </li>
          </ul>

          <div class="d-flex align-items-center">
            <div v-if="authenticated" class="d-flex align-items-center">
              <span class="me-3 text-dark">
                <i class="bi bi-person me-2"></i>
                {{ user?.email }}
              </span>
              <RouterLink to="/profile" class="btn btn-outline-primary me-2">
                <i class="bi bi-person-circle me-2"></i> Profil
              </RouterLink>
              <button class="btn btn-outline-dark" @click="logout">
                <i class="bi bi-box-arrow-right me-2"></i> Déconnexion
              </button>
            </div>
            <div v-else>
              <RouterLink to="/login" class="btn btn-outline-primary">
                <i class="bi bi-box-arrow-in-right me-2"></i> Login
              </RouterLink>
            </div>
          </div>
        </div>
      </div>
    </nav>
    <div style="padding-top: 70px;"></div>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRouter, useRoute } from 'vue-router';

const store = useStore();
const router = useRouter();
const route = useRoute();

// Récupération des données du store Vuex
const authenticated = computed(() => store.state.authenticated);
const user = computed(() => store.state.user);

// Vérifier si on est sur une route admin
const isAdminRoute = computed(() => {
  return route.path.startsWith('/admin');
});

// Fonction de déconnexion
const logout = async () => {
  await store.dispatch('logout');  // Déclenche la mutation pour la déconnexion
  router.push('/login');  // Redirection vers la page de login après la déconnexion
};
</script>

<style scoped>
.navbar {
  z-index: 1000;
}
</style>
