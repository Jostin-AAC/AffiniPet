# Documento del Proceso de Adquisición de Requerimientos — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE04 — Adquisición de Requerimientos  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Plan de Adquisición de Requerimientos

El levantamiento de requerimientos para **AffiniPet** se ejecutó de manera sistemática con el objetivo de comprender el proceso presencial actual del refugio de animales, identificar los cuellos de botella en ventanilla y establecer las reglas sanitarias e institucionales necesarias para el nuevo sistema de escritorio.

### 1.1. Fuentes de requerimientos consultadas
La recolección de información abarcó cuatro categorías de fuentes vinculadas directamente a la operación del refugio:
1. **Personas (Interesados internos y externos):**
   * *Internos directos:* Administrador del Refugio y Encargado de Ventanilla.
   * *Internos indirectos:* Médico Veterinario / Personal Clínico, Encargado de Caniles y Directiva del Refugio.
   * *Externos indirectos:* Ciudadanos solicitantes de adopción (observados durante la atención en ventanilla) y lineamientos de la Entidad Municipal / Regulatoria.
2. **Documentos institucionales:** Formularios físicos actuales de solicitud de adopción, fichas clínicas y conductuales de ingreso, registros manuales de ocupación de caniles/gateras, actas de entrega vigentes y reglamento interno de bienestar animal.
3. **Sistemas existentes:** Registros digitales básicos y listas locales empleadas actualmente en el centro de cómputo del refugio para el control de inventario de animales.
4. **Procesos operativos del refugio:** Flujo completo de atención presencial desde la llegada del ciudadano a ventanilla hasta la evaluación, revisión de ficha clínica y entrega de la mascota.

### 1.2. Técnicas de elicitación utilizadas
* **Entrevistas semiestructuradas:** Realizadas al Administrador del Refugio, Encargado de Ventanilla y Médico Veterinario mediante guías de preguntas abiertas y cerradas para definir roles, permisos, parámetros médicos obligatorios y criterios del modelo de compatibilidad.
* **Observación directa en ventanilla:** Seguimiento presencial del trámite de adopción para detectar tiempos muertos, consultas repetitivas sobre disponibilidad de caniles y causas habituales de desistimiento o devolución.
* **Análisis de documentos:** Revisión de las fichas médicas, formularios de entrevista y actas históricas para identificar datos obligatorios, estados de las mascotas y vacíos de trazabilidad.

### 1.3. Cronograma de adquisición y responsables

| Fase | Actividad Principal | Técnicas y Fuentes | Periodo | Responsable |
| :--- | :--- | :--- | :--- | :--- |
| **Fase 1: Preparación** | Definición de stakeholders, revisión de formularios físicos actuales y elaboración de guías de entrevista. | Análisis de documentos y sistemas existentes. | Día 1 | Paul Olmos (Analista de Negocio) |
| **Fase 2: Levantamiento en campo** | Ejecución de entrevistas con Administrador, Veterinario y Encargado; observación del flujo presencial en ventanilla. | Entrevistas semiestructuradas y observación directa. | Días 2 – 3 | Paul Olmos / Galo Castillo |
| **Fase 3: Refinamiento y aclaración** | Tabulación de hallazgos, formulación de preguntas de aclaración y registro de conflictos detectados entre roles. | Mesas de revisión con Administración y Área Clínica. | Día 4 | Paul Olmos / Jostin Aldas |
| **Fase 4: Documentación y cierre** | Redacción formal de `01_Adquisicion_Requerimientos.md`, verificación de trazabilidad con `PPE03` e integración en GitHub. | Revisión técnica y control de versiones. | Día 5 | Paul Olmos / Jostin Aldas (Integrador) |

---

## 2. Tabla de Información Registrada

La siguiente tabla consolida los **15 registros fundamentales** obtenidos durante el proceso de elicitación, cubriendo todos los módulos operativos de **AffiniPet**:

