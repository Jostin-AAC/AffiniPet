# Resultados de Validación de Requerimientos — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE08 — Matriz de Trazabilidad, Validación y Cierre de la IR  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Metodología y Alcance de la Validación

Con el objetivo de comprobar que los requerimientos especificados en `PPE06` representan las necesidades reales de los interesados del refugio y cumplen con los atributos de calidad exigidos antes de iniciar la codificación en Visual Studio, se ejecutó una revisión técnica y documental aplicando el **Checklist de 9 Preguntas de Validación** sobre los **28 requerimientos aprobados** (`RF-01` a `RF-12`, `RNF-01` a `RNF-08` y `RD-01` a `RD-08`).

### Criterios evaluados (`P1` a `P9`)
* **P1:** ¿Representa una necesidad real?
* **P2:** ¿Es claro?
* **P3:** ¿Es consistente con los demás?
* **P4:** ¿Está completo?
* **P5:** ¿Es factible?
* **P6:** ¿Es verificable?
* **P7:** ¿Tiene fuente identificada?
* **P8:** ¿Tiene prioridad asignada?
* **P9:** ¿Fue revisado por los interesados?

*(Nota de interpretación: **Sí** indica cumplimiento verificado documentalmente; **Pendiente** indica ratificación operativa final en campo durante la puesta en marcha en el refugio).*

---

## 2. Checklist de Validación por Requerimiento

| ID | Requerimiento | P1 | P2 | P3 | P4 | P5 | P6 | P7 | P8 | P9 | Observaciones |
| :---: | :--- | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **RF-01** | Gestión de cuentas de encargados de ventanilla | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Validado con Administrador del Refugio (`Reg. 102`); flujo claro y verificable. |
| **RF-02** | Autenticación y control de acceso por rol | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Separación de permisos aprobada entre Administrador y Encargado (`C-02`). |
| **RF-03** | Verificación previa de sesión y catálogo activo | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Control preventivo derivado de la observación en ventanilla (`Reg. 104`). |
| **RF-04** | Registro de mascotas y asignación de canil o gatera | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Validado con Administración y Encargado de Caniles (`Reg. 103, 112`). |
| **RF-05** | Registro y validación de ficha clínica y etológica | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Ajustado para canalizar los datos del veterinario mediante circuito administrativo (`OBS-04`). |
| **RF-06** | Consulta de mascotas elegibles para adopción | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Coherente con las reglas sanitarias `RD-01`, `RD-02` y `RD-03`. |
| **RF-07** | Registro del perfil socioeconómico del adoptante | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Validado con formularios físicos actuales y observación presencial (`Reg. 108`). |
| **RF-08** | Cálculo ponderado de compatibilidad | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Fórmula $C = 30V + 30T + 20P + 20F$ verificable mediante casos matemáticos (`OBS-03`). |
| **RF-09** | Filtrado y ordenamiento por umbral de compatibilidad | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Pendiente | Corte inclusivo $C \ge 70\%$ definido; calibración final en campo pendiente con Directiva (`C-01`). |
| **RF-10** | Despliegue de mapa interactivo de caniles y gateras | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Validado con Encargado de Caniles y Ventanilla para evitar selección de no aptos. |
| **RF-11** | Consulta de detalle clínico y desistimiento informado | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Garantiza conservar estado *Disponible* ante desistimiento (`Reg. 113`). |
| **RF-12** | Confirmación transaccional y emisión del Acta de Adopción | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Pendiente | Regla atómica aprobada; delegación operativa de aprobación en ventanilla en ajuste (`C-03`). |
| **RNF-01** | Tiempo de respuesta en cálculo y carga de mapa ($\le 2\text{ s}$) | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Métrica cuantificable definida tras corregir ambigüedad inicial (`PPE05`). |
| **RNF-02** | Cifrado de credenciales (SHA-256) y bloqueo por fallos | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Parámetros de seguridad verificables (hash SHA-256 y 3 intentos / 15 min). |
| **RNF-03** | Disponibilidad operativa local sin conexión externa | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Alineado con la restricción de operación local sin Internet (`PPE03`). |
| **RNF-04** | Eficiencia operativa ($\le 5$ pasos) y facilidad de aprendizaje | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Resuelve el conflicto `C-05` equilibrando agilidad en ventanilla y validaciones. |
| **RNF-05** | Capacidad de almacenamiento y escalabilidad local | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Priorizado como *Could* para crecimiento histórico a mediano plazo. |
| **RNF-06** | Arquitectura por capas y mantenibilidad | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Facilita calibrar pesos de compatibilidad sin modificar pantallas (`C-01`). |
| **RNF-07** | Compatibilidad de entorno Windows e impresión A4 | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Ajustado a estaciones Windows 10/11 ($1366 \times 768\text{ px}$) e impresora local A4 (`OBS-06`). |
| **RNF-08** | Integridad transaccional ACID ante concurrencia | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Evita adopciones duplicadas simultáneas entre ventanillas (`Reg. 114`). |
| **RD-01** | Obligatoriedad de vacunación vigente | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Regla clínica ratificada por Médico Veterinario (`Reg. 105`). |
| **RD-02** | Obligatoriedad de esterilización validada | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Regla clínica ratificada por Médico Veterinario (`Reg. 106`). |
| **RD-03** | Completitud del perfil etológico de la mascota | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Necesario para alimentar los factores de compatibilidad (`Reg. 107`). |
| **RD-04** | Fórmula ponderada y acotación de factores | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Pendiente | Acotación $\min(1, \text{disp}/\text{req})$ completa; calibración final pendiente con Directiva (`C-01`). |
| **RD-05** | Umbral mínimo inclusivo ($C \ge 70\%$) y bloqueo crítico | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Protege al animal bloqueando riesgos críticos familiares sin importar el puntaje. |
| **RD-06** | Restricciones institucionales vigentes sobre adoptantes | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Pendiente | Aclarado que devoluciones previas no bloquean automáticamente; catálogo en cierre (`C-04`). |
| **RD-07** | Inmutabilidad documental de las actas emitidas | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Protege integridad legal e histórica mediante rectificaciones vinculadas (`P-05`). |
| **RD-08** | Trazabilidad de auditoría en actas de adopción | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Sí | Cumple exigencias de control interno y respaldo ante Entidad Regulatoria (`Reg. 115`). |

