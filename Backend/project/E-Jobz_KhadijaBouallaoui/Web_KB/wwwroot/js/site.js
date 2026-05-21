document.addEventListener("DOMContentLoaded", function () {
    const navbarCollapse = document.getElementById('navbarResponsive');
    if (!navbarCollapse) return;

    const bsCollapse = new bootstrap.Collapse(navbarCollapse, { toggle: false });

    navbarCollapse.querySelectorAll('a').forEach(function (link) {
        link.addEventListener('click', function () {
            if (navbarCollapse.classList.contains('show')) {
                bsCollapse.hide(); // Ferme le menu
            }
        });
    });
});
 