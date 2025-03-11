<template>
    <div class="container">
      <h1>AI Describe Picture</h1>
      <p>Our AI converts images into detailed photo and picture descriptions. Easily describe a photo or convert an image to a description with accurate results. Try our image description services today</p>
    <div class="button-container">  
        <button class="get-started-btn" @click="handleGetStarted">Get Started</button>
    </div>
  </div>
  </template>
  
  <script>
  export default {
    name: 'Home',
    methods: {
        handleGetStarted() {
            // Accède à l'état authentification depuis Vuex
            const authenticated = this.$store.state.authenticated;
            console.log('Bouton cliqué');
            console.log('Utilisateur authentifié:', authenticated); // Vérification de l'authentification
  
            if (authenticated) {
                this.$router.push('/document'); // Si authentifié, rediriger vers /document
            } else {
                this.$router.push('/login'); // Sinon, rediriger vers /login
            }
        }
    }
  }
  </script>
  
  <style scoped>
  .container {
    max-width: 800px;
    margin: 0 auto;
    padding: 20px;
    text-align: center;
  }   
  
  h2 {
    font-size: 2rem;
    margin-bottom: 10px;
  }           
  
  p {
    font-size: 1.2rem;
    margin-bottom: 20px;
  }       
  
  .get-started-btn {
  background-color: #6366F1;
  color: white;
  padding: 15px 40px;
  border-radius: 9999px;
  border: none;
  font-size: 1.125rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 4px 6px -1px rgba(99, 102, 241, 0.3);
  }
  
  .get-started-btn:hover {
  background-color: #4F46E5;
  transform: translateY(-2px);
  box-shadow: 0 6px 15px -3px rgba(99, 102, 241, 0.4);
  }
  
  .get-started-btn:active {
  transform: translateY(0);
  box-shadow: 0 2px 4px -1px rgba(99, 102, 241, 0.3);
  }
  
  @media (max-width: 768px) {
  h1 {
    font-size: 2.5rem;
  }
  
  .get-started-btn {
    padding: 12px 32px;
    font-size: 1rem;
  }
  }
  </style>
  