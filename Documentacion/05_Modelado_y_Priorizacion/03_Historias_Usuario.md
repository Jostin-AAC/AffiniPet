# Historias de Usuario y Criterios de Aceptación — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE07 — Modelado de Casos de Uso, Historias de Usuario y Priorización  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Propósito y Trazabilidad con Requerimientos (`PPE06`)

El presente documento expresa las necesidades funcionales y operativas de los usuarios de **AffiniPet** mediante **8 Historias de Usuario (`HU-01` a `HU-08`)** redactadas bajo la estructura *"Como [rol], quiero [objetivo], para [beneficio]"*, acompañadas de escenarios verificables en formato *Dado que / Cuando / Entonces* (*Given / When / Then*) y enlazadas directamente con los requerimientos refinados aprobados en `PPE06`.

---

## 2. Catálogo de Historias de Usuario

### **HU-01 — Administración de cuentas de encargados de ventanilla**
* **Historia de Usuario:**  
  **Como** Administrador del Refugio, **quiero** crear, habilitar y revocar las cuentas de acceso de los encargados de ventanilla, **para** garantizar que únicamente personal interno autorizado opere los formularios de atención y formalización de adopciones.
* **Requerimiento asociado (`PPE06`):** `RF-01`, `RNF-02`
* **Criterios de Aceptación:**
  * **Criterio 1 (Creación de cuenta válida):**  
    * **Dado que** he iniciado sesión con el rol de *Administrador del Refugio*,  
    * **Cuando** registro un nuevo encargado ingresando cédula de 10 dígitos, nombres completos, usuario único y contraseña inicial,  
    * **Entonces** el sistema almacena la cuenta con clave cifrada en SHA-256 (`RNF-02`) y estado *Activo* en la base de datos local.
  * **Criterio 2 (Revocación de acceso):**  
    * **Dado que** existe una cuenta activa de *Encargado de Ventanilla*,  
    * **Cuando** cambio su estado a *Inactivo* desde el panel de administración,  
    * **Entonces** el sistema bloquea inmediatamente cualquier nuevo inicio de sesión con dichas credenciales.

---

### **HU-02 — Autenticación y restricción de módulos por rol**
* **Historia de Usuario:**  
  **Como** Encargado de Ventanilla, **quiero** iniciar sesión con mis credenciales individuales y acceder directamente a los formularios de atención presencial, **para** atender a los adoptantes sin riesgo de modificar configuraciones administrativas o del inventario maestro.
* **Requerimiento asociado (`PPE06`):** `RF-02`, `RF-03`, `RNF-02`
* **Criterios de Aceptación:**
  * **Criterio 1 (Ingreso exitoso y control de permisos):**  
    * **Dado que** cuento con una cuenta activa con rol *Encargado de Ventanilla* y el catálogo institucional está habilitado (`RF-03`),  
    * **Cuando** ingreso mi usuario y contraseña correctos en la pantalla de inicio de sesión,  
    * **Entonces** el sistema abre el menú de atención presencial y mantiene bloqueados los módulos de gestión de usuarios e inventario maestro (`RF-02`).
  * **Criterio 2 (Bloqueo por intentos fallidos):**  
    * **Dado que** un usuario intenta autenticarse en la estación de ventanilla,  
    * **Cuando** ingresa una contraseña errónea 3 veces consecutivas,  
    * **Entonces** el sistema bloquea temporalmente la cuenta durante 15 minutos (`RNF-02`).

---

### **HU-03 — Control de inventario de mascotas y asignación de caniles o gateras**
* **Historia de Usuario:**  
  **Como** Administrador del Refugio, **quiero** registrar y actualizar los datos maestros de cada mascota asignándole un canil o gatera disponible, **para** mantener actualizado el inventario físico del refugio y su disponibilidad real en ventanilla.
