<template>
    <div class="container mt-5">
        <!-- Barre de navigation -->
       

        <!-- Titre de la page -->
        <h2 class="text-center mb-4">📂 </h2>

        <!-- Formulaire d'upload -->
        <UploadDocument @document-uploaded="fetchDocuments" /> <!-- Écouter l'événement -->

        <!-- Liste des documents -->
        <div v-if="loading" class="alert alert-info text-center">
            Chargement des documents...
        </div>
        <div v-else-if="documents.length > 0" class="card shadow-sm">
            <div class="card-body">
                <h5 class="card-title">📄 Documents</h5>
                <ul class="list-group">
                    <li v-for="doc in documents" :key="doc.id"
                        class="list-group-item d-flex justify-content-between align-items-center">
                        <a :href="doc.fileUrl" target="_blank" class="text-decoration-none">
                            <i class="fas fa-file-alt mr-2"></i>{{ doc.name }}
                        </a>
                        <button @click="deleteDocument(doc.id)" class="btn btn-danger btn-sm">
                            <i class="fas fa-trash"></i> Supprimer
                        </button>
                    </li>
                </ul>
            </div>
        </div>

        <!-- Message si aucun document -->
        <div v-else class="alert alert-info text-center">
            <i class="fas fa-info-circle"></i> Aucun document disponible.
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import Header from '@/components/Header.vue';
import UploadDocument from './UploadDocument.vue';
import { mapState } from "vuex";

export default {
    components: {
        Header,
        UploadDocument
    },
    created() {
        this.$store.dispatch("checkUserStatus");
    },
    computed: {
        ...mapState({
            authenticated: (state) => state.authenticated,
            user: (state) => state.user,
        }),
    },
    data() {
        return {
            documents: [],
            loading: true,
        };
    },
    methods: {
        async fetchDocuments() {
            try {
                    const response = await axios.get(`api/documents/user/${this.user.id}`);
                    this.documents = response.data;
               
            } catch (error) {
                console.error("Erreur lors de la récupération des documents", error);
            } finally {
                this.loading = false;
            }
        },
        async deleteDocument(documentId) {
            try {
                await axios.delete(`api/documents/${documentId}`);
                console.log("Document supprimé.");
                this.fetchDocuments();  // Rafraîchir la liste après suppression
            } catch (error) {
                console.error("Erreur lors de la suppression", error);
                alert("Échec de la suppression du document.");
            }
        },
    },
    mounted() {
      //  this.fetchDocuments();
    },
    watch: {
        user(newUser) {
            if (newUser && newUser.id) {
                this.fetchDocuments();
            }
        }
    }
};
</script>
