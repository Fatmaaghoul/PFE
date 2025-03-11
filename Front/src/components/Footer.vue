<template>
    <footer class="footer">
      <div class="footer-container">
        <!-- Section Logo et Description -->
        <div class="footer-section">
          <div class="logo-section">
            <img src="@/assets/logo.png" alt="DocuVision" class="footer-logo" />
            <p class="footer-description">Let AI help you manage your documents</p>
          </div>
        </div>
  
        <!-- Section Produits -->
        <div class="footer-section">
          <h3>PRODUCTS</h3>
          <ul>
            <li><router-link to="/document">Documents</router-link></li>
            <li><router-link to="/about">About Us</router-link></li>
            <li><router-link to="/profile">Profile</router-link></li>
          </ul>
        </div>
  
        <!-- Section Liens -->
        <div class="footer-section">
          <h3>LINKS</h3>
          <ul>
            <li><router-link to="/admin/dashboard">Admin Dashboard</router-link></li>
            <li><router-link to="/document">Document Management</router-link></li>
            <li><router-link to="/profile">User Profile</router-link></li>
          </ul>
        </div>
  
        <!-- Section Support -->
        <div class="footer-section">
          <h3>Support</h3>
          <p class="support-email">support@docuvision.com</p>
          
          <h3 class="mt-4">Legal</h3>
          <ul>
            <li><router-link to="/privacy">Privacy Policy</router-link></li>
            <li><router-link to="/terms">Terms of Service</router-link></li>
          </ul>
        </div>
      </div>
  
      <!-- Copyright et Social Media -->
      <div class="footer-bottom">
        <p>Copyright {{ currentYear }} DocuVision. All rights reserved.</p>
        <div class="social-links">
          <a href="mailto:support@docuvision.com" aria-label="Email">
            <i class="bi bi-envelope"></i>
          </a>
          <a href="https://twitter.com/docuvision" target="_blank" rel="noopener noreferrer" aria-label="Twitter">
            <i class="bi bi-twitter"></i>
          </a>
        </div>
      </div>
    </footer>
  </template>
  
  <script setup>
  import { computed } from 'vue';
  
  const currentYear = computed(() => new Date().getFullYear());
  </script>
  
  <style scoped>
  .footer {
    background-color: #f8f9fa;
    padding: 3rem 0 1rem 0;
    margin-top: 4rem;
    border-top: 1px solid #e9ecef;
  }
  
  .footer-container {
    max-width: 1200px;
    margin: 0 auto;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 2rem;
    padding: 0 1rem;
  }
  
  .footer-section {
    display: flex;
    flex-direction: column;
  }
  
  .logo-section {
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }
  
  .footer-logo {
    max-width: 150px;
    height: auto;
  }
  
  .footer-description {
    color: #6c757d;
    font-size: 0.9rem;
  }
  
  h3 {
    color: #343a40;
    font-size: 1rem;
    font-weight: 600;
    margin-bottom: 1rem;
  }
  
  ul {
    list-style: none;
    padding: 0;
    margin: 0;
  }
  
  li {
    margin-bottom: 0.5rem;
  }
  
  a {
    color: #6c757d;
    text-decoration: none;
    transition: color 0.3s ease;
  }
  
  a:hover {
    color: #0d6efd;
  }
  
  .support-email {
    color: #6c757d;
    margin-bottom: 1rem;
  }
  
  .footer-bottom {
    max-width: 1200px;
    margin: 2rem auto 0;
    padding: 1rem;
    border-top: 1px solid #e9ecef;
    display: flex;
    justify-content: space-between;
    align-items: center;
    color: #6c757d;
    font-size: 0.9rem;
  }
  
  .social-links {
    display: flex;
    gap: 1rem;
  }
  
  .social-links a {
    color: #6c757d;
    font-size: 1.2rem;
  }
  
  .social-links a:hover {
    color: #0d6efd;
  }
  
  @media (max-width: 768px) {
    .footer-container {
      grid-template-columns: repeat(2, 1fr);
    }
  }
  
  @media (max-width: 480px) {
    .footer-container {
      grid-template-columns: 1fr;
    }
  
    .footer-bottom {
      flex-direction: column;
      text-align: center;
      gap: 1rem;
    }
  }
  </style>