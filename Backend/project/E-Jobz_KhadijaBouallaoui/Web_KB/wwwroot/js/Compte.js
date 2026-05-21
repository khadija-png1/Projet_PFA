    document.addEventListener("DOMContentLoaded", function() {
    // Sélectionne tous les radios du rôle
    const roleRadios = document.querySelectorAll('input[name="Role"]');
    const form = document.getElementById("registerForm");

    // Fonction pour vérifier si un rôle est sélectionné
    function checkRoleSelected() {
        let selected = false;
        roleRadios.forEach(radio => {
            if (radio.checked) selected = true;
        });

    // Afficher le formulaire seulement si un rôle est choisi
    form.style.display = selected ? "block" : "none";
    }

    // Ajouter l'événement sur chaque radio
    roleRadios.forEach(radio => {
        radio.addEventListener("change", checkRoleSelected);
    });

    // Vérifier au chargement (utile si un radio est déjà coché)
    checkRoleSelected();
});

