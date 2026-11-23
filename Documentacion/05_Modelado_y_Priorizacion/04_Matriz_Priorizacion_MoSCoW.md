# Matriz de Priorización MoSCoW y Acuerdo de Negociación — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE07 — Modelado de Casos de Uso, Historias de Usuario y Priorización  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Criterios de Priorización MoSCoW

Para planificar de forma ordenada la construcción incremental de **AffiniPet**, todos los requerimientos especificados en `PPE06` (`12 RF`, `8 RNF` y `8 RD`) se clasifican mediante la técnica **MoSCoW** bajo los siguientes criterios:

* **Must have (Debe tener):** Requerimientos indispensables para ejecutar el flujo crítico de atención presencial, garantizar la integridad sanitaria, calcular compatibilidad ($\ge 70\%$) y emitir actas inmutables con control transaccional.
* **Should have (Debería tener):** Controles preventivos complementarios, registro de desistimientos, facilidad de uso y arquitectura mantenible que fortalecen la operación sin bloquear el núcleo inicial.
* **Could have (Podría tener):** Capacidades de escalabilidad histórica ampliada deseables a mediano plazo.
* **Won't have (No tendrá en esta versión):** Funcionalidades explícitamente excluidas del alcance institucional (`PPE03` y `PPE04`).

---

## 2. Matriz de Priorización MoSCoW (Catálogo Completo `PPE06`)

| ID | Nombre del requerimiento | Prioridad (MoSCoW) | Justificación |
| :--- | :--- | :---: | :--- |
| **RF-01** | Gestión de cuentas de encargados de ventanilla | **Must** | Base del control de acceso; permite al Administrador habilitar operadores autorizados. |
| **RF-02** | Autenticación y control de acceso por rol | **Must** | Evita accesos no autorizados y separa las funciones administrativas de la operación en ventanilla. |
| **RF-03** | Verificación previa de sesión y catálogo activo | **Should** | Control preventivo útil para validar vigencia de sesión y catálogo antes de iniciar la entrevista. |
| **RF-04** | Registro de mascotas y asignación de canil o gatera | **Must** | Indispensable para mantener actualizado el inventario físico y los estados operativos de los animales. |
| **RF-05** | Registro y validación de ficha clínica y etológica | **Must** | Garantiza la captura de parámetros sanitarios y conductuales necesarios para el motor de cálculo. |
| **RF-06** | Consulta de mascotas elegibles para adopción | **Must** | Filtra únicamente animales disponibles con ficha clínica validada antes de evaluar compatibilidad. |
| **RF-07** | Registro del perfil socioeconómico del adoptante | **Must** | Captura obligatoria y validada de las variables del hogar necesarias para evaluar afinidad. |
| **RF-08** | Cálculo ponderado de compatibilidad | **Must** | Núcleo algorítmico del sistema que determina objetivamente el porcentaje de compatibilidad ($0\%$–$100\%$). |
| **RF-09** | Filtrado y ordenamiento por umbral de compatibilidad | **Must** | Aplica el corte institucional $C \ge 70\%$ y ordena las opciones aptas para orientar la decisión. |
| **RF-10** | Despliegue de mapa interactivo de caniles y gateras | **Must** | Habilita visualmente solo los espacios con mascotas compatibles y bloquea los no aptos. |
| **RF-11** | Consulta de detalle clínico y registro de desistimiento informado | **Should** | Permite documentar desistimientos manteniendo intacto el estado *Disponible* sin emitir acta. |
| **RF-12** | Confirmación transaccional y emisión del Acta de Adopción | **Must** | Cierra formalmente la adopción actualizando el estado a *Adoptado* y emitiendo el acta oficial. |
| **RNF-01** | Tiempo de respuesta en cálculo y carga de mapa ($\le 2\text{ s}$) | **Must** | Garantiza fluidez durante la atención presencial en ventanilla evitando demoras frente al ciudadano. |
| **RNF-02** | Cifrado de credenciales (SHA-256) y bloqueo por intentos fallidos | **Must** | Protege las cuentas del personal del refugio ante accesos indebidos en estaciones compartidas. |
| **RNF-03** | Disponibilidad operativa local sin conexión externa ($99,5\%$) | **Must** | Asegura continuidad operativa en el refugio independientemente de fallos de Internet. |
| **RNF-04** | Eficiencia operativa ($\le 5$ pasos) y facilidad de aprendizaje | **Should** | Optimiza la curva de aprendizaje de los encargados y agiliza el flujo en horas pico. |
| **RNF-05** | Capacidad de almacenamiento y escalabilidad local | **Could** | Soporta crecimiento histórico a largo plazo ($5.000$ mascotas / $10.000$ actas), no crítico en el arranque. |
| **RNF-06** | Arquitectura por capas y mantenibilidad | **Should** | Facilita calibrar pesos de compatibilidad en futuras revisiones sin rediseñar los formularios. |
| **RNF-07** | Compatibilidad de entorno Windows e impresión A4 | **Must** | Asegura funcionamiento en los equipos del refugio e impresión física del acta para firma. |
| **RNF-08** | Integridad transaccional ACID ante concurrencia | **Must** | Crítico para impedir adopciones duplicadas cuando dos ventanillas atienden simultáneamente. |
| **RD-01** | Obligatoriedad de vacunación vigente | **Must** | Política sanitaria no negociable antes de habilitar cualquier mascota para adopción. |
| **RD-02** | Obligatoriedad de esterilización validada | **Must** | Requisito clínico obligatorio de bienestar animal previo a la entrega. |
| **RD-03** | Completitud del perfil etológico de la mascota | **Must** | Sin rasgos conductuales completos el motor de compatibilidad no puede evaluar el entorno familiar. |
| **RD-04** | Fórmula ponderada y acotación de factores de compatibilidad | **Must** | Define la exactitud matemática del cálculo ($C = 30V + 30T + 20P + 20F$) y evita valores $> 100\%$. |
| **RD-05** | Umbral mínimo inclusivo ($C \ge 70\%$) y bloqueo por riesgo crítico | **Must** | Protege al animal y a la familia bloqueando adopciones incompatibles o peligrosas. |
| **RD-06** | Restricciones institucionales vigentes sobre adoptantes | **Should** | Estandariza los bloqueos vigentes evitando rechazar automáticamente por devoluciones previas. |
| **RD-07** | Inmutabilidad documental de las actas emitidas | **Must** | Preserva la integridad legal e histórica impidiendo alterar o borrar actas confirmadas. |
| **RD-08** | Trazabilidad de auditoría en actas de adopción | **Must** | Registra responsable, rol, fecha y hora exacta en cada entrega formalizada. |