---

## 3. Observaciones y Correcciones Derivadas de la Validación

Durante las sesiones de revisión documental y mesas de negociación con los interesados del refugio (`PPE04`, `PPE05` y `PPE07`), se registraron las siguientes observaciones y se aplicaron sus respectivas correcciones sobre la especificación:

| ID | Observación detectada | Corrección aplicada en el SRS | Requerimientos afectados |
| :---: | :--- | :--- | :---: |
| **OBS-01** | El enunciado inicial de tiempo de respuesta usaba términos subjetivos (*"rápidamente"*, *"sin demoras"*), impidiendo su verificación objetiva. | Se estableció un umbral cuantificable de **$\le 2\text{ segundos}$** para el **$95\%$** de solicitudes en catálogos de hasta **500 mascotas**. | `RNF-01` |
| **OBS-02** | Posible duplicidad conceptual entre *"gasto mensual estimado"* de la mascota y *"presupuesto mínimo"* requerido. | Se unificó el criterio empleando exclusivamente el **gasto mensual estimado en USD** de la mascota como denominador del factor $P$ (`Reg. 109`). | `RF-05`, `RD-04` |
| **OBS-03** | Cuando el adoptante disponía de más tiempo o presupuesto del requerido por la mascota, el cálculo podía exceder el $100\%$. | Se acotaron superiormente los factores $T$ y $P$ mediante $\min(1, \text{disponible}/\text{requerido})$, exigiendo requisitos $> 0$ para evitar división por cero. | `RF-08`, `RD-04` |
| **OBS-04** | El Médico Veterinario y el Encargado de Caniles no poseen cuentas propias en esta versión, generando duda sobre cómo se actualizan sus datos. | Se precisó en `RF-04` y `RF-05` que la información clínica y de ubicación aportada por dichos roles se registra mediante el circuito administrativo autorizado. | `RF-04`, `RF-05` |
| **OBS-05** | Faltaba definir el comportamiento del sistema cuando en un hogar coexisten múltiples condiciones familiares (por ejemplo, niños pequeños y otra mascota incompatible). | Se incorporó en `RD-05` la regla del criterio más restrictivo y el bloqueo inmediato ante incompatibilidades críticas familiares sin importar el puntaje $C$. | `RF-09`, `RD-05` |
| **OBS-06** | No se diferenciaba entre un fallo al guardar el acta en base de datos y un fallo físico en la impresora local de ventanilla. | Se especificó en `CU-08`, `RF-12` y `RNF-08` que la transacción atómica asegura el guardado del acta y el cambio a *Adoptado*; un fallo de impresión permite reimprimir sin duplicar el registro. | `RF-12`, `RNF-07`, `RNF-08` |
| **OBS-07** | Existía riesgo de rechazar automáticamente a solicitantes con devoluciones previas sin evaluar la causa real. | Se precisó en `RD-06` que una devolución previa no constituye bloqueo automático; solo bloquean las restricciones institucionales vigentes declaradas por el refugio. | `RF-07`, `RD-06` |
| **OBS-08** | Se propuso inicialmente incluir cobros de tasas de adopción y emisión de recibos contables dentro del formulario de confirmación. | Se descartó dicha propuesta por encontrarse explícitamente fuera del alcance definido en `PPE03` (`01_Vision_y_Alcance.md`). | Alcance (`PPE03`) |