| ID | Información obtenida | Fuente | Aclaración / Respuesta |
| :---: | :--- | :--- | :--- |
| **101** | El sistema operará únicamente en el centro de cómputo y ventanillas de un solo refugio, sin acceso ciudadano externo ni sucursales. | Entrevista: Directiva y Administrador del Refugio | Confirmado: se excluyen portales web públicos, aplicaciones móviles y gestión multi-refugio conforme al alcance definido en `PPE03`. |
| **102** | Solo el Administrador del Refugio puede crear, habilitar y revocar credenciales de acceso para los operadores de ventanilla. | Entrevista: Administrador del Refugio | Aclarado: el Encargado de Ventanilla no puede autogestionar cuentas ni modificar permisos administrativos. |
| **103** | El Médico Veterinario y el Encargado de Caniles aportan datos sanitarios y de ubicación, pero no operan directamente la aplicación en esta versión. | Entrevista: Administrador y Médico Veterinario | Aclarado: la transcripción de datos clínicos y asignación de caniles se canaliza mediante el flujo administrativo habilitado. |
| **104** | Antes de iniciar una atención en ventanilla, el sistema debe verificar que la sesión esté activa y el catálogo de mascotas vigente. | Observación directa en ventanilla | Definido como control preventivo inicial para evitar trámites con sesiones expiradas o inventarios cerrados. |
| **105** | Ninguna mascota puede ser evaluada ni adoptada si no cuenta con vacunación vigente registrada y validada en su ficha clínica. | Análisis de documentos: Ficha Clínica Veterinaria | Regla obligatoria: el sistema excluirá automáticamente del proceso de compatibilidad a toda mascota sin vacunación validada. |
| **106** | La esterilización realizada, registrada y validada es un requisito sanitario indispensable previo a la entrega en adopción. | Entrevista: Médico Veterinario / Reglamento interno | Confirmado: las mascotas pendientes de esterilización permanecen en estado *No disponible* para adopción. |
| **107** | Cada mascota debe contar con un perfil conductual registrado (especie, tamaño, nivel de energía, temperamento y tolerancia familiar). | Análisis de documentos: Fichas de comportamiento | Estos atributos constituyen los parámetros requeridos de la mascota para calcular los factores de compatibilidad. |
| **108** | El registro del adoptante debe capturar tipo de vivienda (*reducido*, *mediano*, *amplio*), minutos diarios disponibles, presupuesto mensual (USD) y composición familiar. | Análisis de documentos: Formulario de solicitud actual | Los campos obligatorios no pueden quedar vacíos y los valores numéricos (minutos, presupuesto, edades) no pueden ser negativos. |
| **109** | El presupuesto mensual en USD mide la capacidad económica específica para cubrir alimentación y cuidados veterinarios de la mascota. | Entrevista: Administrador del Refugio | Se divide el presupuesto disponible del adoptante entre el costo mensual estimado de la mascota, acotando el factor económico a un máximo de $1$. |
| **110** | La evaluación aplica la fórmula ponderada $C = 30V + 30T + 20P + 20F$, redondeada a dos decimales, exigiendo un umbral inclusivo $C \ge 70\%$. | Entrevista: Administrador y Médico Veterinario | Si existe una incompatibilidad familiar crítica (por ejemplo, agresión comprobada con niños u otras mascotas), se bloquea la opción sin importar el valor de $C$. |
| **111** | Las mascotas compatibles deben mostrarse ordenadas de mayor a menor puntaje $C$, desempatando por identificador de mascota ascendente. | Entrevista: Encargado de Ventanilla | Si ninguna mascota alcanza $C \ge 70\%$, el sistema mostrará el mensaje: *"No se encontraron mascotas compatibles con el perfil registrado"*. |
| **112** | El mapa de caniles y gateras solo debe habilitar la selección de espacios físicos que contengan mascotas compatibles y disponibles. | Observación directa en ventanilla | Los caniles o gateras vacíos, en cuarentena o con mascotas incompatibles ($C < 70\%$) se muestran bloqueados y no seleccionables. |
| **113** | Si el adoptante desiste tras revisar la ficha clínica o visitar el canil (desistimiento informado), no se emite acta ni se altera el estado de la mascota. | Observación directa en ventanilla | La mascota conserva intacto su estado *Disponible* para continuar siendo evaluada con otros solicitantes. |
| **114** | Al confirmar la adopción, el sistema debe revalidar en tiempo real que la mascota siga *Disponible* y ejecutar el alta del acta y el cambio a *Adoptado* en una sola transacción. | Análisis de registros existentes | Evita adopciones duplicadas cuando dos ventanillas atienden simultáneamente mediante atomicidad transaccional (todo o nada). |
| **115** | Las actas de adopción emitidas son inmutables y almacenan el usuario responsable, rol, fecha y hora exacta de confirmación. | Análisis de documentos: Actas históricas | No se permite editar ni eliminar actas formalizadas; toda corrección posterior requiere un asiento de rectificación vinculado al acta original. |

---

## 3. Preguntas de Aclaración y Respuestas Obtenidas

Durante las sesiones de revisión entre los analistas y los interesados del refugio, se formularon las siguientes preguntas para resolver ambigüedades detectadas en el levantamiento inicial:

1. **P-01 (Alcance del umbral del 70%):** *¿Alcanzar o superar el $70\%$ de compatibilidad ($C \ge 70\%$) implica que el sistema aprueba automáticamente la entrega de la mascota?*
   * **Respuesta obtenida:** No. El umbral $C \ge 70\%$ actúa como un filtro técnico de elegibilidad. La entrega final sigue sujeta a la aceptación informada del ciudadano tras revisar la ficha clínica y a la aprobación institucional explícita del refugio.
