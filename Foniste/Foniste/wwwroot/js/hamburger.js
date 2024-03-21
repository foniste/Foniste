const checkbox = document.getElementById('hamburger-checkbox');
const ustCizgi = document.querySelector('.hamburger-menu-buton-cizgiler-ust');
const ortaCizgi = document.querySelector('.hamburger-menu-buton-cizgiler-orta');
const altCizgi = document.querySelector('.hamburger-menu-buton-cizgiler-alt');

document.getElementById('hamburger-menu').addEventListener('click', function () {
    checkbox.checked = !checkbox.checked;

    if (checkbox.checked) {
        ustCizgi.style.transform = 'rotate(-45deg)';
        ortaCizgi.style.opacity = '0';
        altCizgi.style.transform = 'rotate(45deg)';
    } else {
        ustCizgi.style.transform = 'rotate(0)';
        ortaCizgi.style.opacity = '1';
        altCizgi.style.transform = 'rotate(0)';
    }
});