### Exclusiones Confirmadas (`Won't have` en esta versión)
Conforme a lo delimitado en `PPE03` y `PPE04`, quedan clasificados como **Won't have**:
1. Cuentas de usuario directas para el Médico Veterinario o Encargado de Caniles (`C-02`).
2. Portal web público de consultas o postulación ciudadana en línea.
3. Aplicación móvil nativa o híbrida (Android/iOS).
4. Pasarela de pagos, cobro de tasas de adopción o contabilidad institucional.

---

## 3. Acuerdo de Negociación Documentado

Durante las sesiones de validación con los interesados del refugio (`PPE04`), se negociaron las prioridades de implementación para resolver los cinco conflictos detectados (`C-01` a `C-05`) y definir la hoja de ruta por iteraciones:

1. **Resolución de `C-01` (Calibración de pesos y umbral vs. criterio clínico):** Se acordó implementar en la primera iteración como **Must** (`RF-08`, `RF-09`, `RD-04`, `RD-05`) la fórmula base $C = 30V + 30T + 20P + 20F$ con corte inclusivo en $70\%$, estructurando el código por capas (`RNF-06`, **Should**) para que el Administrador pueda calibrar los coeficientes tras la validación clínica sin rehacer la interfaz.
2. **Resolución de `C-02` (Ausencia de cuentas propias para Veterinario y Caniles):** Se mantuvo como **Won't have** la creación de credenciales separadas para el área clínica, priorizando como **Must** (`RF-04`, `RF-05`) un módulo administrativo ágil donde el Administrador transcribe y valida oportunamente vacunas, esterilizaciones y ubicaciones antes de habilitar las mascotas.
3. **Resolución de `C-03` (Aprobación institucional en ventanilla):** Se acordó exigir la confirmación explícita en `RF-12` (**Must**) vinculando cada acta al operador autenticado y su rol (`RD-08`), permitiendo que la Directiva delegue operativamente dicha validación en ventanilla bajo auditoría completa.
4. **Resolución de `C-04` (Causales de bloqueo de adoptantes):** Se incorporó el control de restricciones institucionales vigentes en `RD-06` (**Should**), garantizando que una devolución previa no bloquee automáticamente al solicitante mientras la Directiva formaliza el catálogo interno de sanciones.
5. **Resolución de `C-05` (Agilidad presencial vs. rigor de validaciones):** Se resolvió manteniendo todas las validaciones sanitarias y transaccionales (`RD-01` a `RD-08`, `RNF-08`) en prioridad **Must**, pero automatizando los filtros en segundo plano con tiempos $\le 2\text{ segundos}$ (`RNF-01`, **Must**) y flujos de máximo 5 pasos (`RNF-04`, **Should**).

### Planificación por Iteraciones
* **Iteración 1 (Núcleo Crítico — `Must`):** Implementación completa del flujo de seguridad, gestión de mascotas/fichas clínicas, perfilamiento de adoptante, motor de compatibilidad ($\ge 70\%$), mapa interactivo de caniles y emisión transaccional de actas (`RF-01`, `RF-02`, `RF-04` a `RF-10`, `RF-12`, `RNF-01` a `RNF-03`, `RNF-07`, `RNF-08`, `RD-01` a `RD-05`, `RD-07`, `RD-08`).
* **Iteración 2 (Controles Complementarios y Usabilidad — `Should`):** Incorporación de verificación previa de catálogo (`RF-03`), registro formal de desistimientos (`RF-11`), bloqueos institucionales parametrizados (`RD-06`), optimización de flujo en 5 pasos (`RNF-04`) y desacoplamiento por capas (`RNF-06`).
* **Iteración 3 (Escalabilidad Histórica — `Could`):** Pruebas y optimización de almacenamiento masivo hasta $5.000$ mascotas y $10.000$ actas (`RNF-05`).