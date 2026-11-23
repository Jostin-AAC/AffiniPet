# Especificación Formal de Requerimientos Refinados y Verificables — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE06 — Requerimientos Refinados y Verificables  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Lineamientos de Especificación Aplicados

El presente documento recoge la especificación formal y definitiva de los **28 requerimientos aprobados** de **AffiniPet** (`12 RF`, `8 RNF` y `8 RD`), conservando intacta la codificación establecida en `PPE05`. Cada ficha ha sido depurada bajo cinco reglas estrictas de calidad:

1. **Verbo preciso en futuro:** Todos los enunciados del sistema inician con la fórmula normativa *"El sistema deberá..."* seguida de un verbo accionable (*permitir, verificar, registrar, calcular, filtrar, desplegar, revalidar, almacenar, mantener, estructurar, ejecutar, garantizar*).
2. **Atomicidad:** Cada enunciado expresa una única capacidad o restricción verificable por oración.
3. **Ausencia de ambigüedad:** Se han eliminado términos subjetivos (*rápido, amigable, óptimo, adecuado*), sustituyéndolos por umbrales cuantificables ($C \ge 70\%$, $\le 2\text{ segundos}$, $3\text{ intentos}$, $5\text{ pasos}$).
4. **Verificabilidad:** Cada ficha incorpora un criterio de aceptación objetivo que permite diseñar casos de prueba directos.
5. **Trazabilidad:** Cada requerimiento mantiene vinculación explícita con su fuente de elicitación (`PPE04`), su prioridad MoSCoW (`PPE07`) y sus dependencias internas.

---

## 2. Fichas de Requerimientos Funcionales (`RF-01` a `RF-12`)

### **RF-01 — Gestión de cuentas de encargados de ventanilla**
* **Identificador:** RF-01
* **Nombre:** Gestión de cuentas de encargados de ventanilla
* **Enunciado:** El sistema deberá permitir al Administrador del Refugio crear, habilitar y revocar cuentas de usuario para los encargados de ventanilla ingresando cédula de 10 dígitos, nombres completos, nombre de usuario único y contraseña inicial.
* **Fuente:** Administrador del Refugio (`PPE04 - Reg. 102`).
* **Prioridad:** Must
* **Criterio de aceptación:** La cuenta creada o actualizada queda almacenada en la base de datos local con el estado asignado (*Activo* o *Inactivo*) y se refleja inmediatamente en la tabla de usuarios administrados.
* **Dependencias:** Ninguna.

---

### **RF-02 — Autenticación y control de acceso por rol**
* **Identificador:** RF-02
* **Nombre:** Autenticación y control de acceso por rol
* **Enunciado:** El sistema deberá verificar las credenciales ingresadas en el formulario de inicio de sesión y bloquear el acceso a los módulos de administración de cuentas e inventario maestro cuando el usuario autenticado posea el rol de Encargado de Ventanilla.
* **Fuente:** Administrador del Refugio (`PPE04 - Reg. 102, 104`).
* **Prioridad:** Must
* **Criterio de aceptación:** Al iniciar sesión con credenciales válidas de Encargado de Ventanilla, el sistema despliega únicamente las opciones de atención de adopciones y mantiene deshabilitados los controles administrativos.
* **Dependencias:** RF-01, RNF-02.

---

### **RF-03 — Verificación previa de sesión y catálogo activo**
* **Identificador:** RF-03
* **Nombre:** Verificación previa de sesión y catálogo activo
* **Enunciado:** El sistema deberá verificar que la sesión del operador esté vigente, la ventanilla habilitada y el catálogo institucional activo antes de permitir iniciar un trámite de atención presencial.
* **Fuente:** Observación directa en ventanilla (`PPE04 - Reg. 104`).
* **Prioridad:** Should
* **Criterio de aceptación:** Si la sesión ha expirado o el catálogo se encuentra inactivo, el sistema impide abrir el formulario de entrevista y muestra una notificación de estado en pantalla.
* **Dependencias:** RF-02, RNF-03.

---

