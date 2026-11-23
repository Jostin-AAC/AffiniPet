# Informe de Análisis de Refinamiento, Dependencias y Viabilidad — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE05 — Catálogo de Requerimientos y Análisis de Refinamiento  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Propósito del Informe

El presente informe documenta el examen crítico aplicado sobre el catálogo inicial de requerimientos de **AffiniPet** (`01_Catalogo_Requerimientos.md`) antes de pasar a su especificación formal definitiva. El análisis abarca la detección y corrección de defectos de redacción (ambigüedad, incompletitud, conflicto y alcance innecesario), el mapeo de dependencias funcionales y técnicas entre requerimientos, la evaluación de viabilidad del núcleo crítico del sistema en cuatro dimensiones y la consolidación de las versiones refinadas.

---

## 2. Problemáticas Detectadas y Corregidas

Durante la revisión técnica de los enunciados levantados en campo (`PPE04`), se identificaron cinco problemáticas relevantes que habrían generado errores de interpretación o fallos operativos durante la programación. En la Tabla 1 se detalla cada hallazgo, su justificación y la acción correctiva aplicada:

| ID | Problemática | Justificación | Corrección |
| :--- | :--- | :--- | :--- |
| **RNF-01** *(versión inicial)* | **Ambigüedad** | El enunciado inicial indicaba que *"el sistema deberá calcular rápidamente las mascotas compatibles y cargar los caniles sin demoras"*. Términos subjetivos como *"rápidamente"* o *"sin demoras"* impiden establecer un criterio objetivo de prueba. | Se reformuló definiendo un límite cuantificable: tiempo máximo de respuesta de **2 segundos** para el **$95\%$** de las solicitudes en un catálogo de hasta **500 mascotas**. |
| **RD-04** *(versión inicial)* | **Incompletitud** | La regla inicial establecía calcular la afinidad con vivienda, tiempo, presupuesto y familia, pero omitía los pesos exactos de cada variable, el comportamiento cuando el adoptante supera el tiempo o presupuesto requerido (pudiendo exceder el $100\%$) y la validación de requisitos $> 0$. | Se completó especificando la fórmula $C = 30V + 30T + 20P + 20F$, el redondeo a dos decimales y el acotamiento superior a $1$ mediante $\min(1, \text{disponible}/\text{requerido})$ con denominadores estrictamente positivos. |
| **RF-12** *(versión inicial)* | **Conflicto (Concurrencia)** | La redacción inicial permitía registrar el acta directamente al confirmar en pantalla sin revalidar el estado de la mascota. Si dos ventanillas atendían simultáneamente y seleccionaban al mismo animal *Disponible*, se producía un conflicto de doble asignación (`C-05`). | Se incorporó la obligación de revalidar en tiempo real que la mascota continúe en estado *Disponible* en el instante de confirmación y ejecutar el alta del acta y el cambio a *Adoptado* dentro de una única transacción atómica. |
| **RF-05** *(versión inicial)* | **Conflicto (Roles de acceso)** | Inicialmente se planteó que el Médico Veterinario ingresara directamente al software para actualizar vacunas y esterilizaciones, lo cual entraba en conflicto con la restricción de alcance de `PPE03` que limita las cuentas activas únicamente a Administrador y Encargado (`C-02`). | Se precisó que el sistema registra y valida la ficha clínica aportada por el Médico Veterinario mediante el circuito administrativo autorizado, manteniendo intacto el control de roles definido. |
| **RF-13** *(propuesta descartada)* | **Innecesario (Fuera de alcance)** | Durante la recolección inicial surgió la propuesta de incluir un módulo para cobrar cuotas de recuperación y emitir comprobantes financieros al firmar el acta. Esto contradice las exclusiones explícitas de `01_Vision_y_Alcance.md` y complejiza innecesariamente el trámite presencial. | Se descartó y eliminó este requerimiento del catálogo maestro, aclarando que la variable económica ($P$) se utiliza únicamente para evaluar la capacidad mensual de manutención declarada en USD. |

---

## 3. Dependencias Identificadas entre Requerimientos