2. **P-02 (Historial de devoluciones previas):** *¿Un ciudadano que haya devuelto una mascota anteriormente queda bloqueado automáticamente en el sistema para futuras solicitudes?*
   * **Respuesta obtenida:** No de forma automática. Una devolución previa se considera un antecedente informativo, pero el bloqueo operativo en el sistema aplica únicamente cuando existe una restricción institucional vigente registrada por la administración (por ejemplo, incumplimiento grave o maltrato).
3. **P-03 (Acotación de los factores de tiempo $T$ y presupuesto $P$):** *¿Qué sucede si un adoptante dispone de más tiempo libre o presupuesto mensual del que exige la mascota? ¿Puede superar el $100\%$ de compatibilidad?*
   * **Respuesta obtenida:** No. Los factores $T$ y $P$ están acotados superiormente a $1$ mediante la relación $\min(1, \text{disponible} / \text{requerido})$, y los valores requeridos de cada mascota deben ser estrictamente mayores a cero para evitar divisiones por cero.
4. **P-04 (Coexistencia de condiciones familiares en el factor $F$):** *¿Cómo se evalúa el factor familiar $F$ cuando en un hogar coexisten distintas condiciones, como niños pequeños (requiere adaptación) y otra mascota incompatible?*
   * **Respuesta obtenida:** Se aplica el criterio más restrictivo (la condición menos favorable). Si existe una incompatibilidad crítica con otro animal o miembro del hogar, $F = 0$ y se bloquea la recomendación de esa mascota independientemente del puntaje global.
5. **P-05 (Corrección de errores en actas ya emitidas):** *Si después de confirmar la adopción se detecta un error de digitación en los datos del ciudadano, ¿puede el Encargado de Ventanilla modificar el acta guardada?*
   * **Respuesta obtenida:** No. Las actas confirmadas son inmutables por auditoría y respaldo legal. Cualquier rectificación debe registrarse como un documento complementario vinculado al identificador del acta original.
6. **P-06 (Gestión económica de tasas o donaciones):** *¿Debe el sistema registrar el pago de aportes económicos o emitir comprobantes contables al momento de formalizar el acta?*
   * **Respuesta obtenida:** No. Cualquier aporte voluntario o gestión administrativa externa se realiza fuera de **AffiniPet**, ya que el sistema no maneja cobros ni contabilidad.

---

## 4. Registro de Conflictos Detectados

En coherencia con las diferencias de intereses identificadas en `PPE03`, se documentan los conflictos detectados durante la fase de adquisición de requerimientos:

| ID | Conflicto detectado | Interesados involucrados | Descripción | Estado |
| :---: | :--- | :--- | :--- | :--- |
| **C-01** | **Calibración de pesos y umbral de compatibilidad vs. criterio clínico** | Administrador del Refugio / Médico Veterinario / Encargado de Ventanilla | El modelo inicial fija pesos estándar ($30V + 30T + 20P + 20F$) y corte en $70\%$. El Médico Veterinario solicita flexibilidad para ajustar parámetros en animales con condiciones médicas o conductuales especiales, mientras ventanilla requiere reglas fijas y rápidas. | **En negociación** |
| **C-02** | **Ausencia de cuentas propias para Veterinario y Caniles vs. actualización oportuna de fichas** | Médico Veterinario / Administrador del Refugio / Encargado de Ventanilla | Para mantener un control centralizado de accesos, solo el Administrador y el Encargado tienen cuentas en el sistema. Sin embargo, esto genera dependencia operativa cuando el veterinario actualiza vacunas o esterilizaciones y deben transcribirse a tiempo. | **En negociación** |
| **C-03** | **Validación de aprobación institucional en ventanilla vs. tiempos de espera** | Encargado de Ventanilla / Administrador del Refugio / Ciudadano Adoptante | La formalización requiere confirmar la aprobación institucional antes de emitir el acta, pero en horas pico el Administrador puede no estar físicamente junto a la ventanilla, lo que podría retrasar la entrega al ciudadano. | **Pendiente** |
| **C-04** | **Criterios formales para activar bloqueos institucionales de adoptantes** | Administrador del Refugio / Encargado de Ventanilla / Entidad Municipal | Dado que una devolución previa no bloquea automáticamente al solicitante, hace falta estandarizar las causales exactas que habilitan un bloqueo vigente para evitar criterios subjetivos en ventanilla. | **Pendiente** |
| **C-05** | **Rapidez de atención presencial vs. rigor de validaciones sanitarias y transaccionales** | Encargado de Ventanilla / Ciudadano Adoptante / Administrador del Refugio | Ventanilla prioriza formularios ágiles para reducir filas, mientras Administración y Clínica exigen múltiples validaciones previas (vacunas, esterilización, compatibilidad y concurrencia). Se resolvió automatizando los filtros en segundo plano. | **Resuelto** |