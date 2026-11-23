# Documento de Visión y Alcance — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE03 — Configuración del Proyecto, Visión y Matriz de Interesados  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Introducción

### 1.1. Propósito del documento
El presente documento define de manera formal la visión, los límites funcionales, las exclusiones explícitas y las restricciones operativas del sistema **AffiniPet**. Su objetivo es alinear las expectativas de los interesados del refugio de animales (Administrador del Refugio, Encargado de Ventanilla, Médico Veterinario y Directiva) con el equipo de desarrollo, sirviendo como línea base para la especificación de requerimientos y la construcción del software.

### 1.2. Propósito general del sistema
**AffiniPet** es una aplicación de escritorio basada en formularios, diseñada para operar exclusivamente en el centro de cómputo y en las estaciones de atención presencial (ventanillas) de un refugio de animales. El sistema tiene como propósito agilizar la atención en ventanilla, centralizar el estado sanitario de las mascotas y reducir la tasa de devoluciones causadas por la incompatibilidad entre el animal y el hogar del adoptante.

Actualmente, la asignación de mascotas en el refugio se realiza mediante fichas en papel y apreciaciones subjetivas durante la entrevista presencial, lo que genera demoras y adopciones fallidas. **AffiniPet** organiza la atención en etapas secuenciales de control e incorpora un motor de cálculo multicriterio que evalúa objetivamente la afinidad entre el solicitante y cada animal disponible. El porcentaje de compatibilidad funciona como una herramienta de apoyo técnico para la decisión; la aprobación final de cada entrega recae siempre en la autoridad institucional del refugio.

---

## 2. Alcance del sistema

El alcance delimita estrictamente las funcionalidades incluidas en la aplicación de escritorio y excluye aquellas características que quedan fuera de la frontera del proyecto.

### 2.1. Funcionalidades incluidas (Dentro del alcance)

1. **Control de acceso y gestión de sesiones:**
   * Inicio de sesión mediante usuario y contraseña individuales con control de permisos basado en roles (*Administrador del Refugio* y *Encargado de Ventanilla*).
   * Creación, habilitación y revocación de cuentas de ventanilla exclusivamente por parte del Administrador del Refugio.
   * Verificación de sesión activa y disponibilidad del catálogo institucional antes de iniciar cualquier trámite en ventanilla.

2. **Gestión de inventario de mascotas y ficha clínica:**
   * Registro y actualización de las mascotas alojadas en el refugio, controlando su ubicación física (canil o gatera) y su estado operativo (*Disponible*, *Adoptado* o *No disponible*).
   * Registro y consulta de parámetros clínicos y conductuales aportados por el Médico Veterinario (transcritos por el flujo administrativo autorizado), verificando que la mascota cuente con vacunación vigente, esterilización realizada y perfil etológico completo antes de ser elegible para adopción.

3. **Perfilamiento del adoptante y motor de compatibilidad:**
   * Registro presencial de los datos socioeconómicos y del hogar del solicitante, validando obligatoriedad de campos y valores numéricos no negativos: tipo de vivienda (*reducido*, *mediano*, *amplio*), minutos diarios disponibles para cuidado, presupuesto mensual destinado a la mascota (en USD) y composición familiar (presencia de menores y otras mascotas).
   * Verificación preventiva para excluir mascotas no disponibles y bloquear adoptantes que mantengan restricciones institucionales vigentes (sin considerar una devolución previa como bloqueo automático).
   * Cálculo ponderado de compatibilidad mediante la función $C = 30V + 30T + 20P + 20F$ (donde $V$ representa vivienda, $T$ tiempo, $P$ presupuesto y $F$ entorno familiar), redondeando el resultado a dos decimales y filtrando únicamente las mascotas con $C \ge 70\%$. Si existe una incompatibilidad familiar crítica, el sistema bloquea la recomendación sin importar el puntaje global.
   * Presentación de opciones aptas ordenadas de mayor a menor porcentaje de compatibilidad (desempatando por identificador de mascota ascendente), o despliegue del mensaje normalizado *"No se encontraron mascotas compatibles con el perfil registrado"* cuando ninguna supere el umbral.

4. **Exploración física de caniles/gateras y consulta clínica:**
   * Visualización del mapa de caniles y gateras habilitados para el perfil evaluado, impidiendo seleccionar espacios vacíos, en cuarentena o con mascotas incompatibles.
   * Consulta detallada de la ficha clínica y etológica durante la atención en ventanilla.
   * Registro de desistimiento informado cuando el ciudadano decida no continuar tras revisar las condiciones de la mascota, manteniendo intacto su estado *Disponible* sin generar acta.

5. **Formalización y emisión del Acta de Adopción:**
   * Revalidación transaccional de la disponibilidad de la mascota en el instante de confirmación para evitar adopciones duplicadas si dos ventanillas atienden en paralelo.
   * Ejecución atómica (todo o nada) del registro de adopción, emisión del Acta de Adopción formal y actualización automática del estado de la mascota a *Adoptado*.
   * Registro de trazabilidad con el usuario responsable, rol, fecha y hora exacta de emisión, garantizando la inmutabilidad del acta generada (cualquier corrección posterior exige un asiento de rectificación vinculado al acta original).