Para planificar correctamente el orden de diseño, codificación y pruebas unitarias, se identificaron las siguientes relaciones de dependencia estructural y operativa dentro de **AffiniPet**:

1. **`RF-02` (Autenticación y control de roles) depende de `RF-01` (Gestión de cuentas):** El sistema no puede validar credenciales ni aplicar restricciones por rol en el inicio de sesión si previamente el Administrador del Refugio no ha creado y habilitado las cuentas en la base de datos local.
2. **`RF-03` (Verificación de sesión y catálogo activo) depende de `RF-02` (Autenticación) y `RNF-03` (Disponibilidad local):** La apertura de atención en ventanilla requiere una sesión autenticada válida y la disponibilidad operativa de la base de datos local del refugio.
3. **`RF-05` (Ficha clínica y etológica) depende de `RF-04` (Registro de mascotas y caniles):** No es posible registrar vacunas, esterilización ni rasgos conductuales si no existe previamente el registro maestro de la mascota con su identificador único y ubicación física asignada.
4. **`RF-06` (Consulta de mascotas elegibles) depende de `RF-04`, `RF-05`, `RD-01`, `RD-02` y `RD-03`:** Para listar una mascota como apta para evaluación, el sistema requiere que su estado sea *Disponible* (`RF-04`) y que cumpla simultáneamente con vacunación vigente (`RD-01`), esterilización validada (`RD-02`) y perfil etológico completo (`RD-03`) registrados en su ficha (`RF-05`).
5. **`RF-08` (Cálculo de compatibilidad) depende de `RF-06`, `RF-07` y `RD-04`:** El motor de cálculo necesita cruzar los parámetros de las mascotas elegibles (`RF-06`) contra los datos socioeconómicos validados del adoptante (`RF-07`), ejecutando la ecuación ponderada y acotada definida en `RD-04`.
6. **`RF-09` (Filtrado y ordenamiento por umbral) depende de `RF-08` y `RD-05`:** El filtrado de opciones solo puede ejecutarse una vez calculados los porcentajes de compatibilidad (`RF-08`), aplicando el corte inclusivo $C \ge 70\%$ y el bloqueo por incompatibilidad crítica familiar (`RD-05`).
7. **`RF-10` (Mapa interactivo de caniles) y `RF-11` (Detalle clínico y desistimiento) dependen de `RF-09`:** La habilitación visual de caniles y gateras en el mapa de ventanilla depende directamente de la lista filtrada de mascotas compatibles generada en `RF-09`.
8. **`RF-12` (Confirmación transaccional y emisión de acta) depende de `RF-10`, `RF-11`, `RD-06`, `RD-07`, `RD-08`, `RNF-07` y `RNF-08`:** La formalización de la entrega exige que el adoptante haya seleccionado una mascota habilitada sin desistir (`RF-10`, `RF-11`), que no posea bloqueos institucionales vigentes (`RD-06`), que la transacción sea atómica (`RNF-08`), que el acta quede inmutable y auditada (`RD-07`, `RD-08`) y que sea compatible con impresión en formato A4 (`RNF-07`).

---

## 4. Análisis de Viabilidad de Requerimientos Críticos

Se seleccionó como núcleo crítico de evaluación el conjunto conformado por el **Motor de Evaluación de Compatibilidad ($\ge 70\%$), el Mapa Interactivo de Caniles/Gateras y la Confirmación Transaccional de Adopción (`RF-08`, `RF-09`, `RF-10`, `RF-12` y `RNF-08`)**, debido a que concentran la lógica de negocio diferenciadora de **AffiniPet** y el mayor riesgo operativo en ventanilla.

### 4.1. Viabilidad Técnica
* **Evaluación:** **Alta (Totalmente viable).**
* **Sustento:** El algoritmo de compatibilidad ($C = 30V + 30T + 20P + 20F$) y las reglas de filtrado (`RD-01` a `RD-06`) consisten en operaciones aritméticas y lógicas deterministas implementables en **C# (.NET 8.0)** dentro de una capa de servicios desacoplada (`RNF-06`), garantizando tiempos de respuesta inferiores a 2 segundos (`RNF-01`) para catálogos de cientos de mascotas. Asimismo, la interfaz gráfica de caniles/gateras es resoluble mediante controles nativos de **Windows Forms** (paneles dinámicos o cuadrículas con estados habilitado/bloqueado), y la concurrencia entre ventanillas (`RNF-08`) se controla mediante transacciones ACID y bloqueo de fila en el motor relacional local.