### **RF-04 — Registro de mascotas y asignación de canil o gatera**
* **Identificador:** RF-04
* **Nombre:** Registro de mascotas y asignación de canil o gatera
* **Enunciado:** El sistema deberá registrar y actualizar los datos maestros de cada mascota del refugio vinculándola a un canil o gatera disponible y controlando su estado operativo (*Disponible*, *Adoptado* o *No disponible*).
* **Fuente:** Administrador del Refugio y Encargado de Caniles (`PPE04 - Reg. 103, 112`).
* **Prioridad:** Must
* **Criterio de aceptación:** La mascota queda almacenada con un identificador único, asociada a un espacio físico libre y visible en el inventario con su estado operativo correspondiente.
* **Dependencias:** RF-02.

---

### **RF-05 — Registro y validación de ficha clínica y etológica**
* **Identificador:** RF-05
* **Nombre:** Registro y validación de ficha clínica y etológica
* **Enunciado:** El sistema deberá registrar y validar la ficha clínica y conductual aportada por el Médico Veterinario para cada mascota, almacenando estado de vacunación, esterilización, especie, tamaño, nivel de energía, gasto mensual estimado en USD y tolerancia familiar.
* **Fuente:** Médico Veterinario / Ficha Clínica (`PPE04 - Reg. 103, 105, 106, 107`).
* **Prioridad:** Must
* **Criterio de aceptación:** La ficha clínica queda vinculada al identificador de la mascota y el sistema marca el registro como completo únicamente cuando todos los campos sanitarios y etológicos obligatorios están diligenciados.
* **Dependencias:** RF-04, RD-01, RD-02, RD-03.

---

### **RF-06 — Consulta de mascotas elegibles para adopción**
* **Identificador:** RF-06
* **Nombre:** Consulta de mascotas elegibles para adopción
* **Enunciado:** El sistema deberá permitir consultar el catálogo de mascotas elegibles mostrando exclusivamente aquellas cuyo estado operativo sea *Disponible* y que cuenten con su ficha clínica y sanitaria validada.
* **Fuente:** Encargado de Ventanilla / Ficha Clínica (`PPE04 - Reg. 105, 106`).
* **Prioridad:** Must
* **Criterio de aceptación:** El listado desplegado excluye automáticamente cualquier animal en estado *Adoptado*, *No disponible* o con vacunación o esterilización sin validar.
* **Dependencias:** RF-04, RF-05, RD-01, RD-02, RD-03.

---

### **RF-07 — Registro del perfil socioeconómico del adoptante**
* **Identificador:** RF-07
* **Nombre:** Registro del perfil socioeconómico del adoptante
* **Enunciado:** El sistema deberá registrar el perfil presencial del ciudadano adoptante validando obligatoriedad de campos y valores numéricos no negativos para cédula, tipo de vivienda (*reducido*, *mediano*, *amplio*), minutos diarios disponibles, presupuesto mensual en USD y composición familiar.
* **Fuente:** Formulario de solicitud / Observación en ventanilla (`PPE04 - Reg. 108, 109`).
* **Prioridad:** Must
* **Criterio de aceptación:** El sistema rechaza cualquier intento de guardado con campos obligatorios vacíos o valores numéricos negativos y almacena correctamente los perfiles válidos.
* **Dependencias:** RF-03, RD-06.

---

### **RF-08 — Cálculo ponderado de compatibilidad**
* **Identificador:** RF-08
* **Nombre:** Cálculo ponderado de compatibilidad
* **Enunciado:** El sistema deberá calcular el porcentaje de compatibilidad ($0\%$ a $100\%$) entre el perfil del adoptante registrado y cada mascota disponible elegible evaluando de forma ponderada vivienda, tiempo diario, presupuesto mensual y entorno familiar.
* **Fuente:** Administrador del Refugio y Médico Veterinario (`PPE04 - Reg. 109, 110`).
* **Prioridad:** Must
* **Criterio de aceptación:** Para cada mascota elegible, el sistema genera un puntaje numérico redondeado a dos decimales coherente con la ecuación ponderada establecida.
* **Dependencias:** RF-06, RF-07, RD-04.

---

