// ══ DROPDOWN DESCARGAS ══
document.querySelectorAll('.nav-dropdown').forEach(dropdown => {
  let closeTimer = null;

  dropdown.addEventListener('mouseenter', () => {
    clearTimeout(closeTimer);
    dropdown.classList.add('open');
  });

  dropdown.addEventListener('mouseleave', () => {
    closeTimer = setTimeout(() => {
      dropdown.classList.remove('open');
    }, 150);
  });

  document.addEventListener('click', (e) => {
    if (!dropdown.contains(e.target)) {
      dropdown.classList.remove('open');
    }
  });
});


// ══ DOTS CARRUSEL ══
const dots = document.querySelectorAll('.dot');
let current = 0;

dots.forEach((dot, i) => {
  dot.addEventListener('click', () => {
    dots[current].classList.remove('active');
    current = i;
    dot.classList.add('active');
  });
});

// Auto-rotación cada 3 segundos
setInterval(() => {
  dots[current].classList.remove('active');
  current = (current + 1) % dots.length;
  dots[current].classList.add('active');
}, 3000);


// ══ BOTÓN PLAY ══
const playBtn = document.getElementById('playBtn');
if (playBtn) {
  playBtn.addEventListener('click', () => {
    // Aquí puedes abrir un modal con video o redirigir al link de YouTube
    window.open('https://www.youtube.com/watch?v=_HxbiAT4u6g', '_blank');
  });
}


// ══ ANIMACIÓN DE ENTRADA AL SCROLL ══
const observer = new IntersectionObserver((entries) => {
  entries.forEach(entry => {
    if (entry.isIntersecting) {
      entry.target.style.opacity = '1';
      entry.target.style.transform = 'translateY(0)';
    }
  });
}, { threshold: 0.15 });

document.querySelectorAll('.feature-item, .stat-item, .how-left').forEach(el => {
  el.style.opacity = '0';
  el.style.transform = 'translateY(24px)';
  el.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
  observer.observe(el);
});