<template>
  <div class="dashboard-container">
    <!-- Stats Cards Row -->
    <div class="row mb-4">
      <div class="col-md-4">
        <div class="card stats-card bg-gradient-primary">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <h6 class="text-white mb-1">Total Utilisateurs</h6>
                <h3 class="text-white mb-0">{{ users.length }}</h3>
                <small class="text-white-50">Augmenté de 5%</small>
              </div>
              <div class="stats-icon">
                <i class="bi bi-people-fill"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card stats-card bg-gradient-info">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <h6 class="text-white mb-1">Administrateurs</h6>
                <h3 class="text-white mb-0">{{ adminCount }}</h3>
                <small class="text-white-50">Stable</small>
              </div>
              <div class="stats-icon">
                <i class="bi bi-shield-check"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-4">
        <div class="card stats-card bg-gradient-success">
          <div class="card-body">
            <div class="d-flex justify-content-between align-items-center">
              <div>
                <h6 class="text-white mb-1">Utilisateurs Actifs</h6>
                <h3 class="text-white mb-0">{{ users.length - adminCount }}</h3>
                <small class="text-white-50">Augmenté de 10%</small>
              </div>
              <div class="stats-icon">
                <i class="bi bi-person-check-fill"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Main Content Card -->
    <div class="card shadow-sm">
      <div class="card-header bg-white py-3">
        <div class="d-flex justify-content-between align-items-center">
          <div>
            <h3 class="card-title mb-0">
              <i class="bi bi-people me-2"></i>Gestion des utilisateurs
            </h3>
            <p class="text-muted mb-0 mt-1">Gérez tous les utilisateurs de votre système</p>
          </div>
          <div class="d-flex gap-2">
            <button class="btn btn-success" @click="showAddModal = true">
              <i class="bi bi-person-plus me-2"></i>Nouvel utilisateur
            </button>
            <button class="btn btn-primary" @click="refreshUsers">
              <i class="bi bi-arrow-clockwise me-2"></i>Actualiser
            </button>
          </div>
        </div>
      </div>
      <div class="card-body">
        <div v-if="loading" class="text-center py-5">
          <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Chargement...</span>
          </div>
        </div>
        <div v-else-if="users.length > 0" class="table-responsive">
          <table class="table table-hover align-middle">
            <thead class="table-light">
              <tr>
                <th>ID</th>
                <th>Nom</th>
                <th>Email</th>
                <th>Rôle</th>
                <th class="text-end">Actions</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="user in users" :key="user.id">
                <td>{{ user.id }}</td>
                <td>
                  <div class="d-flex align-items-center">
                    <div class="avatar-circle bg-primary text-white me-2">
                      {{ user.username.charAt(0).toUpperCase() }}
                    </div>
                    {{ user.username }}
                  </div>
                </td>
                <td>{{ user.email }}</td>
                <td>
                  <span :class="[
                    'badge',
                    user.roles.includes('Admin') ? 'bg-danger' : 'bg-primary'
                  ]">
                    {{ user.roles.join(', ') }}
                  </span>
                </td>
                <td class="text-end">
                  <div class="btn-group">
                    <button
                      class="btn btn-outline-primary btn-sm"
                      @click="editUser(user)"
                      title="Modifier l'utilisateur"
                    >
                      <i class="bi bi-pencil"></i>
                    </button>
                    <button
                      class="btn btn-outline-warning btn-sm"
                      @click="toggleRole(user)"
                      :title="user.roles.includes('Admin') ? 'Rétrograder en utilisateur' : 'Promouvoir en admin'"
                    >
                      <i class="bi" :class="user.roles.includes('Admin') ? 'bi-person-down' : 'bi-person-up'"></i>
                    </button>
                    <button 
                      class="btn btn-outline-danger btn-sm"
                      @click="deleteUser(user.id)"
                      title="Supprimer l'utilisateur"
                    >
                      <i class="bi bi-trash"></i>
                    </button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div v-else class="text-center py-5">
          <i class="bi bi-people text-muted" style="font-size: 3rem;"></i>
          <p class="text-muted mt-3">Aucun utilisateur trouvé.</p>
          <button class="btn btn-outline-primary mt-3" @click="refreshUsers">
            <i class="bi bi-arrow-clockwise me-2"></i>Réessayer
          </button>
        </div>
      </div>
    </div>

    <!-- Modal Ajout/Modification -->
    <div class="modal fade" :class="{ show: showAddModal || showEditModal }" 
         :style="{ display: (showAddModal || showEditModal) ? 'block' : 'none' }" 
         tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              {{ showEditModal ? 'Modifier l\'utilisateur' : 'Nouvel utilisateur' }}
            </h5>
            <button type="button" class="btn-close" @click="closeModal"></button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="submitForm">
              <div class="mb-3">
                <label class="form-label">Nom d'utilisateur</label>
                <input v-model="formData.username" type="text" class="form-control" required>
              </div>
              <div class="mb-3">
                <label class="form-label">Email</label>
                <input v-model="formData.email" type="email" class="form-control" required>
              </div>
              <div v-if="showAddModal" class="mb-3">
                <label class="form-label">Mot de passe</label>
                <input v-model="formData.password" type="password" class="form-control" required>
              </div>
              <div class="mb-3">
                <label class="form-label">Rôle</label>
                <select v-model="formData.roles" class="form-select" multiple>
                  <option value="User">Utilisateur</option>
                  <option value="Admin">Administrateur</option>
                </select>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" @click="closeModal">Annuler</button>
            <button type="button" class="btn btn-primary" @click="submitForm">
              {{ showEditModal ? 'Modifier' : 'Créer' }}
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';

