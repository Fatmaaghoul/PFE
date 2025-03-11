<template>
  <div class="container mt-6">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="card shadow">
          <div class="card-body">
            <h2 class="card-title text-center mb-4">
              <i class="bi bi-person-circle me-2"></i>Mon Profil
            </h2>

            <div v-if="user" class="profile-info">
              <form @submit.prevent="updateProfile">
                <div class="mb-3">
                  <label class="form-label fw-bold">
                    <i class="bi bi-person me-2"></i>Nom d'utilisateur
                  </label>
                  <input v-model="editUser.userName" type="text" class="form-control" required />
                </div>

                <div class="mb-3">
                  <label class="form-label fw-bold">
                    <i class="bi bi-envelope me-2"></i>Email
                  </label>
                  <input v-model="editUser.email" type="email" class="form-control" required />
                </div>

                <div v-if="errorMessage" class="alert alert-danger" role="alert">
                  {{ errorMessage }}
                </div>
                <div v-if="successMessage" class="alert alert-success" role="alert">
                  {{ successMessage }}
                </div>

                <div class="text-center mt-4">
                  <button type="submit" class="btn btn-primary">
                    <i class="bi bi-save me-2"></i>Mettre à jour
                  </button>
                  
                </div>
              </form>
            </div>

            <div v-else class="text-center">
              <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Chargement...</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, ref, onMounted } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
import axios from "axios";

export default {
  name: "Profile",
  setup() {
    const store = useStore();
    const router = useRouter();

    const user = computed(() => store.state.user);
    const editUser = ref({ userName: "", email: "" });
    const errorMessage = ref("");
    const successMessage = ref("");

    onMounted(() => {
      if (user.value) {
        editUser.value.userName = user.value.userName;
        editUser.value.email = user.value.email;
      }
    });

    const updateProfile = async () => {
      try {
        const token = localStorage.getItem("token");
        const response = await axios.put(
          "/api/auth/edit-profile",
          {
            userName: editUser.value.userName,
            email: editUser.value.email,
          },
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );

        successMessage.value = response.data.Message;
        errorMessage.value = "";
        store.dispatch("fetchUser");
      } catch (error) {
        errorMessage.value = error.response?.data?.Message || "Erreur lors de la mise à jour.";
        successMessage.value = "";
      }
    };

   

    return { user, editUser, updateProfile, errorMessage, successMessage };
  },
};
</script>