* **Requerimiento asociado (`PPE06`):** `RF-04`, `RNF-05`
* **Criterios de Aceptación:**
  * **Criterio 1 (Alta de mascota en espacio libre):**  
    * **Dado que** existe un canil o gatera disponible en el refugio,  
    * **Cuando** registro los datos maestros de la mascota y le asigno dicho espacio con estado *Disponible*,  
    * **Entonces** la mascota queda guardada con identificador único y ocupa ese espacio físico en el catálogo.
  * **Criterio 2 (Exclusión de animales no disponibles):**  
    * **Dado que** una mascota se encuentra en estado *Adoptado* o *No disponible*,  
    * **Cuando** se realiza una consulta de catálogo en ventanilla,  
    * **Entonces** el sistema excluye automáticamente al animal de las opciones elegibles para adopción.

---

### **HU-04 — Registro y validación de ficha clínica y etológica**
* **Historia de Usuario:**  
  **Como** Médico Veterinario (a través del flujo administrativo habilitado), **quiero** que se registre y valide el estado de vacunación, esterilización y perfil etológico de cada mascota, **para** asegurar que únicamente animales sanitariamente aptos y con requerimientos definidos participen en las adopciones.
* **Requerimiento asociado (`PPE06`):** `RF-05`, `RF-06`, `RD-01`, `RD-02`, `RD-03`
* **Criterios de Aceptación:**
  * **Criterio 1 (Habilitación sanitaria completa):**  
    * **Dado que** una mascota registrada cuenta con vacunación vigente (`RD-01`), esterilización validada (`RD-02`) y perfil conductual completo (`RD-03`),  
    * **Cuando** se guarda la ficha clínica validada,  
    * **Entonces** la mascota aparece como elegible en las consultas de ventanilla (`RF-06`).
  * **Criterio 2 (Bloqueo por ficha incompleta):**  
    * **Dado que** una mascota tiene vacunación vencida, esterilización pendiente o atributos etológicos sin completar,  
    * **Cuando** se ejecuta el motor de compatibilidad,  
    * **Entonces** el sistema excluye automáticamente a la mascota del cálculo y del listado de opciones.

---

### **HU-05 — Registro y validación del perfil socioeconómico del adoptante**
* **Historia de Usuario:**  
  **Como** Encargado de Ventanilla, **quiero** registrar los datos de vivienda, tiempo diario disponible, presupuesto mensual en USD y composición familiar del ciudadano adoptante, **para** capturar información completa y libre de errores antes de evaluar su compatibilidad.
* **Requerimiento asociado (`PPE06`):** `RF-07`, `RD-06`, `RNF-04`
* **Criterios de Aceptación:**
  * **Criterio 1 (Guardado de perfil válido):**  
    * **Dado que** atiendo presencialmente a un solicitante sin restricciones institucionales vigentes (`RD-06`),  
    * **Cuando** completo todos los campos obligatorios con valores numéricos no negativos (minutos diarios $\ge 0$ y presupuesto mensual $\ge 0$),  
    * **Entonces** el sistema almacena el perfil del adoptante y habilita la evaluación de compatibilidad.
  * **Criterio 2 (Validación de campos inválidos o bloqueo activo):**  
    * **Dado que** se intenta guardar el formulario de entrevista,  
    * **Cuando** existe un campo obligatorio vacío, un valor numérico negativo o una restricción institucional vigente sobre la cédula ingresada,  
    * **Entonces** el sistema impide continuar, muestra una alerta descriptiva y no bloquea automáticamente a ciudadanos que solo tengan una devolución previa sin sanción vigente (`RD-06`).

---

### **HU-06 — Cálculo ponderado de compatibilidad y filtrado por umbral del 70 %**
* **Historia de Usuario:**  
  **Como** Encargado de Ventanilla, **quiero** calcular automáticamente el porcentaje de compatibilidad entre el adoptante y las mascotas disponibles aplicando el umbral mínimo del $70\%$, **para** presentar recomendaciones objetivas ordenadas por nivel de afinidad.
