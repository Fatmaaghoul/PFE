<template>
  <div class="admin-layout">
    <!-- Sidebar -->
    <div class="sidebar" :class="{ 'collapsed': isSidebarCollapsed }">
      <div class="sidebar-header">
        <div class="logo-container">
          <i class="bi bi-grid-3x3-gap-fill logo-icon"></i>
          <span v-if="!isSidebarCollapsed">Admin Panel</span>
        </div>
        <button class="collapse-btn" @click="toggleSidebar">
          <i class="bi" :class="isSidebarCollapsed ? 'bi-chevron-right' : 'bi-chevron-left'"></i>
        </button>
      </div>

      <div class="user-info" v-if="!isSidebarCollapsed" @click="goToProfile" style="cursor: pointer;">
        <div class="avatar">
          <i class="bi bi-person-circle"></i>
        </div>
        <div class="user-details">
          <h6 class="mb-0">{{ user?.email }}</h6>
          <span class="text-muted">{{ user?.role }}</span>
        
        </div>
      </div>

      <nav class="sidebar-nav">
        <router-link to="/admin/dashboard" class="nav-item" :class="{ 'collapsed': isSidebarCollapsed }">
          <i class="bi bi-house-door"></i>
          <span v-if="!isSidebarCollapsed">Dashboard</span>
        </router-link>

        <router-link to="/admin/users" class="nav-item" :class="{ 'collapsed': isSidebarCollapsed }">
          <i class="bi bi-people"></i>
          <span v-if="!isSidebarCollapsed">Utilisateurs</span>
        </router-link>

        <div class="nav-section" v-if="!isSidebarCollapsed">
          <h6 class="nav-section-title">Interface</h6>
        </div>

        <router-link to="/admin/documents" class="nav-item" :class="{ 'collapsed': isSidebarCollapsed }">
          <i class="bi bi-file-earmark-text"></i>
          <span v-if="!isSidebarCollapsed">Documents</span>
        </router-link>

        <router-link to="/admin/settings" class="nav-item" :class="{ 'collapsed': isSidebarCollapsed }">
          <i class="bi bi-gear"></i>
          <span v-if="!isSidebarCollapsed">Paramètres</span>
        </router-link>
      </nav>

      <div class="sidebar-footer" v-if="!isSidebarCollapsed">
        <button class="btn btn-logout" @click="logout">
          <i class="bi bi-box-arrow-right"></i>
          <span>Déconnexion</span>
        </button>
      </div>
    </div>

    <!-- Main Content -->
    <div class="main-content" :class="{ 'expanded': isSidebarCollapsed }">
      <div class="top-bar">
        <div class="d-flex align-items-center">
           <!-- <button class="btn btn-link text-dark me-3" @click="toggleSidebar">
            <i class="bi bi-list fs-4"></i>
          </button>-->
          <div class="search-box">
            <i class="bi bi-search"></i>
            <input type="text" placeholder="Rechercher..." />
          </div>
        </div>
        <div class="top-bar-right">
          <button class="btn btn-link text-dark">
            <i class="bi bi-bell"></i>
          </button>
          <button class="btn btn-link text-dark">
            <i class="bi bi-gear"></i>
          </button>
        </div>
      </div>

      <!-- Router View -->
      <div class="content-wrapper">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, computed } from 'vue';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';

export default {
  name: 'AdminLayout',
  setup() {
    const router = useRouter();
    const store = useStore();
    const isSidebarCollapsed = ref(false);
    const user = computed(() => store.state.user);

    const logout = async () => {
      await store.dispatch('logout');
      router.push('/login');
    };


    const toggleSidebar = () => {
      isSidebarCollapsed.value = !isSidebarCollapsed.value;
    };
    const goToProfile = () => {
      router.push('/admin/profile');
    };

    
    return {
      isSidebarCollapsed,
      toggleSidebar,
      user,
      goToProfile,
      logout
    };
  }
};
</script>

<style scoped>
.admin-layout {
  display: flex;
  min-height: 100vh;
}

.sidebar {
  width: 260px;
  background: #fff;
  border-right: 1px solid #e0e0e0;
  display: flex;
  flex-direction: column;
  transition: all 0.3s ease;
  box-shadow: 0 0 15px rgba(0,0,0,0.05);
  position: fixed;
  height: 100vh;
  z-index: 1000;
}

.sidebar.collapsed {
  width: 70px;
}

.sidebar-header {
  padding: 1.5rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-bottom: 1px solid #e0e0e0;
}

.logo-container {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.logo-icon {
  font-size: 2rem;
  color: #4e73df;
}

.collapse-btn {
  border: none;
  background: none;
  color: #6c757d;
  cursor: pointer;
}

.user-info {
  padding: 1.5rem;
  display: flex;
  align-items: center;
  gap: 1rem;
  border-bottom: 1px solid #e0e0e0;
}

.avatar {
  display: flex;
  align-items: center;
  justify-content: center;
}

.avatar i {
  font-size: 2.5rem;
  color: #4e73df;
}

.sidebar-nav {
  padding: 1rem 0;
  flex-grow: 1;
}

.nav-section {
  padding: 1rem 1.5rem 0.5rem;
}

.nav-section-title {
  color: #6c757d;
  font-size: 0.75rem;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.nav-item {
  display: flex;
  align-items: center;
  padding: 0.75rem 1.5rem;
  color: #6c757d;
  text-decoration: none;
  transition: all 0.3s ease;
  gap: 1rem;
}

.nav-item:hover, .nav-item.router-link-active {
  color: #4e73df;
  background: rgba(78, 115, 223, 0.1);
}

.nav-item.collapsed {
  padding: 0.75rem;
  justify-content: center;
}

.nav-item i {
  font-size: 1.2rem;
}

.sidebar-footer {
  padding: 1rem 1.5rem;
  border-top: 1px solid #e0e0e0;
}

.btn-logout {
  width: 100%;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: #dc3545;
  background: none;
  border: 1px solid #dc3545;
  padding: 0.5rem 1rem;
  border-radius: 5px;
  transition: all 0.3s ease;
}

.btn-logout:hover {
  background: #dc3545;
  color: white;
}

.main-content {
  flex-grow: 1;
  background: #f8f9fc;
  transition: all 0.3s ease;
  margin-left: 260px;
}

.main-content.expanded {
  margin-left: 70px;
}

.top-bar {
  background: white;
  padding: 1rem 2rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #e0e0e0;
}

.search-box {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  background: #f8f9fc;
  padding: 0.5rem 1rem;
  border-radius: 5px;
}

.search-box input {
  border: none;
  background: none;
  outline: none;
  width: 200px;
}

.top-bar-right {
  display: flex;
  gap: 1rem;
}

.content-wrapper {
  padding: 2rem;
  height: calc(100vh - 70px);
  overflow-y: auto;
}
</style> 