### **RF-09 — Filtrado y ordenamiento por umbral de compatibilidad**
* **Identificador:** RF-09
* **Nombre:** Filtrado y ordenamiento por umbral de compatibilidad
* **Enunciado:** El sistema deberá filtrar los resultados de compatibilidad conservando únicamente las mascotas con puntaje $C \ge 70\%$ ordenadas de mayor a menor afinidad (desempatando por ID ascendente), o desplegar el mensaje *"No se encontraron mascotas compatibles con el perfil registrado"* si ninguna alcanza el umbral.
* **Fuente:** Encargado de Ventanilla / Directiva (`PPE04 - Reg. 110, 111`).
* **Prioridad:** Must
* **Criterio de aceptación:** Ninguna mascota con puntaje inferior al $70\%$ aparece en la lista de opciones recomendadas y el ordenamiento descendente se cumple en el $100\%$ de las evaluaciones.
* **Dependencias:** RF-08, RD-05, RNF-01.

---

### **RF-10 — Despliegue de mapa interactivo de caniles y gateras**
* **Identificador:** RF-10
* **Nombre:** Despliegue de mapa interactivo de caniles y gateras
* **Enunciado:** El sistema deberá desplegar un mapa interactivo de caniles y gateras habilitando para selección únicamente los espacios físicos que alojen mascotas compatibles ($C \ge 70\%$) y bloqueando visualmente los espacios vacíos, en cuarentena o con mascotas no aptas.
* **Fuente:** Encargado de Caniles / Observación en ventanilla (`PPE04 - Reg. 112`).
* **Prioridad:** Must
* **Criterio de aceptación:** Al hacer clic sobre un canil bloqueado no se ejecuta ninguna acción de selección, mientras que los caniles habilitados responden mostrando el acceso a la ficha de la mascota.
* **Dependencias:** RF-04, RF-09, RNF-01.

---

### **RF-11 — Consulta de detalle clínico y registro de desistimiento informado**
* **Identificador:** RF-11
* **Nombre:** Consulta de detalle clínico y registro de desistimiento informado
* **Enunciado:** El sistema deberá permitir visualizar el detalle clínico y etológico completo de una mascota seleccionada en el mapa y registrar el desistimiento informado del adoptante manteniendo el estado *Disponible* de la mascota sin emitir acta.
* **Fuente:** Observación en ventanilla / Ciudadano Adoptante (`PPE04 - Reg. 113`).
* **Prioridad:** Should
* **Criterio de aceptación:** Tras registrar un desistimiento, no se crea ningún registro en la tabla de actas y la mascota continúa apareciendo como *Disponible* para otras evaluaciones.
* **Dependencias:** RF-10.

---

### **RF-12 — Confirmación transaccional y emisión del Acta de Adopción**
* **Identificador:** RF-12
* **Nombre:** Confirmación transaccional y emisión del Acta de Adopción
* **Enunciado:** El sistema deberá revalidar en tiempo real que la mascota elegida permanezca en estado *Disponible* al confirmar la adopción, ejecutar de forma atómica el registro del Acta de Adopción y cambiar el estado de la mascota a *Adoptado*.
* **Fuente:** Administrador del Refugio y Actas históricas (`PPE04 - Reg. 114, 115`).
* **Prioridad:** Must
* **Criterio de aceptación:** Al confirmar la adopción, el acta queda almacenada con los datos completos del adoptante, mascota y encargado, y la mascota pasa inmediatamente a estado *Adoptado* impidiendo una segunda asignación simultánea.
* **Dependencias:** RF-10, RF-11, RD-06, RD-07, RD-08, RNF-07, RNF-08.

---

## 3. Fichas de Requerimientos No Funcionales (`RNF-01` a `RNF-08`)

### **RNF-01 — Tiempo de respuesta en cálculo y carga de mapa**
* **Identificador:** RNF-01
* **Nombre:** Tiempo de respuesta en cálculo y carga de mapa
* **Enunciado:** El sistema deberá ejecutar el cálculo de compatibilidad y desplegar el mapa interactivo de caniles en un tiempo máximo de 2 segundos para el $95\%$ de las solicitudes realizadas sobre un catálogo de hasta 500 mascotas.
* **Fuente:** Observación en ventanilla (`PPE04 - Reg. 104, 111`).
* **Prioridad:** Must
* **Criterio de aceptación:** En pruebas de carga con 500 registros de mascotas locales, al menos 19 de cada 20 ejecuciones completan el renderizado en un tiempo $\le 2{,}0\text{ segundos}$.
* **Dependencias:** RF-08, RF-09, RF-10.

