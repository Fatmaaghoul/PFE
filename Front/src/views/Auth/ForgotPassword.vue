<template>
  <div class="auth-container">
    <div class="auth-card">
      <div class="auth-content">
        <!-- Partie formulaire -->
        <div class="auth-form-container">
          <div class="back-to-login">
            <router-link to="/login" class="back-link">
              <i class="bi bi-arrow-left"></i> Back to login
            </router-link>
          </div>

          <h2>Forgot your password?</h2>
          <p class="subtitle">Don't worry, happens to all of us. Enter your email below to recover your password</p>

          <form @submit.prevent="sendResetEmail">
            <div class="form-group">
              <label>Email</label>
              <input 
                v-model="email" 
                type="email" 
                placeholder="john.doe@gmail.com"
                class="form-input"
                required 
              />
            </div>

            <button type="submit" class="submit-btn" :disabled="loading">
              {{ loading ? 'Submitting...' : 'Submit' }}
            </button>
          </form>

          <p v-if="message" :class="['message', messageType]">{{ message }}</p>
        </div>

        <!-- Partie illustration -->
        <div class="illustration">
          <img src="@/assets/forgot-password-illustration.png" alt="Forgot Password" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { ref } from 'vue';

export default {
  name: 'ForgotPassword',
  setup() {
    const email = ref('');
    const loading = ref(false);
    const message = ref('');
    const messageType = ref('');

    const sendResetEmail = async () => {
      loading.value = true;
      message.value = '';
      
      try {
        const response = await axios.post('https://localhost:7186/api/auth/forgot-password', {
          email: email.value
        });
        
        message.value = response.data.message || response.data;
        messageType.value = 'success';
      } catch (error) {
        message.value = error.response?.data || 'An error occurred while sending the reset link';
        messageType.value = 'error';
      } finally {
        loading.value = false;
      }
    };

    return {
      email,
      loading,
      message,
      messageType,
      sendResetEmail
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

.back-to-login {
  margin-bottom: 2rem;
}

.back-link {
  color: #64748b;
  text-decoration: none;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 0.95rem;
}

.back-link:hover {
  color: #4f46e5;
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
  