async function ingresar() {
    const Nit = document.getElementById("Nit").value;
    const Usuario = document.getElementById("Usuario").value;
    const Contraseña = document.getElementById("Contraseña").value;
    const error = document.getElementById("error");

    if (!Nit || !Usuario || !Contraseña) {
        error.style.display = "block";
        error.textContent = "Completa todos los campos por favor";
        return;
    }

    try {
        const respuesta = await fetch('/api/asesoras/login', { 
            method: 'POST',                                    
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                NIT: Nit,
                Usuario: Usuario,
                Contraseña: Contraseña
            })
        });

        if (respuesta.ok) {
            const data = await respuesta.json();
            if (data.success) {
                window.location.href = "Dashboard.html";
            }
        } else {
            const data = await respuesta.json();  
            error.style.display = "block";
            error.textContent = data.message || "Credenciales incorrectas";
        }

    } catch (e) {
        error.style.display = "block";
        error.textContent = "No se pudo conectar al servidor.";
    }
}