---

### **RNF-02 — Cifrado de credenciales y bloqueo por intentos fallidos**
* **Identificador:** RNF-02
* **Nombre:** Cifrado de credenciales y bloqueo por intentos fallidos
* **Enunciado:** El sistema deberá almacenar todas las contraseñas de usuario cifradas mediante algoritmo hash SHA-256 y bloquear temporalmente la cuenta durante 15 minutos tras registrar 3 intentos fallidos consecutivos de inicio de sesión.
* **Fuente:** Administrador del Refugio (`PPE04 - Reg. 102`).
* **Prioridad:** Must
* **Criterio de aceptación:** Ninguna contraseña es legible en texto plano en la base de datos y al tercer intento erróneo consecutivo el sistema impide el acceso durante 15 minutos.
* **Dependencias:** RF-01, RF-02.

---

### **RNF-03 — Disponibilidad operativa local sin conexión externa**
* **Identificador:** RNF-03
* **Nombre:** Disponibilidad operativa local sin conexión externa
* **Enunciado:** El sistema deberá mantener una disponibilidad operativa mínima del $99,5\%$ durante la jornada presencial del refugio (08h00 a 17h00), operando sobre una base de datos relacional local sin requerir conexión a Internet.
* **Fuente:** Directiva del Refugio (`PPE04 - Reg. 101`).
* **Prioridad:** Must
* **Criterio de aceptación:** Todas las funcionalidades del sistema operan de manera íntegra al desconectar el acceso a Internet de la estación de trabajo.
* **Dependencias:** Ninguna.

---

### **RNF-04 — Eficiencia operativa y facilidad de aprendizaje en ventanilla**
* **Identificador:** RNF-04
* **Nombre:** Eficiencia operativa y facilidad de aprendizaje en ventanilla
* **Enunciado:** El sistema deberá permitir completar el flujo presencial desde el registro del adoptante hasta la emisión del Acta de Adopción en un máximo de 5 pasos de pantalla, registrando una tasa máxima de 1 error de operación por hora tras 30 minutos de inducción.
* **Fuente:** Encargado de Ventanilla (`PPE04 - Reg. 108, 111`).
* **Prioridad:** Should
* **Criterio de aceptación:** Un operador capacitado durante 30 minutos completa un trámite de adopción de principio a fin navegando por 5 o menos vistas consecutivas.
* **Dependencias:** RF-07, RF-09, RF-10, RF-12.

---

### **RNF-05 — Capacidad de almacenamiento y escalabilidad local**
* **Identificador:** RNF-05
* **Nombre:** Capacidad de almacenamiento y escalabilidad local
* **Enunciado:** El sistema deberá soportar el almacenamiento y consulta de hasta 5.000 fichas de mascotas y 10.000 actas históricas manteniendo una degradación en el tiempo de respuesta inferior al $10\%$.
* **Fuente:** Administrador del Refugio (`PPE04 - Reg. 101, 115`).
* **Prioridad:** Could
* **Criterio de aceptación:** Con 5.000 mascotas y 10.000 actas cargadas en la base de datos local, los tiempos de consulta no superan en más del $10\%$ los tiempos base registrados con 500 registros.
* **Dependencias:** RNF-01, RNF-03.

---

### **RNF-06 — Arquitectura por capas y mantenibilidad**
* **Identificador:** RNF-06
* **Nombre:** Arquitectura por capas y mantenibilidad
* **Enunciado:** El sistema deberá estructurarse bajo una arquitectura de separación por capas (Presentación, Lógica de Negocio y Acceso a Datos) en C# sobre .NET 8.0, permitiendo modificar los pesos de compatibilidad sin alterar los formularios de interfaz gráfica.
* **Fuente:** Equipo de Desarrollo / Restricciones (`PPE03 - Sec. 3.1`).
* **Prioridad:** Should
* **Criterio de aceptación:** La modificación de los coeficientes del motor de compatibilidad se realiza exclusivamente en la capa de lógica de negocio sin recompilar ni editar archivos de diseño de formularios.
* **Dependencias:** RF-08.