* **Requerimiento asociado (`PPE06`):** `RF-08`, `RF-09`, `RD-04`, `RD-05`, `RNF-01`
* **Criterios de Aceptación:**
  * **Criterio 1 (Cálculo ponderado y ordenamiento descendente):**  
    * **Dado que** existe un perfil válido de adoptante y mascotas elegibles en estado *Disponible*,  
    * **Cuando** presiono el botón *Evaluar Compatibilidad*,  
    * **Entonces** el sistema calcula $C = 30V + 30T + 20P + 20F$ redondeado a dos decimales (`RD-04`), retiene únicamente las mascotas con $C \ge 70\%$ sin incompatibilidad crítica familiar (`RD-05`) y las ordena de mayor a menor puntaje en $\le 2\text{ segundos}$ (`RNF-01`).
  * **Criterio 2 (Sin coincidencias o riesgo crítico familiar):**  
    * **Dado que** una mascota presenta incompatibilidad crítica con menores o animales del hogar, o ninguna mascota alcanza $C \ge 70\%$,  
    * **Cuando** finaliza el cálculo,  
    * **Entonces** el sistema excluye la mascota incompatible y, si no quedan opciones aptas, muestra exactamente el mensaje: *"No se encontraron mascotas compatibles con el perfil registrado"*.

---

### **HU-07 — Exploración de caniles compatibles, ficha clínica y desistimiento informado**
* **Historia de Usuario:**  
  **Como** Encargado de Ventanilla, **quiero** visualizar en el mapa interactivo únicamente los caniles y gateras con mascotas compatibles, consultar su ficha clínica y registrar un eventual desistimiento, **para** guiar la elección informada del adoptante sin alterar el inventario si decide no adoptar.
* **Requerimiento asociado (`PPE06`):** `RF-10`, `RF-11`, `RNF-04`
* **Criterios de Aceptación:**
  * **Criterio 1 (Habilitación selectiva en el mapa):**  
    * **Dado que** existen mascotas con compatibilidad $C \ge 70\%$,  
    * **Cuando** se despliega el mapa interactivo de caniles y gateras,  
    * **Entonces** solo los espacios con mascotas compatibles están habilitados para selección y despliegue de ficha clínica (`RF-10`), mientras los caniles vacíos o incompatibles permanecen bloqueados.
  * **Criterio 2 (Desistimiento informado sin emisión de acta):**  
    * **Dado que** el ciudadano revisa la ficha clínica o visita el canil de una mascota compatible,  
    * **Cuando** decide no continuar y registro su desistimiento informado (`RF-11`),  
    * **Entonces** el sistema cierra el trámite sin crear ningún acta y mantiene el estado *Disponible* de la mascota.

---

### **HU-08 — Confirmación transaccional y emisión del Acta de Adopción**
* **Historia de Usuario:**  
  **Como** Encargado de Ventanilla, **quiero** confirmar la entrega de la mascota seleccionada verificando su disponibilidad en tiempo real, **para** emitir un Acta de Adopción inmutable, auditada y lista para impresión sin duplicar asignaciones.
* **Requerimiento asociado (`PPE06`):** `RF-12`, `RD-07`, `RD-08`, `RNF-07`, `RNF-08`
* **Criterios de Aceptación:**
  * **Criterio 1 (Confirmación atómica y trazabilidad):**  
    * **Dado que** el adoptante acepta la mascota compatible y cuenta con aprobación institucional,  
    * **Cuando** confirmo la adopción y la mascota sigue *Disponible*,  
    * **Entonces** en una única transacción ACID (`RNF-08`) se registra el Acta de Adopción inmutable (`RD-07`) con usuario, rol, fecha y hora (`RD-08`), se actualiza la mascota a *Adoptado* y se genera la vista de impresión A4 (`RNF-07`).
  * **Criterio 2 (Protección ante concurrencia simultánea):**  
    * **Dado que** otra ventanilla confirma la misma mascota milisegundos antes,  
    * **Cuando** intento confirmar la adopción en mi estación,  
    * **Entonces** el sistema revierte la transacción sin generar registros duplicados y notifica que la mascota ya no se encuentra disponible.