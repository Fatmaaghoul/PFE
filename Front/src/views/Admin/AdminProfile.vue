<template>
  <div class="container-fluid">
    <!-- Header with navigation -->
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h1 class="h3 mb-0">Profile</h1>
      <nav aria-label="breadcrumb">
        <ol class="breadcrumb mb-0">
          <li class="breadcrumb-item active">Profile</li>
        </ol>
      </nav>
    </div>

    <!-- Profile section -->
    <div class="card shadow-sm mb-4">
      <div class="card-body">
        <div class="d-flex align-items-center mb-4">
          <!--<div class="profile-image me-3">  -->

                    <div class="avatar-circle bg-primary text-white me-2">
                      {{ user.email.charAt(0).toUpperCase() }}
                    </div>
          <div>
            <h2 class="h4 mb-1">{{ user?.email }}</h2>
            <p class="text-muted mb-0">{{ user?.roles }}</p>
            <p class="text-muted mb-0">{{ location }}</p>
          </div>
         
        </div>

        <!-- Social links -->
           <!--<div class="d-flex gap-2 mb-4">
          <a href="#" class="btn btn-outline-secondary rounded-circle">
            <i class="bi bi-facebook"></i>
          </a>
          <a href="#" class="btn btn-outline-secondary rounded-circle">
            <i class="bi bi-twitter-x"></i>
          </a>
          <a href="#" class="btn btn-outline-secondary rounded-circle">
            <i class="bi bi-linkedin"></i>
          </a>
          <a href="#" class="btn btn-outline-secondary rounded-circle">
            <i class="bi bi-instagram"></i>
          </a>
        </div>-->
      </div>
    </div>

    <!-- Personal Information -->
    <div class="card shadow-sm">
      <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-4">
          <h3 class="h5 mb-0">Personal Information</h3>
          <button class="btn btn-outline-primary btn-sm rounded-pill">
            <i class="bi bi-pencil me-2"></i>Edit
          </button>
        </div>

        <div class="row g-4">
          <div class="col-md-6">
            <div class="mb-3">
              <label class="form-label text-muted small">First Name</label>
              <input type="text" class="form-control-plaintext" :value="firstName" readonly />
            </div>
          </div>
          <div class="col-md-6">
            <div class="mb-3">
              <label class="form-label text-muted small">Last Name</label>
              <input type="text" class="form-control-plaintext" :value="lastName" readonly />
            </div>
          </div>
          <div class="col-md-6">
            <div class="mb-3">
              <label class="form-label text-muted small">Email address</label>
              <input type="email" class="form-control-plaintext" :value="user?.email" readonly />
            </div>
          </div>
          <div class="col-md-6">
            <div class="mb-3">
              <label class="form-label text-muted small">Phone</label>
              <input type="tel" class="form-control-plaintext" :value="phone" readonly />
            </div>
          </div>
          <div class="col-12">
            <div class="mb-3">
              <label class="form-label text-muted small">Bio</label>
              <input type="text" class="form-control-plaintext" :value="bio" readonly />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, ref } from 'vue';
import { useStore } from 'vuex';

export default {
  name: 'AdminProfile',
  setup() {
    const store = useStore();
    const user = computed(() => store.state.user);

    // Ces valeurs devraient venir de votre backend/store
    const firstName = ref('John');
    const lastName = ref('John');
    const phone = ref('+09 363 398 46');
    const bio = ref('Administrateur');
    const location = ref('..');

    return {
      user,
      firstName,
      lastName,
      phone,
      bio,
      location
    };
  }
};
</script>

<style scoped>
.profile-image img {
  object-fit: cover;
  border: 2px solid #fff;
  box-shadow: 0 0 10px rgba(0,0,0,0.1);
}

.form-control-plaintext {
  font-size: 1rem;
  color: #333;
  padding: 0.375rem 0;
}

.btn-outline-secondary {
  width: 40px;
  height: 40px;
  padding: 0;
  display: flex;
  align-items: center;
  justify-content: center;
}

.breadcrumb {
  background: transparent;
}

.card {
  border: none;
  border-radius: 10px;
}

.btn-outline-primary {
  border-radius: 20px;
  padding: 0.5rem 1.5rem;
}

.text-muted {
  color: #6c757d !important;
}
</style> 