### 2.2. Exclusiones explícitas ("NO alcance")

Para evitar desviaciones de alcance durante el desarrollo, quedan formalmente excluidos de esta versión de **AffiniPet**:

* **Sin arquitectura multi-refugio:** El sistema administra únicamente la operación de un solo refugio; no incluye sincronización entre múltiples sedes ni administración por proveedores externos.
* **Sin portal web público ni postulación en línea:** No existirá página web pública para que los ciudadanos consulten mascotas o envíen solicitudes desde Internet.
* **Sin aplicación móvil:** No se desarrollarán aplicaciones para teléfonos inteligentes (Android o iOS).
* **Sin cuentas de usuario para personal clínico o actores externos:** Ni el Médico Veterinario, ni el Ciudadano Adoptante, ni la Entidad Reguladora tendrán credenciales de inicio de sesión directas en la aplicación.
* **Sin pasarela de pagos ni contabilidad:** El sistema no procesa cobros de tasas de adopción, donaciones ni facturación; el presupuesto declarado por el adoptante se utiliza únicamente como variable de cálculo en el motor de compatibilidad.

---

## 3. Restricciones

### 3.1. Restricciones técnicas y operativas
* **Plataforma tecnológica:** El sistema debe construirse como una aplicación de escritorio en lenguaje **C#** sobre **Windows Forms (.NET 8.0)**, ejecutándose de forma local en las computadoras del refugio y conectada a una base de datos relacional local que garantice integridad transaccional y control de concurrencia.
* **Calibración institucional de parámetros:** Los pesos de la fórmula de compatibilidad ($30, 30, 20, 20$) y el umbral mínimo ($70\%$) constituyen la línea base operativa y deberán ser validados con la Directiva del Refugio y el Médico Veterinario antes del despliegue final.
* **Inmutabilidad documental:** Las actas de adopción confirmadas no pueden editarse ni eliminarse físicamente de la base de datos.

### 3.2. Restricciones económicas
* **Presupuesto acotado:** Al tratarse de un refugio de animales con recursos limitados, la solución no debe requerir licencias comerciales costosas ni servicios en la nube de pago recurrente.
* **Uso de equipamiento existente:** La interfaz debe funcionar adecuadamente sobre las computadoras e impresoras locales ya disponibles en las ventanillas del refugio.

### 3.3. Restricciones de tiempo y planificación
* **Cronograma académico del proyecto:** El levantamiento de requisitos, modelado, implementación y pruebas de verificación deben ajustarse estrictamente a los hitos establecidos en la asignatura durante el semestre vigente, priorizando el flujo crítico de atención presencial en ventanilla.

---

## 4. Definiciones y abreviaturas

| Término / Sigla | Definición en el contexto de AffiniPet |
| :--- | :--- |
| **Acta de adopción** | Documento formal e inmutable emitido por el sistema al confirmarse una entrega, que vincula al adoptante, la mascota, el puntaje de compatibilidad y los datos de auditoría del encargado. |
| **Canil** | Espacio físico individual o compartido destinado al alojamiento de perros dentro del refugio. |
| **Compatibilidad ($C$)** | Indicador porcentual redondeado a dos decimales ($0\%$ a $100\%$) que mide la afinidad entre el hogar del adoptante y los requerimientos de una mascota mediante la fórmula $C = 30V + 30T + 20P + 20F$. |
| **Desistimiento informado** | Decisión libre del ciudadano de no concretar la adopción tras revisar las opciones compatibles, los caniles o la ficha clínica, sin emitir acta ni alterar el estado de la mascota. |
| **Ficha clínica** | Registro sanitario y conductual de cada animal que consolida su vacunación vigente, esterilización y características etológicas. |
| **Gatera** | Espacio físico acondicionado específicamente para el alojamiento de gatos dentro del refugio. |
| **Incompatibilidad crítica** | Condición familiar o conductual que representa un riesgo directo para la mascota o los habitantes del hogar, bloqueando automáticamente la recomendación sin importar el valor de $C$. |
| **RD** | **Requerimiento de Dominio.** Regla invariante propia del funcionamiento del refugio y bienestar animal que condiciona las validaciones del sistema. |
| **RF** | **Requerimiento Funcional.** Función, cálculo o servicio específico que el software debe ejecutar. |
| **RNF** | **Requerimiento No Funcional.** Atributo de calidad o restricción técnica (seguridad, rendimiento, usabilidad, confiabilidad) que acota el comportamiento del sistema. |
| **SRS** | **Especificación de Requerimientos de Software** (*Software Requirements Specification*). Documento maestro que consolida los requerimientos funcionales, no funcionales y de dominio del sistema. |
| **Umbral de compatibilidad** | Porcentaje mínimo inclusivo ($C \ge 70\%$) exigido para que una mascota disponible sea habilitada como opción compatible para el adoptante. |