<template>
  <div class="auth-container">
    <div class="auth-card">
      <div class="auth-content">
        <!-- Partie formulaire -->
        <div class="auth-form-container">
          <h2>Set a password</h2>
          <p class="subtitle">Your previous password has been reset. Please set a new password for your account.</p>

          <form @submit.prevent="resetPassword">
            <div class="form-group">
              <label>Create Password</label>
              <div class="password-input">
                <input 
                  v-model="newPassword" 
                  :type="showPassword ? 'text' : 'password'"
                  placeholder="Enter your new password"
                  class="form-input"
                  required 
                />
                <button 
                  type="button" 
                  class="toggle-password"
                  @click="showPassword = !showPassword"
                >
                  <i class="bi" :class="showPassword ? 'bi-eye-slash' : 'bi-eye'"></i>
                </button>
              </div>
            </div>

            <div class="form-group">
              <label>Re-enter Password</label>
              <div class="password-input">
                <input 
                  v-model="confirmPassword" 
                  :type="showConfirmPassword ? 'text' : 'password'"
                  placeholder="Confirm your new password"
                  class="form-input"
                  required 
                />
                <button 
                  type="button" 
                  class="toggle-password"
                  @click="showConfirmPassword = !showConfirmPassword"
                >
                  <i class="bi" :class="showConfirmPassword ? 'bi-eye-slash' : 'bi-eye'"></i>
                </button>
              </div>
            </div>

            <button type="submit" class="submit-btn" :disabled="loading">
              {{ loading ? 'Setting password...' : 'Set password' }}
            </button>
          </form>

          <p v-if="message" :class="['message', status]">{{ message }}</p>
        </div>

        <!-- Partie illustration -->
        <div class="illustration">
          <img src="@/assets/reset-password-illustration.png" alt="Reset Password" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

export default {
  setup() {
    const newPassword = ref('');
    const confirmPassword = ref('');
    const message = ref('');
    const status = ref('');
    const loading = ref(false);
    const showPassword = ref(false);
    const showConfirmPassword = ref(false);
    const route = useRoute();
    const router = useRouter();
    const token = ref('');
    const email = ref('');

    onMounted(() => {
      // Récupérer et nettoyer le token
      const rawToken = route.query.token || '';
      token.value = decodeURIComponent(rawToken).replace(/ /g, '+');
      email.value = route.query.email || '';
      
      if (!token.value || !email.value) {
        message.value = "Token ou email manquant dans l'URL";
        status.value = 'error';
      }
    });

    const resetPassword = async () => {
      if (newPassword.value !== confirmPassword.value) {
        message.value = "Les mots de passe ne correspondent pas";
        status.value = 'error';
        return;
      }

      if (!token.value || !email.value) {
        message.value = "Token ou email manquant";
        status.value = 'error';
        return;
      }

      loading.value = true;
      try {
        const response = await axios.post('https://localhost:7186/api/auth/reset-password', {
          email: email.value,
          token: token.value,
          newPassword: newPassword.value
        });

        message.value = "Mot de passe réinitialisé avec succès";
        status.value = 'success';
        setTimeout(() => {
          router.push('/login');
        }, 3000);
      } catch (error) {
        message.value = error.response?.data?.message || error.response?.data || "Une erreur est survenue";
        status.value = 'error';
      } finally {
        loading.value = false;
      }
    };

    return { 
      newPassword,
      confirmPassword,
      message,
      resetPassword,
      status,
      loading,
      showPassword,
      showConfirmPassword
    };
  }
};
</script>

<style scoped>
.auth-container {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #f8fafc;
  padding: 2rem;
}

.auth-card {
  background: white;
  border-radius: 20px;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 1000px;
  overflow: hidden;
}

.auth-content {
  display: flex;
  align-items: stretch;
}

.auth-form-container {
  flex: 1;
  padding: 2.5rem;
  max-width: 450px;
}

h2 {
  font-size: 1.75rem;
  font-weight: 600;
  color: #1e293b;
  margin-bottom: 0.5rem;
}

.subtitle {
  color: #64748b;
  margin-bottom: 2rem;
  font-size: 0.95rem;
  line-height: 1.5;
}

.form-group {
  margin-bottom: 1.25rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  color: #4b5563;
  font-size: 0.95rem;
}

.form-input {
  width: 100%;
  padding: 0.875rem 1rem;
  border: 1px solid #e2e8f0;
  border-radius: 8px;
  font-size: 0.95rem;
  transition: all 0.2s;
}

.form-input:focus {
  outline: none;
  border-color: #4f46e5;
  box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1);
}

.password-input {
  position: relative;
}

.toggle-password {
  position: absolute;
  right: 1rem;
  top: 50%;
  transform: translateY(-50%);
  background: none;
  border: none;
  color: #64748b;
  cursor: pointer;
  padding: 0;
}

.submit-btn {
  width: 100%;
  padding: 0.875rem;
  background-color: #4f46e5;
  color: white;
  border: none;
  border-radius: 8px;
  font-weight: 500;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.2s;
  margin-top: 1rem;
}

.submit-btn:hover:not(:disabled) {
  background-color: #4338ca;
  transform: translateY(-1px);
}

.submit-btn:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.message {
  margin-top: 1rem;
  padding: 0.75rem;
  border-radius: 6px;
  font-size: 0.95rem;
}

.message.success {
  background-color: #f0fdf4;
  color: #166534;
  border: 1px solid #dcfce7;
}

.message.error {
  background-color: #fef2f2;
  color: #991b1b;
  border: 1px solid #fee2e2;
}

.illustration {
  flex: 1;
  background-color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 2rem;
}

.illustration img {
  width: 100%;
  height: auto;
  max-width: 400px;
  object-fit: contain;
}

@media (max-width: 1024px) {
  .illustration {
    display: none;
  }

  .auth-form-container {
    max-width: none;
    margin: 0 auto;
  }

  .auth-card {
    max-width: 450px;
  }
}

@media (max-width: 640px) {
  .auth-container {
    padding: 1rem;
  }

  .auth-form-container {
    padding: 1.5rem;
  }

  h2 {
    font-size: 1.5rem;
  }
}
</style>