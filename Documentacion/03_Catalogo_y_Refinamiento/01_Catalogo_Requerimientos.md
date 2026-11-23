# Catálogo Maestro de Requerimientos — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE05 — Catálogo de Requerimientos y Análisis de Refinamiento  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Criterios de Clasificación y Trazabilidad

El presente catálogo consolida y clasifica la totalidad de los requerimientos identificados durante el proceso de elicitación (`PPE04`), manteniendo trazabilidad directa hacia los interesados del refugio (`PPE03`) y los registros numerados (`101` a `115`). De acuerdo con los criterios metodológicos de la ingeniería de requerimientos, cada ítem se organiza bajo los siguientes parámetros:

* **Tipo:**
  * **Funcional (`RF`):** Describe una función, servicio o comportamiento específico que debe ejecutar el software.
  * **No Funcional (`RNF`):** Define un atributo de calidad, restricción técnica o condición operativa (rendimiento, seguridad, disponibilidad, usabilidad, escalabilidad, mantenibilidad, compatibilidad y confiabilidad).
  * **Dominio (`RD`):** Establece una regla o política invariante propia del contexto veterinario, ético e institucional del refugio de animales.
* **Nivel:**
  * **Usuario:** Expresa una necesidad o regla de negocio desde la perspectiva operativa del personal del refugio.
  * **Sistema:** Detalla de forma precisa y técnica la conducta o restricción verificable que debe cumplir la aplicación.
* **Estado:**
  * **Analizado:** Requerimiento examinado durante la revisión técnica que requiere ajustes de redacción antes de su aprobación final (documentados en `02_Informe_Analisis_Refinamiento.md`).
  * **Aprobado:** Requerimiento claro, consistente, viable y listo para pasar a especificación formal (`PPE06`).

---

## 2. Tabla Consolidada del Catálogo de Requerimientos

La tabla incluye **12 Requerimientos Funcionales (`RF-01` a `RF-12`)**, **8 Requerimientos No Funcionales (`RNF-01` a `RNF-08`)** y **8 Requerimientos de Dominio (`RD-01` a `RD-08`)**, congelando esta codificación para los entregables posteriores:

| ID | Descripción | Tipo | Nivel | Fuente | Estado |
| :---: | :--- | :---: | :---: | :--- | :---: |
| **RF-01** | El sistema deberá permitir al Administrador del Refugio crear, habilitar y revocar cuentas de usuario para los encargados de ventanilla mediante cédula, nombres, usuario único y contraseña inicial. | Funcional | Sistema | Administrador del Refugio (`Reg. 102`) | Aprobado |
| **RF-02** | El sistema deberá autenticar las credenciales ingresadas en el inicio de sesión y restringir el acceso a las funciones administrativas cuando el usuario tenga el rol de Encargado de Ventanilla. | Funcional | Sistema | Administrador del Refugio (`Reg. 102, 104`) | Aprobado |
| **RF-03** | El sistema deberá verificar que la sesión esté vigente, la ventanilla habilitada y el catálogo institucional activo antes de permitir iniciar una atención presencial. | Funcional | Sistema | Observación directa en ventanilla (`Reg. 104`) | Aprobado |
| **RF-04** | El sistema deberá registrar y actualizar los datos maestros de cada mascota del refugio, asignándole un canil o gatera disponible y controlando su estado operativo (*Disponible*, *Adoptado*, *No disponible*). | Funcional | Sistema | Administrador y Encargado de Caniles (`Reg. 103, 112`) | Aprobado |
| **RF-05** | El sistema deberá registrar y validar la ficha clínica y etológica de cada mascota, incluyendo estado de vacunación, esterilización, especie, tamaño, nivel de energía, gasto mensual estimado y tolerancia familiar. | Funcional | Sistema | Médico Veterinario / Ficha Clínica (`Reg. 103, 105, 106, 107`) | Analizado |
| **RF-06** | El sistema deberá permitir consultar el listado de mascotas elegibles mostrando únicamente aquellas en estado *Disponible* que cuenten con su ficha clínica y sanitaria validada. | Funcional | Sistema | Encargado de Ventanilla / Ficha Clínica (`Reg. 105, 106`) | Aprobado |
| **RF-07** | El sistema deberá registrar el perfil presencial del adoptante validando obligatoriedad de campos y valores numéricos no negativos para vivienda, minutos diarios disponibles, presupuesto mensual (USD) y composición familiar. | Funcional | Sistema | Formulario de solicitud / Observación (`Reg. 108, 109`) | Aprobado |
| **RF-08** | El sistema deberá calcular el porcentaje de compatibilidad ($0\%$ a $100\%$) entre el perfil del adoptante y cada mascota disponible elegible mediante la evaluación ponderada de vivienda, tiempo, presupuesto y familia. | Funcional | Sistema | Administrador y Médico Veterinario (`Reg. 109, 110`) | Aprobado |
| **RF-09** | El sistema deberá filtrar los resultados de compatibilidad conservando únicamente las mascotas con puntaje $C \ge 70\%$ ordenadas de mayor a menor afinidad (desempatando por ID ascendente), o mostrar el mensaje normalizado si no existen coincidencias. | Funcional | Sistema | Encargado de Ventanilla / Directiva (`Reg. 110, 111`) | Aprobado |
| **RF-10** | El sistema deberá desplegar un mapa interactivo de caniles y gateras habilitando únicamente los espacios que contengan mascotas compatibles ($C \ge 70\%$) y bloqueando visualmente los espacios vacíos, en cuarentena o no aptos. | Funcional | Sistema | Encargado de Caniles / Observación (`Reg. 112`) | Aprobado |
| **RF-11** | El sistema deberá permitir consultar el detalle clínico-etológico de una mascota compatible en ventanilla y registrar el desistimiento informado del adoptante manteniendo intacto el estado *Disponible* sin emitir acta. | Funcional | Sistema | Observación en ventanilla / Adoptante (`Reg. 113`) | Aprobado |
| **RF-12** | El sistema deberá revalidar la disponibilidad de la mascota al confirmar la adopción, ejecutar atómicamente el registro del Acta de Adopción y cambiar el estado de la mascota a *Adoptado*. | Funcional | Sistema | Administrador y Actas históricas (`Reg. 114, 115`) | Analizado |
| **RNF-01** | El sistema deberá calcular la compatibilidad y desplegar el mapa de caniles en un tiempo máximo de 2 segundos para el $95\%$ de las solicitudes realizadas sobre un catálogo de hasta 500 mascotas. | No Funcional | Sistema | Observación en ventanilla (`Reg. 104, 111`) | Analizado |
| **RNF-02** | El sistema deberá almacenar las contraseñas de usuarios cifradas mediante hash SHA-256 y bloquear temporalmente la cuenta durante 15 minutos tras 3 intentos fallidos consecutivos de acceso. | No Funcional | Sistema | Administrador del Refugio (`Reg. 102`) | Aprobado |
| **RNF-03** | El sistema deberá mantener una disponibilidad operativa mínima del $99,5\%$ durante el horario presencial del refugio (08h00 a 17h00), funcionando sobre una base de datos local sin requerir conexión a internet. | No Funcional | Sistema | Directiva del Refugio (`Reg. 101`) | Aprobado |
| **RNF-04** | El sistema deberá permitir completar el flujo presencial de registro, evaluación, consulta clínica y emisión de acta en un máximo de 5 pasos de pantalla, con una tasa máxima de 1 error de operación por hora tras 30 minutos de capacitación. | No Funcional | Sistema | Encargado de Ventanilla (`Reg. 108, 111`) | Aprobado |
| **RNF-05** | El sistema deberá soportar el almacenamiento y consulta de hasta 5.000 registros de mascotas y 10.000 actas históricas manteniendo una variación en el tiempo de respuesta inferior al $10\%$. | No Funcional | Sistema | Administrador del Refugio (`Reg. 101, 115`) | Aprobado |
| **RNF-06** | El sistema deberá estructurarse bajo arquitectura de programación por capas (Presentación, Lógica de Negocio y Acceso a Datos) en C# sobre .NET 8.0, permitiendo ajustar los parámetros de compatibilidad sin modificar la interfaz gráfica. | No Funcional | Sistema | Equipo de Desarrollo / Restricciones (`PPE03`) | Aprobado |
| **RNF-07** | El sistema deberá ejecutarse en estaciones de trabajo con Windows 10 o Windows 11 (64 bits, resolución mínima de $1366 \times 768\text{ px}$) y generar el Acta de Adopción compatible con impresión estándar en formato A4. | No Funcional | Sistema | Centro de Cómputo del Refugio (`Reg. 101`) | Aprobado |
| **RNF-08** | El sistema deberá garantizar integridad transaccional ACID al confirmar adopciones simultáneas entre ventanillas, asegurando $0$ registros duplicados o inconsistentes ante cierres inesperados. | No Funcional | Sistema | Análisis de registros existentes (`Reg. 114`) | Aprobado |
| **RD-01** | Ninguna mascota podrá ser evaluada ni entregada en adopción si no cuenta con su esquema de vacunación vigente registrado y validado en la ficha clínica. | Dominio | Usuario | Médico Veterinario / Ficha Clínica (`Reg. 105`) | Aprobado |
| **RD-02** | Toda mascota elegible para adopción deberá contar con su procedimiento de esterilización realizado, registrado y validado previamente por el área clínica. | Dominio | Usuario | Médico Veterinario / Reglamento (`Reg. 106`) | Aprobado |
| **RD-03** | Toda mascota deberá tener completo su perfil etológico (especie, tamaño, energía, temperamento y tolerancia familiar) antes de participar en el cálculo de compatibilidad. | Dominio | Usuario | Médico Veterinario / Fichas de conducta (`Reg. 107`) | Aprobado |
| **RD-04** | El cálculo de compatibilidad deberá aplicar la fórmula $C = 30V + 30T + 20P + 20F$, redondeando a dos decimales y acotando los factores $T$ y $P$ a un máximo de $1$ mediante $\min(1, \text{disponible}/\text{requerido})$ con requisitos $> 0$. | Dominio | Sistema | Directiva y Médico Veterinario (`Reg. 109, 110`) | Analizado |
| **RD-05** | Una mascota se considerará compatible únicamente si alcanza $C \ge 70\%$ y no presenta incompatibilidades críticas familiares; ante riesgo crítico familiar, se bloqueará su recomendación independientemente del puntaje global. | Dominio | Sistema | Directiva y Médico Veterinario (`Reg. 110, P-04`) | Aprobado |
| **RD-06** | El sistema bloqueará el trámite únicamente cuando el adoptante mantenga una restricción institucional vigente declarada por el refugio, sin considerar una devolución previa como bloqueo automático. | Dominio | Usuario | Directiva del Refugio (`P-02, C-04`) | Aprobado |
| **RD-07** | Las actas de adopción emitidas son registros históricos inmutables que no pueden editarse ni eliminarse físicamente; cualquier corrección posterior exigirá un asiento de rectificación vinculado al acta original. | Dominio | Sistema | Actas históricas / Directiva (`Reg. 115, P-05`) | Aprobado |
| **RD-08** | Toda acta de adopción formalizada deberá registrar obligatoriamente el identificador y nombre del usuario responsable, su rol operativo, y la fecha y hora exacta de confirmación. | Dominio | Sistema | Administrador y Entidad Regulatoria (`Reg. 115`) | Aprobado |

*(Nota: Los requerimientos marcados inicialmente en estado **Analizado** —`RF-05`, `RF-12`, `RNF-01` y `RD-04`— corresponden a los ítems depurados durante la revisión técnica y pasan a estado **Aprobado** tras su reformulación en el documento `02_Informe_Analisis_Refinamiento.md`).*