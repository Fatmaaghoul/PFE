<template>
    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <h5 class="card-title">📤 Uploader un document PDF</h5>
            <form @submit.prevent="uploadDocument">
                <div class="form-group">
                    <input type="file" class="form-control" @change="handleFileUpload" accept=".pdf" />
                    <p v-if="file" class="mt-2 text-muted">📄 Fichier sélectionné : {{ file.name }}</p>
                </div>
                <button type="submit" class="btn btn-primary mt-2" :disabled="!file || !authenticated">
                    <i class="fas fa-upload"></i> Uploader
                </button>
            </form>
        </div>
    </div>
</template>

<script>
import axios from "axios";
import { mapState } from "vuex";

export default {
    created() {
        this.$store.dispatch("checkUserStatus"); // Vérifie si l'utilisateur est déjà connecté
    },
    computed: {
        ...mapState({
            authenticated: (state) => state.authenticated,
            user: (state) => state.user,
        }),
    },
    data() {
        return {
            formData: new FormData(),
            file: null,
        };
    },
    methods: {
        handleFileUpload(event) {
            const selectedFile = event.target.files[0];

            if (selectedFile) {
                if (selectedFile.type !== "application/pdf") {
                    alert("❌ Seuls les fichiers PDF sont autorisés.");
                    this.file = null;
                    event.target.value = ""; // Réinitialiser le champ fichier
                    return;
                }
                this.file = selectedFile;
            }
        },
        async uploadDocument() {
            if (!this.file) {
                alert("❌ Veuillez sélectionner un fichier PDF.");
                return;
            }
            if (!this.authenticated || !this.user?.id) {
                alert("❌ Vous devez être connecté pour uploader un document.");
                return;
            }
            this.formData.append("file", this.file);
          
            try {
                const response = await axios.post(`api/documents/upload?userId=${this.user.id}`, this.formData, {
                    headers: { "Content-Type": "multipart/form-data" }, withCredentials: true,
                });

                console.log("✅ Document uploadé :", response.data);
                alert("✅ Document PDF uploadé avec succès !");
                this.file = null; // Réinitialiser après l'upload

                // Rafraîchir la liste des documents après l'upload
                this.$emit('document-uploaded'); // Émettre un événement pour signaler que l'upload est terminé
            } catch (error) {
                console.error("❌ Erreur lors de l'upload", error);
                alert(error.response?.data || "❌ Échec de l'upload du document.");
            }
        }
    }
};
</script>