export default {
  name: "Users",
  setup() {
    const users = ref([]);
    const loading = ref(true);
    const router = useRouter();
    const store = useStore();
    const showAddModal = ref(false);
    const showEditModal = ref(false);
    const formData = ref({
      username: '',
      email: '',
      password: '',
      roles: ['User']
    });
    const editingUserId = ref(null);

    // Computed properties for statistics
    const adminCount = computed(() => {
      return users.value.filter(user => user.roles.includes('Admin')).length;
    });

    const checkAdminAccess = () => {
      const token = localStorage.getItem('token');
      if (!token) {
        console.error("No token found");
        store.dispatch('logout');
        router.push('/login');
        return false;
      }

      const tokenParts = token.split('.');
      const payload = JSON.parse(atob(tokenParts[1]));
      console.log("Token payload in checkAdminAccess:", payload);

      const userRole = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      console.log("User role from token:", userRole);

      if (userRole !== 'Admin') {
        console.error("User is not an admin");
        alert("Accès non autorisé. Cette page est réservée aux administrateurs.");
        router.push('/Document');
        return false;
      }

      return true;
    };

    const fetchUsers = async () => {
      if (!checkAdminAccess()) {
        loading.value = false;
        return;
      }
      
      loading.value = true;
      try {
        console.log("Fetching users from API...");
        const token = localStorage.getItem('token');
        console.log("Current token:", token);
        
        if (!token) {
          console.error("No token found");
          store.dispatch('logout');
          router.push('/login');
          return;
        }

        console.log("Making API request to: /api/users/all");
        
        const response = await axios({
          method: 'get',
          url: '/api/users/all',
          headers: {
            'Authorization': `Bearer ${token}`,
            'Accept': '*/*',
            'Content-Type': 'application/json'
          },
          timeout: 10000,
          validateStatus: function (status) {
            return status >= 200 && status < 500;
          }
        });
        
        console.log("API Response:", response);
        
        if (Array.isArray(response.data)) {
          users.value = response.data;
          console.log("Users array updated:", users.value);
        } else {
          console.error("Response data is not an array:", response.data);
          users.value = [];
        }
      } catch (error) {
        console.error("Error details:", error);
        handleError(error);
      } finally {
        loading.value = false;
        console.log("Loading state set to false");
      }
    };

    const handleError = (error) => {
      let errorMessage = "Erreur lors de la récupération des utilisateurs.";
      if (error.response?.status === 401) {
        errorMessage = "Vous n'êtes pas autorisé à accéder à cette page. Veuillez vous reconnecter.";
        store.dispatch('logout');
        router.push('/login');
      } else if (error.response?.status === 403) {
        errorMessage = "Accès refusé. Cette page est réservée aux administrateurs.";
        router.push('/Document');
      } else if (error.response?.status === 404) {
        errorMessage = `L'endpoint n'a pas été trouvé. URL: ${error.config?.baseURL}${error.config?.url}\nVeuillez vérifier que le serveur backend est en cours d'exécution.`;
      } else if (error.response?.data?.message) {
        errorMessage = error.response.data.message;
      }
      
      alert(errorMessage);
      users.value = [];
    };

    const refreshUsers = () => {
      fetchUsers();
    };

    const deleteUser = async (userId) => {
      if (!confirm("Êtes-vous sûr de vouloir supprimer cet utilisateur ? Cette action est irréversible.")) return;

      try {
        await axios.delete(`/api/users/delete/${userId}`);
        users.value = users.value.filter(user => user.id !== userId);
      } catch (error) {
        console.error("Error details:", error);
        alert("Une erreur est survenue lors de la suppression de l'utilisateur.");
      }
    };

    const toggleRole = async (user) => {
      try {
        const newRoles = user.roles.includes('Admin') 
          ? ['User'] 
          : ['Admin'];
        
        await axios.put(`/api/users/edit/${user.id}`, 
          { 
            username: user.username,
            email: user.email,
            roles: newRoles
          }
        );
        
        user.roles = newRoles;
      } catch (error) {
        console.error("Error details:", error);
        alert("Une erreur est survenue lors du changement de rôle.");
      }
    };

    const editUser = (user) => {
      editingUserId.value = user.id;
      formData.value = {
        username: user.username,
        email: user.email,
        roles: [...user.roles]
      };
      showEditModal.value = true;
    };

    const closeModal = () => {
      showAddModal.value = false;
      showEditModal.value = false;
      editingUserId.value = null;
      formData.value = {
        username: '',
        email: '',
        password: '',
        roles: ['User']
      };
    };

    const submitForm = async () => {
      try {
        const token = localStorage.getItem('token');
        if (!token) {
          console.error("No token found");
          store.dispatch('logout');
          router.push('/login');
          return;
        }

        if (showEditModal.value) {
          console.log("Updating user:", {
            id: editingUserId.value,
            data: {
              username: formData.value.username,
              email: formData.value.email,
              roles: formData.value.roles
            }
          });
          
          await axios.put(`/api/users/edit/${editingUserId.value}`, {
            username: formData.value.username,
            email: formData.value.email,
            roles: formData.value.roles
          }, {
            headers: {
              'Authorization': `Bearer ${token}`,
              'Accept': '*/*',
              'Content-Type': 'application/json'
            }
          });
        } else {
          // Convertir le Proxy en tableau simple
          const rolesArray = Array.from(formData.value.roles);
          
          const userData = {
            username: formData.value.username,
            email: formData.value.email,
            password: formData.value.password,
            roles: rolesArray
          };

          console.log("Creating new user with data:", userData);
          
          const response = await axios.post('/api/users/add', userData, {
            headers: {
              'Authorization': `Bearer ${token}`,
              'Accept': '*/*',
              'Content-Type': 'application/json'
            }
          });
          
          console.log("Create user response:", response.data);
        }
        
        closeModal();
        refreshUsers();
      } catch (error) {
        console.error("Error details:", {
          message: error.message,
          response: error.response?.data,
          status: error.response?.status,
          statusText: error.response?.statusText
        });
        
        let errorMessage = "Une erreur est survenue lors de l'enregistrement de l'utilisateur.";
        if (error.response?.data?.message) {
          errorMessage = error.response.data.message;
        }
        
        alert(errorMessage);
      }
    };

    onMounted(fetchUsers);

    return {
      users,
      loading,
      showAddModal,
      showEditModal,
      formData,
      deleteUser,
      toggleRole,
      refreshUsers,
      editUser,
      closeModal,
      submitForm,
      adminCount
    };
  },
};
</script>

