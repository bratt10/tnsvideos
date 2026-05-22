// ── Navegación sidebar (otras secciones) ──
function toggleNav(item) {
  const sub = item.nextElementSibling;
  if (!sub || !sub.classList.contains('nav-sub')) return;
  const open = sub.classList.toggle('open');
  const chevron = item.querySelector('.nav-chevron');
  if (chevron) chevron.style.transform = open ? 'rotate(180deg)' : '';
}

// ── Click en VIDEOS desde sidebar ──
function abrirVideos() {
  document.querySelectorAll('.nav-item').forEach(n => n.classList.remove('active'));
  event.currentTarget.classList.add('active');
  setPanel('subir');
}

// ── Cambio de panel (Subir / Editar / Borrar) ──
function setPanel(name) {
  const paneles = ['subir', 'editar', 'borrar'];
  const labels  = { subir: 'Videos / Subir video', editar: 'Videos / Editar video', borrar: 'Videos / Borrar video' };

  paneles.forEach(p => {
    document.getElementById('panel-' + p).classList.add('panel-hidden');
    const btn = document.getElementById('btn-' + p);
    if (btn) btn.classList.remove('active');
  });

  document.getElementById('panel-' + name).classList.remove('panel-hidden');
  const btnActivo = document.getElementById('btn-' + name);
  if (btnActivo) btnActivo.classList.add('active');

  document.getElementById('breadcrumb-current').textContent = labels[name] || name;
}

// ── SUBIR: guardar video ──
async function guardarVideo() {
  const titulo     = document.getElementById('sub-titulo').value.trim();
  const url        = document.getElementById('sub-url').value.trim();
  const descripcion = document.getElementById('sub-desc').value.trim();

  if (!titulo || !url) {
    alert('El título y la URL son obligatorios.');
    return;
  }

  try {
    const res = await fetch('/api/videos', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ titulo, url, descripcion })
    });

    if (!res.ok) {
      const msg = await res.text();
      alert('Error: ' + msg);
      return;
    }

    alert('Video guardado correctamente.');
    document.getElementById('sub-titulo').value = '';
    document.getElementById('sub-url').value    = '';
    document.getElementById('sub-desc').value   = '';

  } catch (e) {
    alert('No se pudo conectar al servidor.');
  }
}

// ── EDITAR: buscar video ──
async function buscarEditar() {
  const q = document.getElementById('edit-search').value.trim();
  if (!q) return;

  try {
    const res = await fetch('/api/videos/buscar?titulo=' + encodeURIComponent(q));

    if (!res.ok) {
      alert('No se encontró ningún video con ese nombre.');
      document.getElementById('edit-result').classList.add('panel-hidden');
      return;
    }

    const video = await res.json();

    document.getElementById('edit-result').dataset.id = video.id;
    document.getElementById('edit-res-title').textContent = video.titulo;
    document.getElementById('edit-res-meta').textContent  = video.activo ? 'Activo' : 'Inactivo';
    document.getElementById('edit-titulo').value = video.titulo;
    document.getElementById('edit-url').value    = video.url;
    document.getElementById('edit-desc').value   = video.descripcion;

    document.getElementById('edit-result').classList.remove('panel-hidden');

  } catch (e) {
    alert('No se pudo conectar al servidor.');
  }
}

async function guardarEdicion() {
  const id          = document.getElementById('edit-result').dataset.id;
  const titulo      = document.getElementById('edit-titulo').value.trim();
  const url         = document.getElementById('edit-url').value.trim();
  const descripcion = document.getElementById('edit-desc').value.trim();

  if (!titulo || !url) {
    alert('El título y la URL son obligatorios.');
    return;
  }

  try {
    const res = await fetch('/api/videos/' + id, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ titulo, url, descripcion })
    });

    if (!res.ok) {
      const msg = await res.text();
      alert('Error: ' + msg);
      return;
    }

    alert('Video actualizado correctamente.');
    cancelarEditar();

  } catch (e) {
    alert('No se pudo conectar al servidor.');
  }
}

async function inactivarVideo() {
  const id = document.getElementById('edit-result').dataset.id;

  try {
    const res = await fetch('/api/videos/' + id + '/inactivar', {
      method: 'PATCH'
    });

    if (!res.ok) {
      const msg = await res.text();
      alert('Error: ' + msg);
      return;
    }

    alert('Video inactivado correctamente.');
    document.getElementById('edit-res-meta').textContent = 'Inactivo';

  } catch (e) {
    alert('No se pudo conectar al servidor.');
  }
}

function cancelarEditar() {
  document.getElementById('edit-result').classList.add('panel-hidden');
  document.getElementById('edit-search').value = '';
}

// ── BORRAR: buscar video ──
async function buscarBorrar() {
  const q = document.getElementById('del-search').value.trim();
  if (!q) return;

  try {
    const res = await fetch('/api/videos/buscar?titulo=' + encodeURIComponent(q));

    if (!res.ok) {
      alert('No se encontró ningún video con ese nombre.');
      document.getElementById('del-result').classList.add('panel-hidden');
      return;
    }

    const video = await res.json();

    document.getElementById('del-result').dataset.id = video.id;
    document.getElementById('del-res-title').textContent = video.titulo;
    document.getElementById('del-res-meta').textContent  = video.activo ? 'Activo' : 'Inactivo';
    document.getElementById('del-titulo').textContent    = video.titulo;
    document.getElementById('del-url').textContent       = video.url;
    document.getElementById('del-desc').textContent      = video.descripcion;

    document.getElementById('del-result').classList.remove('panel-hidden');

  } catch (e) {
    alert('No se pudo conectar al servidor.');
  }
}

function cancelarBorrar() {
  document.getElementById('del-result').classList.add('panel-hidden');
  document.getElementById('del-search').value = '';
}

// ── CONFIRMACIÓN de borrado ──
function pedirConfirmacion() {
  document.getElementById('confirm-overlay').classList.remove('panel-hidden');
}

function cerrarConfirmacion() {
  document.getElementById('confirm-overlay').classList.add('panel-hidden');
}

async function confirmarBorrar() {
  const id = document.getElementById('del-result').dataset.id;

  try {
    const res = await fetch('/api/videos/' + id, {
      method: 'DELETE'
    });

    if (!res.ok) {
      const msg = await res.text();
      cerrarConfirmacion();
      alert('Error: ' + msg);
      return;
    }

    cerrarConfirmacion();
    cancelarBorrar();
    alert('Video eliminado correctamente.');

  } catch (e) {
    cerrarConfirmacion();
    alert('No se pudo conectar al servidor.');
  }
}