### 4.2. Viabilidad Económica
* **Evaluación:** **Alta (Costo de licenciamiento nulo).**
* **Sustento:** El desarrollo se realiza utilizando herramientas gratuitas y oficiales: **Microsoft Visual Studio Community 2022**, el SDK de **.NET 8.0** y un motor de base de datos relacional local sin costo de licencia. Al operar localmente en el refugio sin servidores en la nube, portales web ni pasarelas de pago externas, no se generan gastos recurrentes de infraestructura ni adquisiciones de hardware adicional.

### 4.3. Viabilidad Temporal
* **Evaluación:** **Media-Alta (Viable dentro del plazo académico).**
* **Sustento:** La delimitación estricta del alcance realizada en `PPE03` (excluyendo aplicaciones móviles, multi-refugio y módulos contables) permite concentrar el tiempo disponible del semestre en los cuatro módulos esenciales. Mediante una priorización estructurada (MoSCoW), el equipo puede construir y probar primero el flujo crítico de ventanilla (`RF-04` a `RF-12`) junto con sus casos límite ($C = 70\%$, bloqueos y concurrencia) dentro del cronograma establecido.

### 4.4. Viabilidad Organizacional
* **Evaluación:** **Alta (Alineada con el proceso actual del refugio).**
* **Sustento:** El sistema no obliga a reestructurar los roles existentes del refugio ni exige que los ciudadanos adoptantes o el personal veterinario utilicen cuentas informáticas nuevas. Al conservar la atención presencial asistida por el **Encargado de Ventanilla** y el control administrativo del **Administrador del Refugio**, la curva de aprendizaje es mínima (30 minutos de capacitación según `RNF-04`) y reduce directamente la carga manual de revisar carpetas físicas.

---

## 5. Versión Refinada de los Requerimientos Afectados

En la Tabla 2 se presenta la comparación explícita entre la redacción inicial con defectos y la versión refinada aprobada para los requerimientos modificados durante el análisis:

| ID | Versión Inicial (Con Defecto) | Versión Refinada (Aprobada) |
| :---: | :--- | :--- |
| **RF-05** | El sistema deberá permitir que el veterinario ingrese a registrar las vacunas, esterilización y comportamiento de cada animal. | El sistema deberá registrar y validar la ficha clínica y etológica de cada mascota aportada por el Médico Veterinario (mediante el flujo administrativo autorizado), incluyendo estado de vacunación, esterilización, especie, tamaño, nivel de energía, gasto mensual estimado y tolerancia familiar. |
| **RF-12** | El sistema deberá registrar la adopción y generar el acta cuando el encargado presione el botón de confirmar. | El sistema deberá revalidar en tiempo real la disponibilidad de la mascota al confirmar la adopción, ejecutar de forma atómica el registro del Acta de Adopción y actualizar el estado de la mascota a *Adoptado*. |
| **RNF-01** | El sistema deberá calcular rápidamente las mascotas compatibles y mostrar los caniles sin demoras en pantalla. | El sistema deberá calcular la compatibilidad y desplegar el mapa de caniles en un tiempo máximo de 2 segundos para el $95\%$ de las solicitudes realizadas sobre un catálogo de hasta 500 mascotas. |
| **RD-04** | El sistema calculará la compatibilidad de la mascota tomando en cuenta la vivienda, el tiempo libre, el presupuesto y la familia del adoptante. | El cálculo de compatibilidad deberá aplicar la fórmula $C = 30V + 30T + 20P + 20F$, redondeando a dos decimales y acotando los factores $T$ y $P$ a un máximo de $1$ mediante $\min(1, \text{disponible}/\text{requerido})$ con requisitos $> 0$. |