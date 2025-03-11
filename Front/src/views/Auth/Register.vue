<template>
  <div class="auth-container">
    <div class="auth-card">
      <div class="auth-content">
        <!-- Partie formulaire -->
        <div class="auth-form-container">
          <div class="logo-section">
            <img src="@/assets/logo.png" alt="Your Logo" class="logo" />
          </div>
          <h2>Sign Up</h2>
          <p class="subtitle">Create your account</p>

          <form @submit.prevent="submit">
            <div class="form-group">
              <input 
                v-model="data.name" 
                type="text" 
                placeholder="Full Name"
                class="form-input"
                required 
              />
            </div>
            <div class="form-group">
              <input 
                v-model="data.email" 
                type="email" 
                placeholder="Email"
                class="form-input"
                required 
              />
            </div>
            <div class="form-group">
              <div class="password-input">
                <input 
                  v-model="data.password" 
                  type="password"
                  placeholder="Password"
                  class="form-input"
                  required 
                />
              </div>
            </div>
            <div class="form-group">
              <div class="password-input">
                <input 
                  v-model="data.confirmPassword" 
                  type="password"
                  placeholder="Confirm Password"
                  class="form-input"
                  required 
                />
              </div>
            </div>

            <button type="submit" class="submit-btn">Create Account</button>
          </form>

          <p class="signup-text">
            Already have an account? 
            <router-link to="/login" class="signup-link">Log in</router-link>
          </p>
        </div>

        <!-- Partie illustration -->
        <div class="illustration">
          <img src="@/assets/register-illustration.png" alt="Register" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { reactive } from 'vue';
import { useRouter } from 'vue-router';

export default {
  name: "Register",
  setup() {
    const data = reactive({ 
      name: "", 
      email: "", 
      password: "", 
      confirmPassword: "" 
    });
    const router = useRouter();
    
    const submit = async () => {
      if (data.password !== data.confirmPassword) {
        alert("Les mots de passe ne correspondent pas !");
        return;
      }
      try {
        await axios.post('/api/auth/register', {
          username: data.name,
          password: data.password,
          email: data.email,
        });
        router.push('/login');
      } catch (error) {
        alert("Une erreur est survenue. Veuillez essayer à nouveau.");
      }
    };

    return { data, submit };
  },
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

.logo-section {
  text-align: center;
  margin-bottom: 1.5rem;
}

.logo {
  height: 40px;
  width: auto;
}

h2 {
  font-size: 1.75rem;
  font-weight: 600;
  color: #1e293b;
  text-align: center;
  margin-bottom: 0.5rem;
}

.subtitle {
  color: #64748b;
  text-align: center;
  margin-bottom: 2rem;
  font-size: 0.95rem;
}

.form-group {
  margin-bottom: 1.25rem;
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

.submit-btn:hover {
  background-color: #4338ca;
  transform: translateY(-1px);
}

.signup-text {
  text-align: center;
  color: #64748b;
  font-size: 0.95rem;
  margin-top: 1.5rem;
}

.signup-link {
  color: #4f46e5;
  text-decoration: none;
  font-weight: 500;
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
}
</style>
