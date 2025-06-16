// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const toggleBtn = document.getElementById('toggleTheme');
console.log("boton", toggleBtn)
if (toggleBtn) {
    const themeKey = 'bs-theme';
    const html = document.documentElement;

    const savedTheme = localStorage.getItem(themeKey);
    if (savedTheme) {
        html.setAttribute('data-bs-theme', savedTheme);
    }

    toggleBtn.addEventListener('click', () => {
        const currentTheme = html.getAttribute('data-bs-theme');
        const newTheme = currentTheme === 'light' ? 'dark' : 'light';
        html.setAttribute('data-bs-theme', newTheme);
        localStorage.setItem(themeKey, newTheme);
    });
}

function scrollCards(direction) {
    const container = document.getElementById('scrollContainer');
    const scrollAmount = container.offsetWidth * 0.8; // scroll por 80% del ancho visible
    container.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
}