<style scoped>
.dashboard-container {
  padding: 1.5rem;
}

.stats-card {
  border: none;
  border-radius: 10px;
  transition: transform 0.2s;
}

.stats-card:hover {
  transform: translateY(-5px);
}

.bg-gradient-primary {
  background: linear-gradient(45deg, #4e73df 0%, #224abe 100%);
}

.bg-gradient-info {
  background: linear-gradient(45deg, #36b9cc 0%, #1a8a9c 100%);
}

.bg-gradient-success {
  background: linear-gradient(45deg, #1cc88a 0%, #13855c 100%);
}

.stats-icon {
  font-size: 2rem;
  opacity: 0.8;
  color: white;
}

.avatar-circle {
  width: 35px;
  height: 35px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
}

.card {
  border: none;
  border-radius: 10px;
  box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15);
}

.card-header {
  border-bottom: 1px solid rgba(0,0,0,.125);
  border-radius: 10px 10px 0 0 !important;
  background: white;
}

.table th {
  font-weight: 600;
  text-transform: uppercase;
  font-size: 0.85rem;
  letter-spacing: 0.5px;
}

.badge {
  padding: 0.5em 0.75em;
  font-weight: 500;
}

.btn-group .btn {
  padding: 0.375rem 0.75rem;
}

.btn-group .btn i {
  font-size: 0.875rem;
}

.modal {
  background-color: rgba(0, 0, 0, 0.5);
}

.table-responsive {
  border-radius: 0.5rem;
}

.table {
  margin-bottom: 0;
}

.table thead th {
  border-top: none;
}
</style>