---

### **RNF-07 — Compatibilidad de entorno de escritorio e impresión**
* **Identificador:** RNF-07
* **Nombre:** Compatibilidad de entorno de escritorio e impresión
* **Enunciado:** El sistema deberá ejecutarse de forma nativa en estaciones de trabajo con Windows 10 o Windows 11 (64 bits, resolución mínima de $1366 \times 768\text{ px}$) y generar el Acta de Adopción formateada para impresión estándar en hoja A4.
* **Fuente:** Centro de Cómputo del Refugio (`PPE04 - Reg. 101`).
* **Prioridad:** Must
* **Criterio de aceptación:** La aplicación se despliega sin recortes visuales en pantallas de $1366 \times 768\text{ px}$ y genera un documento de acta legible y ajustado a márgenes A4.
* **Dependencias:** RF-12.

---

### **RNF-08 — Integridad transaccional ACID ante concurrencia**
* **Identificador:** RNF-08
* **Nombre:** Integridad transaccional ACID ante concurrencia
* **Enunciado:** El sistema deberá garantizar atomicidad e integridad transaccional ACID durante la confirmación simultánea de adopciones desde múltiples ventanillas, asegurando $0$ actas duplicadas o estados inconsistentes ante fallos inesperados.
* **Fuente:** Análisis de registros existentes (`PPE04 - Reg. 114`).
* **Prioridad:** Must
* **Criterio de aceptación:** Ante dos solicitudes concurrentes sobre la misma mascota, únicamente la primera confirma la adopción y la segunda recibe un aviso de no disponibilidad sin alterar los datos.
* **Dependencias:** RF-12.

---

## 4. Fichas de Requerimientos de Dominio (`RD-01` a `RD-08`)

### **RD-01 — Obligatoriedad de vacunación vigente**
* **Identificador:** RD-01
* **Nombre:** Obligatoriedad de vacunación vigente
* **Enunciado:** El sistema deberá impedir que cualquier mascota sea incluida en evaluaciones de compatibilidad o procesos de adopción mientras no posea su esquema de vacunación vigente registrado y validado en su ficha clínica.
* **Fuente:** Médico Veterinario / Ficha Clínica (`PPE04 - Reg. 105`).
* **Prioridad:** Must
* **Criterio de aceptación:** Toda mascota con estado de vacunación pendiente o vencido queda excluida automáticamente de los resultados de `RF-06` y `RF-08`.
* **Dependencias:** RF-05, RF-06.

---

### **RD-02 — Obligatoriedad de esterilización validada**
* **Identificador:** RD-02
* **Nombre:** Obligatoriedad de esterilización validada
* **Enunciado:** El sistema deberá exigir que toda mascota elegible para adopción cuente con su procedimiento de esterilización realizado, registrado y validado previamente por el área clínica.
* **Fuente:** Médico Veterinario / Reglamento interno (`PPE04 - Reg. 106`).
* **Prioridad:** Must
* **Criterio de aceptación:** El sistema bloquea la transición a elegible para adopción de cualquier animal cuya esterilización no esté confirmada en su ficha.
* **Dependencias:** RF-05, RF-06.

---

### **RD-03 — Completitud del perfil etológico de la mascota**
* **Identificador:** RD-03
* **Nombre:** Completitud del perfil etológico de la mascota
* **Enunciado:** El sistema deberá exigir el registro completo del perfil conductual de cada mascota (especie, tamaño, nivel de energía, temperamento y tolerancia familiar) antes de habilitarla para el cálculo de compatibilidad.
* **Fuente:** Médico Veterinario / Fichas de conducta (`PPE04 - Reg. 107`).
* **Prioridad:** Must
* **Criterio de aceptación:** Una mascota con atributos etológicos incompletos no participa en el cálculo ponderado del motor de compatibilidad.
* **Dependencias:** RF-05, RF-06.

