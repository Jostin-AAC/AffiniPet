# ESPECIFICACIÓN DE REQUERIMIENTOS DE SOFTWARE (SRS) — SISTEMA AFFINIPET

**Versión:** 1.0 (Línea Base Consolidada — Cierre de Ingeniería de Requerimientos)  
**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Docente:** Ing. José Naranjo  
**Institución:** Escuela Politécnica Nacional (EPN) — Facultad de Ingeniería de Sistemas  
**Autores:** Jostin Aldas (Líder Técnico), Paul Olmos (Analista de Negocio), Galo Castillo (Modelado y QA)  
**Fecha:** 22 de noviembre de 2026  

---

## Tabla de Contenido

1. [Introducción](#1-introducción)  
2. [Descripción General](#2-descripción-general)  
3. [Requerimientos Funcionales (Versión Refinada)](#3-requerimientos-funcionales-versión-refinada)  
4. [Requerimientos No Funcionales (Versión Refinada)](#4-requerimientos-no-funcionales-versión-refinada)  
5. [Requerimientos de Dominio](#5-requerimientos-de-dominio)  
6. [Modelo de Casos de Uso](#6-modelo-de-casos-de-uso)  
7. [Historias de Usuario y Criterios de Aceptación](#7-historias-de-usuario-y-criterios-de-aceptación)  
8. [Matriz de Priorización MoSCoW y Acuerdo de Negociación](#8-matriz-de-priorización-moscow-y-acuerdo-de-negociación)  
9. [Matriz de Trazabilidad](#9-matriz-de-trazabilidad)  
10. [Resultados de Validación](#10-resultados-de-validación)  
11. [Control de Versiones](#11-control-de-versiones)  

---

## 1. Introducción

### 1.1. Propósito del documento y del sistema
El presente documento constituye la **Especificación de Requerimientos de Software (SRS)** consolidada para **AffiniPet**, definiendo de manera formal, completa y verificable las capacidades funcionales, atributos de calidad, reglas de dominio, modelos de interacción y criterios de validación del sistema. Sirve como línea base contractual y técnica entre los interesados del refugio de animales y el equipo de desarrollo para iniciar la fase de diseño arquitectónico y construcción en C# sobre Windows Forms (.NET 8.0).

**AffiniPet** es una aplicación de escritorio diseñada para operar en el centro de cómputo y las estaciones de atención presencial (ventanillas) de un refugio de animales. Su propósito es digitalizar el inventario de mascotas y fichas clínicas, capturar el perfil socioeconómico del ciudadano durante la entrevista y calcular de forma objetiva el porcentaje de compatibilidad ($C \ge 70\%$) entre el hogar solicitante y cada mascota disponible, reduciendo demoras y devoluciones por incompatibilidad.

### 1.2. Alcance del sistema

#### Funcionalidades incluidas (Dentro del alcance)
* **Control de acceso y roles:** Autenticación individual con contraseñas cifradas en SHA-256 y separación de permisos entre *Administrador del Refugio* y *Encargado de Ventanilla*.
* **Inventario de mascotas y ficha clínica:** Registro de mascotas en caniles o gateras, control de estados (*Disponible*, *Adoptado*, *No disponible*) y validación obligatoria de vacunación vigente, esterilización y perfil etológico completo.
* **Perfilamiento del adoptante y motor de compatibilidad:** Captura presencial validada de vivienda, minutos diarios disponibles, presupuesto mensual en USD y composición familiar; verificación de bloqueos institucionales vigentes y cálculo ponderado mediante $C = 30V + 30T + 20P + 20F$ con umbral inclusivo $C \ge 70\%$ y bloqueo automático por incompatibilidad crítica familiar.
* **Exploración de caniles y consulta clínica:** Visualización en mapa interactivo habilitando únicamente espacios con mascotas compatibles ($C \ge 70\%$), consulta detallada de ficha clínica y registro de desistimiento informado sin alterar el inventario.
* **Formalización transaccional y emisión de acta:** Revalidación de disponibilidad en tiempo real ante concurrencia entre ventanillas, transición atómica a *Adoptado* y emisión de un Acta de Adopción inmutable y auditada en formato A4.

#### Exclusiones explícitas ("NO alcance")
* Sin arquitectura multi-refugio ni sincronización entre sucursales externas.
* Sin portal web público para consultas o postulaciones ciudadanas en línea.
* Sin aplicación móvil (Android o iOS).
* Sin cuentas de usuario directas para Médico Veterinario, Ciudadano Adoptante o Entidad Reguladora.
* Sin pasarela de pagos, cobro de tasas de adopción ni gestión contable.

### 1.3. Definiciones y abreviaturas

| Término / Sigla | Definición |
| :--- | :--- |
| **Acta de adopción** | Documento formal e inmutable generado al confirmar una entrega, que vincula adoptante, mascota, puntaje de compatibilidad y metadatos de auditoría. |
| **Canil / Gatera** | Espacios físicos individuales o compartidos destinados al alojamiento de perros (canil) o gatos (gatera) dentro del refugio. |
| **Compatibilidad ($C$)** | Indicador porcentual redondeado a dos decimales ($0\%$–$100\%$) calculado mediante $C = 30V + 30T + 20P + 20F$. |
| **Desistimiento informado** | Decisión libre del adoptante de no continuar tras revisar las opciones compatibles o la ficha clínica, conservando intacto el estado *Disponible* de la mascota. |
| **Ficha clínica** | Registro sanitario y etológico que consolida vacunación vigente, esterilización y rasgos conductuales de cada mascota. |
| **Incompatibilidad crítica** | Condición familiar o conductual que representa un riesgo grave para la mascota o los habitantes del hogar, bloqueando la recomendación sin importar el puntaje $C$. |
| **RF / RNF / RD** | Requerimiento Funcional / Requerimiento No Funcional / Requerimiento de Dominio. |
| **SRS** | Especificación de Requerimientos de Software (*Software Requirements Specification*). |

---

## 2. Descripción General

### 2.1. Contexto del negocio
En el flujo operativo tradicional del refugio, la asignación de mascotas se apoya en fichas físicas en papel y valoraciones subjetivas en ventanilla, provocando tiempos elevados de consulta, desconocimiento de requerimientos clínicos y devoluciones posteriores de animales. **AffiniPet** sistematiza la atención presencial en estaciones locales mediante un proceso guiado en seis etapas de control, donde el porcentaje de compatibilidad orienta técnicamente la elección antes de la aprobación institucional y firma del acta.

### 2.2. Interesados (Stakeholders) y resolución de conflictos

| Interesado | Clasificación | Interés | Influencia | Necesidad principal |
| :--- | :--- | :---: | :---: | :--- |
| **Administrador del Refugio** | Interno / Directo | Alto | Alta | Gestionar cuentas de encargados, controlar inventario de mascotas/caniles y supervisar la trazabilidad de actas. |
| **Encargado de Ventanilla** | Interno / Directo | Alto | Media | Registrar adoptantes, evaluar compatibilidad, explorar caniles habilitados y emitir actas de forma ágil. |
| **Médico Veterinario** | Interno / Indirecto | Alto | Media | Asegurar que ninguna mascota sea adoptada sin vacunación vigente, esterilización validada y perfil etológico completo. |
| **Ciudadano Adoptante** | Externo / Indirecto | Alto | Baja | Conocer mascotas acordes a su vivienda, tiempo y presupuesto, revisar su estado clínico y formalizar la adopción. |
| **Entidad Municipal / Regulatoria** | Externo / Indirecto | Medio | Alta | Contar con evidencia documental trazable e inmutable sobre las adopciones responsables gestionadas por el refugio. |

**Acuerdo de resolución de conflictos:** Mediante mesas de trabajo con los interesados, se priorizó el cumplimiento sanitario y el corte de compatibilidad ($C \ge 70\%$) por encima de la rapidez operativa, automatizando en segundo plano los filtros clínicos y el cálculo ponderado para mantener agilidad en ventanilla. Asimismo, se estableció un circuito administrativo validado para transcribir los datos aportados por el Médico Veterinario sin requerir cuentas clínicas separadas en esta versión.

### 2.3. Restricciones
* **Tecnológicas:** Aplicación de escritorio en **C#** sobre **Windows Forms (.NET 8.0)** ejecutándose en estaciones con Windows 10/11 ($1366 \times 768\text{ px}$ mínimo) y base de datos relacional local con soporte transaccional ACID.
* **Económicas:** Presupuesto institucional acotado; cero costos de licenciamiento comercial o infraestructura en la nube, operando sobre el equipamiento e impresoras A4 existentes.
* **Temporales y normativas:** Alcance acotado al cronograma académico del semestre; inmutabilidad estricta de actas formalizadas y calibración institucional de los coeficientes de compatibilidad antes del despliegue definitivo.

---

## 3. Requerimientos Funcionales (Versión Refinada)

* **RF-01 — Gestión de cuentas de encargados de ventanilla:**  
  * **Enunciado:** El sistema deberá permitir al Administrador del Refugio crear, habilitar y revocar cuentas de usuario para los encargados de ventanilla ingresando cédula de 10 dígitos, nombres completos, nombre de usuario único y contraseña inicial.  
  * **Fuente:** Administrador del Refugio (`Reg. 102`) | **Prioridad:** Must | **Dependencias:** Ninguna.  
  * **Criterio de aceptación:** La cuenta creada o actualizada queda almacenada en la base de datos local con el estado asignado (*Activo* o *Inactivo*) y se refleja inmediatamente en la tabla de usuarios.

* **RF-02 — Autenticación y control de acceso por rol:**  
  * **Enunciado:** El sistema deberá verificar las credenciales ingresadas en el formulario de inicio de sesión y bloquear el acceso a los módulos de administración de cuentas e inventario maestro cuando el usuario autenticado posea el rol de Encargado de Ventanilla.  
  * **Fuente:** Administrador del Refugio (`Reg. 102, 104`) | **Prioridad:** Must | **Dependencias:** `RF-01`, `RNF-02`.  
  * **Criterio de aceptación:** Al iniciar sesión con credenciales válidas de Encargado de Ventanilla, el sistema despliega únicamente las opciones de atención de adopciones y mantiene deshabilitados los controles administrativos.

* **RF-03 — Verificación previa de sesión y catálogo activo:**  
  * **Enunciado:** El sistema deberá verificar que la sesión del operador esté vigente, la ventanilla habilitada y el catálogo institucional activo antes de permitir iniciar un trámite de atención presencial.  
  * **Fuente:** Observación directa en ventanilla (`Reg. 104`) | **Prioridad:** Should | **Dependencias:** `RF-02`, `RNF-03`.  
  * **Criterio de aceptación:** Si la sesión ha expirado o el catálogo se encuentra inactivo, el sistema impide abrir el formulario de entrevista y muestra una notificación de estado en pantalla.

* **RF-04 — Registro de mascotas y asignación de canil o gatera:**  
  * **Enunciado:** El sistema deberá registrar y actualizar los datos maestros de cada mascota del refugio vinculándola a un canil o gatera disponible y controlando su estado operativo (*Disponible*, *Adoptado* o *No disponible*).  
  * **Fuente:** Administrador del Refugio y Encargado de Caniles (`Reg. 103, 112`) | **Prioridad:** Must | **Dependencias:** `RF-02`.  
  * **Criterio de aceptación:** La mascota queda almacenada con identificador único, asociada a un espacio físico libre y visible en el inventario con su estado operativo correspondiente.

* **RF-05 — Registro y validación de ficha clínica y etológica:**  
  * **Enunciado:** El sistema deberá registrar y validar la ficha clínica y conductual aportada por el Médico Veterinario para cada mascota, almacenando estado de vacunación, esterilización, especie, tamaño, nivel de energía, gasto mensual estimado en USD y tolerancia familiar.  
  * **Fuente:** Médico Veterinario / Ficha Clínica (`Reg. 103, 105, 106, 107`) | **Prioridad:** Must | **Dependencias:** `RF-04`, `RD-01`, `RD-02`, `RD-03`.  
  * **Criterio de aceptación:** La ficha clínica queda vinculada al identificador de la mascota y el sistema marca el registro como completo únicamente cuando todos los campos sanitarios y etológicos obligatorios están diligenciados.

* **RF-06 — Consulta de mascotas elegibles para adopción:**  
  * **Enunciado:** El sistema deberá permitir consultar el catálogo de mascotas elegibles mostrando exclusivamente aquellas cuyo estado operativo sea *Disponible* y que cuenten con su ficha clínica y sanitaria validada.  
  * **Fuente:** Encargado de Ventanilla / Ficha Clínica (`Reg. 105, 106`) | **Prioridad:** Must | **Dependencias:** `RF-04`, `RF-05`, `RD-01`, `RD-02`, `RD-03`.  
  * **Criterio de aceptación:** El listado desplegado excluye automáticamente cualquier animal en estado *Adoptado*, *No disponible* o con vacunación o esterilización sin validar.

* **RF-07 — Registro del perfil socioeconómico del adoptante:**  
  * **Enunciado:** El sistema deberá registrar el perfil presencial del ciudadano adoptante validando obligatoriedad de campos y valores numéricos no negativos para cédula, tipo de vivienda (*reducido*, *mediano*, *amplio*), minutos diarios disponibles, presupuesto mensual en USD y composición familiar.  
  * **Fuente:** Formulario de solicitud / Observación (`Reg. 108, 109`) | **Prioridad:** Must | **Dependencias:** `RF-03`, `RD-06`.  
  * **Criterio de aceptación:** El sistema rechaza cualquier intento de guardado con campos obligatorios vacíos o valores numéricos negativos y almacena correctamente los perfiles válidos.

* **RF-08 — Cálculo ponderado de compatibilidad:**  
  * **Enunciado:** El sistema deberá calcular el porcentaje de compatibilidad ($0\%$ a $100\%$) entre el perfil del adoptante registrado y cada mascota disponible elegible evaluando de forma ponderada vivienda, tiempo diario, presupuesto mensual y entorno familiar.  
  * **Fuente:** Administrador y Médico Veterinario (`Reg. 109, 110`) | **Prioridad:** Must | **Dependencias:** `RF-06`, `RF-07`, `RD-04`.  
  * **Criterio de aceptación:** Para cada mascota elegible, el sistema genera un puntaje numérico redondeado a dos decimales coherente con la ecuación ponderada establecida.

* **RF-09 — Filtrado y ordenamiento por umbral de compatibilidad:**  
  * **Enunciado:** El sistema deberá filtrar los resultados de compatibilidad conservando únicamente las mascotas con puntaje $C \ge 70\%$ ordenadas de mayor a menor afinidad (desempatando por ID ascendente), o desplegar el mensaje *"No se encontraron mascotas compatibles con el perfil registrado"* si ninguna alcanza el umbral.  
  * **Fuente:** Encargado de Ventanilla / Directiva (`Reg. 110, 111`) | **Prioridad:** Must | **Dependencias:** `RF-08`, `RD-05`, `RNF-01`.  
  * **Criterio de aceptación:** Ninguna mascota con puntaje inferior al $70\%$ aparece en la lista de opciones recomendadas y el ordenamiento descendente se cumple en el $100\%$ de las evaluaciones.

* **RF-10 — Despliegue de mapa interactivo de caniles y gateras:**  
  * **Enunciado:** El sistema deberá desplegar un mapa interactivo de caniles y gateras habilitando para selección únicamente los espacios físicos que alojen mascotas compatibles ($C \ge 70\%$) y bloqueando visualmente los espacios vacíos, en cuarentena o con mascotas no aptas.  
  * **Fuente:** Encargado de Caniles / Observación (`Reg. 112`) | **Prioridad:** Must | **Dependencias:** `RF-04`, `RF-09`, `RNF-01`.  
  * **Criterio de aceptación:** Al hacer clic sobre un canil bloqueado no se ejecuta ninguna acción de selección, mientras que los caniles habilitados responden mostrando el acceso a la ficha de la mascota.

* **RF-11 — Consulta de detalle clínico y registro de desistimiento informado:**  
  * **Enunciado:** El sistema deberá permitir visualizar el detalle clínico y etológico completo de una mascota seleccionada en el mapa y registrar el desistimiento informado del adoptante manteniendo el estado *Disponible* de la mascota sin emitir acta.  
  * **Fuente:** Observación en ventanilla / Adoptante (`Reg. 113`) | **Prioridad:** Should | **Dependencias:** `RF-10`.  
  * **Criterio de aceptación:** Tras registrar un desistimiento, no se crea ningún registro en la tabla de actas y la mascota continúa apareciendo como *Disponible* para otras evaluaciones.

* **RF-12 — Confirmación transaccional y emisión del Acta de Adopción:**  
  * **Enunciado:** El sistema deberá revalidar en tiempo real que la mascota elegida permanezca en estado *Disponible* al confirmar la adopción, ejecutar de forma atómica el registro del Acta de Adopción y cambiar el estado de la mascota a *Adoptado*.  
  * **Fuente:** Administrador y Actas históricas (`Reg. 114, 115`) | **Prioridad:** Must | **Dependencias:** `RF-10`, `RF-11`, `RD-06`, `RD-07`, `RD-08`, `RNF-07`, `RNF-08`.  
  * **Criterio de aceptación:** Al confirmar la adopción, el acta queda almacenada con los datos completos del adoptante, mascota y encargado, y la mascota pasa inmediatamente a estado *Adoptado* impidiendo una segunda asignación simultánea.

---

## 4. Requerimientos No Funcionales (Versión Refinada)

* **RNF-01 — Tiempo de respuesta en cálculo y carga de mapa:** El sistema deberá ejecutar el cálculo de compatibilidad y desplegar el mapa interactivo de caniles en un tiempo máximo de 2 segundos para el $95\%$ de las solicitudes realizadas sobre un catálogo de hasta 500 mascotas. *(Prioridad: Must | Criterio: $\le 2{,}0\text{ s}$ en al menos 19 de 20 ejecuciones).*
* **RNF-02 — Cifrado de credenciales y bloqueo por intentos fallidos:** El sistema deberá almacenar todas las contraseñas de usuario cifradas mediante algoritmo hash SHA-256 y bloquear temporalmente la cuenta durante 15 minutos tras registrar 3 intentos fallidos consecutivos de inicio de sesión. *(Prioridad: Must | Criterio: Hash SHA-256 en BD y bloqueo efectivo al 3.er fallo).*
* **RNF-03 — Disponibilidad operativa local sin conexión externa:** El sistema deberá mantener una disponibilidad operativa mínima del $99,5\%$ durante la jornada presencial del refugio (08h00 a 17h00), operando sobre una base de datos relacional local sin requerir conexión a Internet. *(Prioridad: Must | Criterio: Operación íntegra en modo desconectado).*
* **RNF-04 — Eficiencia operativa y facilidad de aprendizaje en ventanilla:** El sistema deberá permitir completar el flujo presencial desde el registro del adoptante hasta la emisión del Acta de Adopción en un máximo de 5 pasos de pantalla, registrando una tasa máxima de 1 error de operación por hora tras 30 minutos de inducción. *(Prioridad: Should | Criterio: Flujo completo en $\le 5$ vistas consecutivas).*
* **RNF-05 — Capacidad de almacenamiento y escalabilidad local:** El sistema deberá soportar el almacenamiento y consulta de hasta 5.000 fichas de mascotas y 10.000 actas históricas manteniendo una degradación en el tiempo de respuesta inferior al $10\%$. *(Prioridad: Could | Criterio: Variación de latencia $< 10\%$ bajo carga histórica).*
* **RNF-06 — Arquitectura por capas y mantenibilidad:** El sistema deberá estructurarse bajo una arquitectura de separación por capas (Presentación, Lógica de Negocio y Acceso a Datos) en C# sobre .NET 8.0, permitiendo modificar los pesos de compatibilidad sin alterar los formularios de interfaz gráfica. *(Prioridad: Should | Criterio: Recalibración de pesos sin editar formularios).*
* **RNF-07 — Compatibilidad de entorno de escritorio e impresión:** El sistema deberá ejecutarse de forma nativa en estaciones de trabajo con Windows 10 o Windows 11 (64 bits, resolución mínima de $1366 \times 768\text{ px}$) y generar el Acta de Adopción formateada para impresión estándar en hoja A4. *(Prioridad: Must | Criterio: Renderizado sin recortes en $1366 \times 768\text{ px}$ y salida A4).*
* **RNF-08 — Integridad transaccional ACID ante concurrencia:** El sistema deberá garantizar atomicidad e integridad transaccional ACID durante la confirmación simultánea de adopciones desde múltiples ventanillas, asegurando $0$ actas duplicadas o estados inconsistentes ante fallos inesperados. *(Prioridad: Must | Criterio: Confirmación exclusiva del primer trámite y reversión del segundo).*

---

## 5. Requerimientos de Dominio

* **RD-01 — Obligatoriedad de vacunación vigente:** El sistema deberá impedir que cualquier mascota sea incluida en evaluaciones de compatibilidad o procesos de adopción mientras no posea su esquema de vacunación vigente registrado y validado en su ficha clínica. *(Prioridad: Must).*
* **RD-02 — Obligatoriedad de esterilización validada:** El sistema deberá exigir que toda mascota elegible para adopción cuente con su procedimiento de esterilización realizado, registrado y validado previamente por el área clínica. *(Prioridad: Must).*
* **RD-03 — Completitud del perfil etológico de la mascota:** El sistema deberá exigir el registro completo del perfil conductual de cada mascota (especie, tamaño, nivel de energía, temperamento y tolerancia familiar) antes de habilitarla para el cálculo de compatibilidad. *(Prioridad: Must).*
* **RD-04 — Fórmula ponderada y acotación de factores de compatibilidad:** El sistema deberá calcular el índice de compatibilidad mediante la expresión $C = 30V + 30T + 20P + 20F$ redondeada a dos decimales, acotando superiormente a $1$ los factores $T$ y $P$ mediante $\min(1, \text{disponible}/\text{requerido})$ con valores requeridos estrictamente mayores a cero. *(Prioridad: Must).*
* **RD-05 — Umbral mínimo inclusivo y bloqueo por riesgo crítico familiar:** El sistema deberá considerar compatible únicamente a la mascota que obtenga un puntaje $C \ge 70\%$ y no presente incompatibilidades críticas con la composición familiar del adoptante, bloqueando su recomendación ante cualquier riesgo crítico sin importar el puntaje global. *(Prioridad: Must).*
* **RD-06 — Restricciones institucionales vigentes sobre adoptantes:** El sistema deberá bloquear el inicio o confirmación del trámite de adopción únicamente cuando el ciudadano mantenga una restricción institucional vigente declarada por el refugio, sin considerar una devolución previa como causa automática de bloqueo. *(Prioridad: Should).*
* **RD-07 — Inmutabilidad documental de las actas emitidas:** El sistema deberá impedir la edición o eliminación física de las actas de adopción formalizadas en la base de datos, exigiendo la creación de un asiento de rectificación vinculado al identificador del acta original ante cualquier corrección posterior. *(Prioridad: Must).*
* **RD-08 — Trazabilidad de auditoría en actas de adopción:** El sistema deberá registrar de forma automática en cada Acta de Adopción emitida el identificador y nombre del usuario responsable en sesión, su rol operativo, y la fecha y hora exacta de confirmación. *(Prioridad: Must).*

---

## 6. Modelo de Casos de Uso

### 6.1. Diagrama UML de Casos de Uso
El diagrama visual en notación UML se encuentra integrado en el repositorio bajo la ruta `/Documentacion/05_Modelado_y_Priorizacion/02_Diagrama_Casos_Uso.png`, delimitando la frontera del sistema **AffiniPet**, los actores externos y las relaciones `<<include>>` y `<<extend>>`.

### 6.2. Actores y Listado de Casos de Uso
* **Actores identificados:** *Administrador del Refugio* (Persona - directo), *Encargado de Ventanilla* (Persona - directo), *Médico Veterinario* (Persona - apoyo clínico mediado), *Ciudadano Adoptante* (Persona - apoyo presencial) e *Impresora local* (Sistema externo).

| ID | Nombre del Caso de Uso | Actor Principal | Requerimientos Asociados |
| :---: | :--- | :--- | :--- |
| **CU-01** | Iniciar sesión en estación de ventanilla | Encargado de Ventanilla / Administrador | `RF-02`, `RF-03`, `RNF-02`, `RNF-03` |
| **CU-02** | Gestionar credenciales de encargados de ventanilla | Administrador del Refugio | `RF-01`, `RNF-02` |
| **CU-03** | Registrar y actualizar mascota en canil o gatera | Administrador del Refugio | `RF-04`, `RNF-05` |
| **CU-04** | Registrar y actualizar ficha clínica y etológica | Administrador *(apoyo: Médico Veterinario)* | `RF-05`, `RD-01`, `RD-02`, `RD-03` |
| **CU-05** | Registrar y actualizar perfil socioeconómico del adoptante | Encargado *(apoyo: Ciudadano Adoptante)* | `RF-07`, `RD-06`, `RNF-04` |
| **CU-06** | Evaluar compatibilidad entre adoptante y mascota | Encargado de Ventanilla | `RF-06`, `RF-08`, `RF-09`, `RD-04`, `RD-05`, `RNF-01`, `RNF-06` |
| **CU-07** | Explorar caniles y gateras compatibles y ficha clínica | Encargado de Ventanilla | `RF-10`, `RF-11`, `RNF-01`, `RNF-04` |
| **CU-08** | Confirmar adopción y emitir Acta de Adopción | Encargado *(apoyo: Adoptante, Impresora)* | `RF-12`, `RD-06`, `RD-07`, `RD-08`, `RNF-07`, `RNF-08` |

### 6.3. Escenarios Detallados Críticos (`CU-06` y `CU-08`)
* **`CU-06` (Evaluar compatibilidad entre adoptante y mascota):**  
  * *Precondiciones:* Sesión activa (`CU-01`), perfil validado del adoptante sin restricciones vigentes (`CU-05`, `RD-06`) y mascotas disponibles con ficha sanitaria/etológica completa (`RD-01`–`RD-03`).  
  * *Flujo principal:* (1) El Encargado solicita evaluar compatibilidad; (2) el sistema recupera mascotas elegibles (`RF-06`); (3) calcula $C = 30V + 30T + 20P + 20F$ acotando $T, P \le 1$ (`RD-04`); (4) filtra $C \ge 70\%$ sin riesgo crítico familiar (`RD-05`) y ordena descendentemente en $\le 2\text{ s}$ (`RNF-01`).  
  * *Flujos alternativos:* Si ninguna mascota alcanza $70\%$, muestra el mensaje *"No se encontraron mascotas compatibles con el perfil registrado"*; si el adoptante desiste, se cierra sin alterar estados.
* **`CU-08` (Confirmar adopción y emitir Acta de Adopción):**  
  * *Precondiciones:* Mascota compatible seleccionada en `CU-07`, revisión de ficha clínica y aceptación expresa del ciudadano junto con aprobación institucional.  
  * *Flujo principal:* (1) El Encargado confirma la entrega; (2) el sistema revalida compatibilidad (`<<include>> CU-06`) y verifica en transacción ACID (`RNF-08`) que la mascota siga *Disponible*; (3) registra el Acta de Adopción inmutable (`RD-07`) con auditoría de usuario, rol, fecha y hora (`RD-08`); (4) actualiza la mascota a *Adoptado* y despliega el formato A4 para impresión (`RNF-07`).  
  * *Flujos alternativos:* Ante desistimiento previo, mantiene estado *Disponible* (`RF-11`); ante concurrencia simultánea, ejecuta *rollback* y notifica indisponibilidad; ante fallo de impresora, permite reimprimir sin duplicar el acta.

---

## 7. Historias de Usuario y Criterios de Aceptación

1. **`HU-01` (`RF-01`, `RNF-02`):** **Como** Administrador del Refugio, **quiero** crear, habilitar y revocar cuentas de encargados, **para** controlar quién opera el sistema.  
   * *Dado que* estoy autenticado como Administrador, *cuando* registro una cuenta con cédula, usuario único y clave, *entonces* se guarda con hash SHA-256 y estado *Activo*.  
   * *Dado que* revoco una cuenta activa, *cuando* el encargado intenta ingresar, *entonces* el acceso es bloqueado inmediatamente.
2. **`HU-02` (`RF-02`, `RF-03`, `RNF-02`):** **Como** Encargado de Ventanilla, **quiero** iniciar sesión con mis credenciales, **para** atender adopciones sin acceder a funciones administrativas.  
   * *Dado que* ingreso credenciales válidas de Encargado, *cuando* accedo al sistema, *entonces* los menús administrativos permanecen deshabilitados.  
   * *Dado que* se fallan 3 intentos seguidos de clave, *cuando* ocurre el tercer error, *entonces* la cuenta se bloquea por 15 minutos.
3. **`HU-03` (`RF-04`, `RNF-05`):** **Como** Administrador del Refugio, **quiero** registrar mascotas asignándoles un canil o gatera libre, **para** mantener actualizado el inventario físico.  
   * *Dado que* existe un canil libre, *cuando* registro una mascota en estado *Disponible*, *entonces* queda vinculada a dicho espacio.  
   * *Dado que* una mascota está *Adoptada*, *cuando* se consulta el catálogo, *entonces* queda excluida de las opciones elegibles.
4. **`HU-04` (`RF-05`, `RF-06`, `RD-01`–`RD-03`):** **Como** Médico Veterinario (vía flujo administrativo), **quiero** registrar vacunas, esterilización y perfil etológico, **para** habilitar solo animales aptos.  
   * *Dado que* la mascota tiene vacunas, esterilización y perfil etológico completos, *cuando* se valida su ficha, *entonces* es elegible en `RF-06`.  
   * *Dado que* falta vacunación o esterilización, *cuando* se evalúa compatibilidad, *entonces* el animal es excluido automáticamente.
5. **`HU-05` (`RF-07`, `RD-06`, `RNF-04`):** **Como** Encargado de Ventanilla, **quiero** registrar el perfil socioeconómico del adoptante, **para** capturar datos validados antes del cálculo.  
   * *Dado que* el solicitante no tiene sanciones vigentes, *cuando* ingreso valores válidos no negativos, *entonces* se almacena su perfil.  
   * *Dado que* existen campos vacíos, valores negativos o restricción institucional vigente, *cuando* intento guardar, *entonces* el sistema rechaza el avance.
6. **`HU-06` (`RF-08`, `RF-09`, `RD-04`, `RD-05`, `RNF-01`):** **Como** Encargado de Ventanilla, **quiero** calcular y filtrar compatibilidad $\ge 70\%$, **para** recomendar opciones objetivas.  
   * *Dado que* existen mascotas aptas, *cuando* ejecuto la evaluación, *entonces* se ordenan descendentemente las que cumplen $C \ge 70\%$ en $\le 2\text{ s}$.  
   * *Dado que* ninguna alcanza $70\%$ o hay riesgo crítico familiar, *cuando* finaliza el cálculo, *entonces* se excluyen y se muestra el mensaje normalizado si no hay coincidencias.
7. **`HU-07` (`RF-10`, `RF-11`, `RNF-04`):** **Como** Encargado de Ventanilla, **quiero** explorar caniles compatibles y registrar desistimientos, **para** guiar la decisión sin alterar el inventario si desisten.  
   * *Dado que* hay mascotas compatibles, *cuando* abro el mapa, *entonces* solo los caniles aptos permiten clic y lectura de ficha clínica.  
   * *Dado que* el ciudadano desiste, *cuando* registro el desistimiento informado, *entonces* la mascota sigue *Disponible* sin emitir acta.
8. **`HU-08` (`RF-12`, `RD-07`, `RD-08`, `RNF-07`, `RNF-08`):** **Como** Encargado de Ventanilla, **quiero** confirmar la adopción de forma atómica, **para** emitir un acta inmutable y auditada.  
   * *Dado que* la mascota sigue *Disponible*, *cuando* confirmo la adopción, *entonces* se genera el acta auditada en A4 y el estado pasa a *Adoptado*.  
   * *Dado que* otra ventanilla adopta simultáneamente la misma mascota, *cuando* confirmo, *entonces* se revierte la operación evitando duplicados.

---

## 8. Matriz de Priorización MoSCoW y Acuerdo de Negociación

### 8.1. Clasificación de Requerimientos
* **Must have (21 requerimientos):** `RF-01`, `RF-02`, `RF-04`, `RF-05`, `RF-06`, `RF-07`, `RF-08`, `RF-09`, `RF-10`, `RF-12`, `RNF-01`, `RNF-02`, `RNF-03`, `RNF-07`, `RNF-08`, `RD-01`, `RD-02`, `RD-03`, `RD-04`, `RD-05`, `RD-07`, `RD-08` *(Núcleo crítico de seguridad, sanidad, cálculo $\ge 70\%$, mapa de caniles y emisión atómica de actas)*.
* **Should have (6 requerimientos):** `RF-03` (verificación previa de catálogo), `RF-11` (registro formal de desistimiento), `RNF-04` (eficiencia en 5 pasos), `RNF-06` (arquitectura por capas para calibrar pesos) y `RD-06` (restricciones institucionales vigentes sin bloqueo por devolución previa).
* **Could have (1 requerimiento):** `RNF-05` (escalabilidad histórica hasta $5.000$ mascotas y $10.000$ actas).
* **Won't have (Exclusiones):** Cuentas propias para veterinario/caniles, portal web público, aplicación móvil y pasarela de pagos.

### 8.2. Acuerdo de Negociación e Iteraciones
Se negociaron los cinco conflictos detectados (`C-01` a `C-05`): en **Iteración 1 (`Must`)** se construye el flujo completo de ventanilla con la fórmula base $C = 30V + 30T + 20P + 20F$ y corte en $70\%$; en **Iteración 2 (`Should`)** se entregan los controles complementarios, la arquitectura desacoplada para calibración paramétrica (`C-01`) y el registro formal de desistimientos y sanciones vigentes (`C-04`); y en **Iteración 3 (`Could`)** se optimiza el rendimiento histórico masivo.

---

## 9. Matriz de Trazabilidad

| ID | Fuente (`PPE04`) | Caso de Uso | Criterio de Aceptación Resumido | Prueba Propuesta | Estado |
| :---: | :--- | :---: | :--- | :--- | :---: |
| **RF-01** | Administrador (`Reg. 102`) | `CU-02` | Cuenta creada/actualizada en BD con estado asignado. | Alta y revocación de usuario en BD local. | Pendiente |
| **RF-02** | Administrador (`Reg. 102, 104`) | `CU-01` | Bloqueo de menús administrativos para rol Encargado. | Login con rol Encargado y verificación de restricciones. | Pendiente |
| **RF-03** | Observación (`Reg. 104`) | `CU-01` | Bloqueo de entrevista ante sesión expirada o catálogo inactivo. | Simulación de sesión expirada antes de atender. | Pendiente |
| **RF-04** | Admin / Caniles (`Reg. 103, 112`) | `CU-03` | Mascota asociada a canil/gatera libre con estado operativo. | Registro de mascota en espacio libre y cambio de estado. | Pendiente |
| **RF-05** | Veterinario (`Reg. 103, 105–107`) | `CU-04` | Ficha clínica completa vinculada al ID de mascota. | Validación de campos sanitarios y etológicos obligatorios. | Pendiente |
| **RF-06** | Ventanilla / Clínica (`Reg. 105, 106`) | `CU-06` | Lista solo mascotas *Disponibles* con ficha validada. | Consulta sobre lote mixto de mascotas aptas y no aptas. | Pendiente |
| **RF-07** | Formulario / Obs. (`Reg. 108, 109`) | `CU-05` | Rechaza campos vacíos o negativos y guarda perfiles válidos. | Ingreso de datos válidos vs. negativos/vacíos. | Pendiente |
| **RF-08** | Admin / Veterinario (`Reg. 109, 110`) | `CU-06` | Calcula puntaje redondeado a dos decimales según fórmula. | Contraste matemático contra casos manuales. | Pendiente |
| **RF-09** | Ventanilla / Directiva (`Reg. 110, 111`) | `CU-06` | Filtra $C \ge 70\%$ ordenado descendentemente o muestra aviso. | Prueba de valores límite ($69,99\%$, $70\%$, $70,01\%$). | Pendiente |
| **RF-10** | Caniles / Obs. (`Reg. 112`) | `CU-07` | Habilita clic solo en caniles con mascotas compatibles. | Interacción en mapa sobre caniles aptos y bloqueados. | Pendiente |
| **RF-11** | Obs. / Adoptante (`Reg. 113`) | `CU-07` | Desistimiento mantiene estado *Disponible* sin emitir acta. | Ejecución de desistimiento y verificación de BD. | Pendiente |
| **RF-12** | Admin / Actas (`Reg. 114, 115`) | `CU-08` | Revalida disponibilidad, emite acta y cambia a *Adoptado*. | Confirmación integral y verificación de transición. | Pendiente |
| **RNF-01** | Observación (`Reg. 104, 111`) | `CU-06, CU-07` | Tiempo $\le 2\text{ s}$ en $\ge 95\%$ de consultas (500 mascotas). | Prueba de carga con 500 registros locales. | Pendiente |
| **RNF-02** | Administrador (`Reg. 102`) | `CU-01, CU-02` | Claves en SHA-256 y bloqueo 15 min al 3.er fallo. | Inspección de hash y 3 intentos fallidos seguidos. | Pendiente |
| **RNF-03** | Directiva (`Reg. 101`) | `CU-01`–`CU-08` | Operación íntegra local sin conexión a Internet. | Ejecución completa con red desconectada. | Pendiente |
| **RNF-04** | Ventanilla (`Reg. 108, 111`) | `CU-05`–`CU-08` | Trámite completo en $\le 5$ pasos de pantalla. | Conteo de vistas en flujo de atención simulado. | Pendiente |
| **RNF-05** | Administrador (`Reg. 101, 115`) | `CU-03, CU-08` | Degradación $< 10\%$ con 5.000 mascotas y 10.000 actas. | Prueba de volumen con datos sintéticos masivos. | Pendiente |
| **RNF-06** | Equipo Desarrollo (`PPE03`) | `CU-06` | Ajuste de pesos en capa de negocio sin tocar vistas. | Cambio de coeficientes verificando aislamiento de capas. | Pendiente |
| **RNF-07** | Centro Cómputo (`Reg. 101`) | `CU-08` | Visualización en $1366 \times 768\text{ px}$ e impresión A4. | Prueba visual en resolución mínima e impresión A4. | Pendiente |
| **RNF-08** | Registros (`Reg. 114`) | `CU-08` | Atomicidad ACID ante adopciones simultáneas. | Prueba de concurrencia desde dos estaciones. | Pendiente |
| **RD-01** | Veterinario (`Reg. 105`) | `CU-04, CU-06` | Excluye mascotas sin vacunación vigente validada. | Evaluación con vacuna pendiente o vencida. | Pendiente |
| **RD-02** | Veterinario (`Reg. 106`) | `CU-04, CU-06` | Excluye mascotas sin esterilización validada. | Evaluación con mascota no esterilizada. | Pendiente |
| **RD-03** | Veterinario (`Reg. 107`) | `CU-04, CU-06` | Excluye mascotas con perfil etológico incompleto. | Evaluación omitiendo atributos conductuales. | Pendiente |
| **RD-04** | Admin / Vet (`Reg. 109, 110, P-03`) | `CU-06` | Acotación $T, P \le 1$ y requisitos $> 0$ sin exceder $100\%$. | Prueba con valores excedentes y denominadores válidos. | Pendiente |
| **RD-05** | Admin / Vet (`Reg. 110, P-04`) | `CU-06` | Bloquea recomendación ante riesgo crítico familiar. | Evaluación con $C = 85\%$ pero incompatibilidad crítica. | Pendiente |
| **RD-06** | Directiva (`P-02, C-04`) | `CU-05, CU-08` | Bloquea solo sanciones vigentes; no bloquea por devolución. | Prueba comparativa con devolución vs. sanción activa. | Pendiente |
| **RD-07** | Actas / Directiva (`Reg. 115, P-05`) | `CU-08` | Impide editar/borrar actas; exige asiento vinculado. | Verificación de bloqueo de edición/borrado en BD. | Pendiente |
| **RD-08** | Admin / Reguladora (`Reg. 115`) | `CU-08` | Registra usuario, rol, fecha y hora exacta en cada acta. | Auditoría de campos generados en tabla de actas. | Pendiente |

---

## 10. Resultados de Validación

Se aplicó el checklist de validación de 9 criterios (`P1`: Necesidad real, `P2`: Claridad, `P3`: Consistencia, `P4`: Completitud, `P5`: Factibilidad, `P6`: Verificabilidad, `P7`: Fuente identificada, `P8`: Prioridad asignada, `P9`: Revisión por interesados) sobre los 28 requerimientos, obteniendo cumplimiento documental completo (`P1` a `P8` = **Sí** en el $100\%$ de ítems) y manteniendo bajo calibración operativa en campo únicamente los parámetros institucionales (`RF-09`, `RF-12`, `RD-04`, `RD-06`). El detalle completo por requerimiento y las ocho observaciones corregidas (`OBS-01` a `OBS-08`) constan en `/Documentacion/06_Trazabilidad_y_Validacion/02_Resultados_Validacion.md`.

---

## 11. Control de Versiones

| Versión | Fecha | Autor(es) | Descripción del cambio | Aprobado por |
| :---: | :---: | :--- | :--- | :--- |
| **v0.1** | 20/11/2026 | Jostin Aldas, Paul Olmos, Galo Castillo | Configuración base del repositorio, solución WinForms (.NET 8), Visión, Alcance y Matriz de Interesados (`PPE03`). | Jostin Aldas / Ing. José Naranjo |
| **v0.2** | 21/11/2026 | Paul Olmos, Jostin Aldas | Plan de adquisición, 15 registros de elicitación (`101`–`115`), aclaraciones y conflictos (`PPE04`). | Jostin Aldas / Ing. José Naranjo |
| **v0.4** | 22/11/2026 | Jostin Aldas | Catálogo maestro (`28 requerimientos`), análisis de defectos, dependencias y viabilidad (`PPE05`). | Jostin Aldas / Ing. José Naranjo |
| **v0.6** | 22/11/2026 | Jostin Aldas | Fichas formales individuales de requerimientos refinados y verificables (`PPE06`). | Jostin Aldas / Ing. José Naranjo |
| **v0.8** | 22/11/2026 | Galo Castillo, Paul Olmos, Jostin Aldas | Modelo y diagrama UML de Casos de Uso, Historias de Usuario y Matriz MoSCoW (`PPE07`). | Jostin Aldas / Ing. José Naranjo |
| **v1.0** | 22/11/2026 | Jostin Aldas, Galo Castillo, Paul Olmos | Consolidación integral del `SRS_Final.md`, Matriz de Trazabilidad, Validación y cierre de la IR (`PPE08`). | Equipo AffiniPet / Ing. José Naranjo |