---

### **RD-04 — Fórmula ponderada y acotación de factores de compatibilidad**
* **Identificador:** RD-04
* **Nombre:** Fórmula ponderada y acotación de factores de compatibilidad
* **Enunciado:** El sistema deberá calcular el índice de compatibilidad mediante la expresión $C = 30V + 30T + 20P + 20F$ redondeada a dos decimales, acotando superiormente a $1$ los factores $T$ y $P$ mediante $\min(1, \text{disponible}/\text{requerido})$ con valores requeridos estrictamente mayores a cero.
* **Fuente:** Directiva y Médico Veterinario (`PPE04 - Reg. 109, 110, P-03`).
* **Prioridad:** Must
* **Criterio de aceptación:** El resultado calculado nunca supera el $100,00\%$, no genera errores de división por cero y coincide exactamente con el cálculo manual de la fórmula ponderada.
* **Dependencias:** RF-08.

---

### **RD-05 — Umbral mínimo inclusivo y bloqueo por riesgo crítico familiar**
* **Identificador:** RD-05
* **Nombre:** Umbral mínimo inclusivo y bloqueo por riesgo crítico familiar
* **Enunciado:** El sistema deberá considerar compatible únicamente a la mascota que obtenga un puntaje $C \ge 70\%$ y no presente incompatibilidades críticas con la composición familiar del adoptante, bloqueando su recomendación ante cualquier riesgo crítico sin importar el puntaje global.
* **Fuente:** Directiva y Médico Veterinario (`PPE04 - Reg. 110, P-04`).
* **Prioridad:** Must
* **Criterio de aceptación:** Una mascota con $C = 85\%$ pero con incompatibilidad crítica frente a niños pequeños o mascotas existentes del hogar es descartada automáticamente de los resultados aptos.
* **Dependencias:** RF-08, RF-09.

---

### **RD-06 — Restricciones institucionales vigentes sobre adoptantes**
* **Identificador:** RD-06
* **Nombre:** Restricciones institucionales vigentes sobre adoptantes
* **Enunciado:** El sistema deberá bloquear el inicio o confirmación del trámite de adopción únicamente cuando el ciudadano mantenga una restricción institucional vigente declarada por el refugio, sin considerar una devolución previa como causa automática de bloqueo.
* **Fuente:** Directiva del Refugio (`PPE04 - P-02, C-04`).
* **Prioridad:** Should
* **Criterio de aceptación:** Un solicitante con historial de devolución sin sanción vigente puede completar su evaluación normalmente, mientras que un solicitante con restricción activa es bloqueado con aviso en pantalla.
* **Dependencias:** RF-07, RF-12.

---

### **RD-07 — Inmutabilidad documental de las actas emitidas**
* **Identificador:** RD-07
* **Nombre:** Inmutabilidad documental de las actas emitidas
* **Enunciado:** El sistema deberá impedir la edición o eliminación física de las actas de adopción formalizadas en la base de datos, exigiendo la creación de un asiento de rectificación vinculado al identificador del acta original ante cualquier corrección posterior.
* **Fuente:** Actas históricas / Directiva (`PPE04 - Reg. 115, P-05`).
* **Prioridad:** Must
* **Criterio de aceptación:** Ningún rol de usuario dispone de opciones de borrado o sobrescritura sobre registros de actas confirmadas y toda rectificación conserva intacto el registro original.
* **Dependencias:** RF-12.

---

### **RD-08 — Trazabilidad de auditoría en actas de adopción**
* **Identificador:** RD-08
* **Nombre:** Trazabilidad de auditoría en actas de adopción
* **Enunciado:** El sistema deberá registrar de forma automática en cada Acta de Adopción emitida el identificador y nombre del usuario responsable en sesión, su rol operativo, y la fecha y hora exacta de confirmación.
* **Fuente:** Administrador y Entidad Regulatoria (`PPE04 - Reg. 115`).
* **Prioridad:** Must
* **Criterio de aceptación:** El $100\%$ de las actas almacenadas contienen metadatos completos de autoría y marca temporal verificables en consulta.
* **Dependencias:** RF-02, RF-12.