# Modelo Textual de Casos de Uso — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE07 — Modelado de Casos de Uso, Historias de Usuario y Priorización  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Delimitación del Sistema y Convenciones

El modelo de casos de uso representa las interacciones funcionales entre los actores del refugio y **AffiniPet** en las estaciones de cómputo de atención presencial. El **límite del sistema** engloba la interfaz de escritorio en Windows Forms, las reglas de negocio, el motor ponderado de compatibilidad y la base de datos relacional local (componente interno de persistencia). La Entidad Municipal / Regulatoria permanece en la matriz de interesados sin interfaz informática directa en esta versión.

---

## 2. Identificación de Actores

| Actor | Tipo | Descripción |
| :--- | :---: | :--- |
| **Administrador del Refugio** | Persona | Usuario directo con privilegios administrativos que gestiona cuentas de encargados, administra el inventario de mascotas y caniles/gateras, y transcribe la información clínica validada por el veterinario. |
| **Encargado de Ventanilla** | Persona | Usuario directo principal de la atención presencial: registra el perfil del adoptante, ejecuta el motor de compatibilidad, explora caniles habilitados y formaliza el Acta de Adopción. |
| **Médico Veterinario** | Persona | Participante de apoyo clínico en `CU-04`: aporta y valida vacunación, esterilización y perfil etológico mediante el circuito administrativo autorizado, sin operar credenciales propias en la interfaz. |
| **Ciudadano Adoptante** | Persona | Participante de apoyo presencial en `CU-05` y `CU-08`: suministra sus datos socioeconómicos durante la entrevista y expresa su aceptación o desistimiento informado ante el Encargado de Ventanilla. |
| **Impresora local** | Sistema externo | Dispositivo periférico conectado a la estación de ventanilla que recibe la orden de impresión en formato A4 del Acta de Adopción desde `CU-08`. |

---

## 3. Listado de Casos de Uso

En la siguiente tabla se especifican los **8 casos de uso** del sistema, trazados directamente hacia los requerimientos refinados aprobados en `PPE06`:

| ID | Nombre | Actor principal | Requerimientos asociados |
| :--- | :--- | :--- | :--- |
| **CU-01** | Iniciar sesión en estación de ventanilla | Encargado de Ventanilla / Administrador del Refugio | `RF-02`, `RF-03`, `RNF-02`, `RNF-03` |
| **CU-02** | Gestionar credenciales de encargados de ventanilla | Administrador del Refugio | `RF-01`, `RNF-02` |
| **CU-03** | Registrar y actualizar mascota en canil o gatera | Administrador del Refugio | `RF-04`, `RNF-05` |
| **CU-04** | Registrar y actualizar ficha clínica y etológica | Administrador del Refugio *(apoyo: Médico Veterinario)* | `RF-05`, `RD-01`, `RD-02`, `RD-03` |
| **CU-05** | Registrar y actualizar perfil socioeconómico del adoptante | Encargado de Ventanilla *(apoyo: Ciudadano Adoptante)* | `RF-07`, `RD-06`, `RNF-04` |
| **CU-06** | Evaluar compatibilidad entre adoptante y mascota | Encargado de Ventanilla | `RF-06`, `RF-08`, `RF-09`, `RD-04`, `RD-05`, `RNF-01`, `RNF-06` |
| **CU-07** | Explorar caniles y gateras compatibles y ficha clínica | Encargado de Ventanilla | `RF-10`, `RF-11`, `RNF-01`, `RNF-04` |
| **CU-08** | Confirmar adopción y emitir Acta de Adopción | Encargado de Ventanilla *(apoyo: Ciudadano Adoptante, Impresora local)* | `RF-12`, `RD-06`, `RD-07`, `RD-08`, `RNF-07`, `RNF-08` |

---

## 4. Relaciones Estructurales entre Casos de Uso

| Caso de Uso Origen | Relación UML | Caso de Uso Destino | Justificación Técnica y Operativa |
| :--- | :---: | :--- | :--- |
| **CU-07** | `<<include>>` | **CU-06** | Para habilitar visualmente los caniles o gateras aptos en el mapa, el sistema requiere ejecutar previamente el cálculo y filtrado de compatibilidad (`C >= 70%`). |
| **CU-08** | `<<include>>` | **CU-06** | Antes de confirmar la adopción, el sistema revalida los parámetros actuales de compatibilidad para garantizar que la elegibilidad siga vigente. |
| **CU-08** | `<<extend>>` | **CU-07** | Tras explorar un canil compatible y revisar su ficha clínica en `CU-07`, el adoptante puede solicitar formalizar la entrega (`punto de extensión: selección de mascota`) o desistir sin ejecutar `CU-08`. |

---

## 5. Escenarios Detallados de Casos de Uso

### 5.1. Escenario Detallado: `CU-06` — Evaluar compatibilidad entre adoptante y mascota

* **Identificador y Nombre:** `CU-06` — Evaluar compatibilidad entre adoptante y mascota.
* **Actor principal:** Encargado de Ventanilla.
* **Requerimientos asociados:** `RF-06`, `RF-08`, `RF-09`, `RD-04`, `RD-05`, `RNF-01`, `RNF-06`.

#### Precondiciones
1. El Encargado de Ventanilla mantiene una sesión activa y autorizada (`CU-01`) con el catálogo institucional habilitado (`RF-03`).
2. El perfil socioeconómico del ciudadano adoptante ha sido registrado y validado sin restricciones institucionales vigentes (`CU-05`, `RF-07`, `RD-06`).
3. Existen mascotas en estado *Disponible* con vacunación vigente (`RD-01`), esterilización validada (`RD-02`) y perfil etológico completo (`RD-03`).

#### Flujo principal
1. El Encargado de Ventanilla selecciona el perfil del adoptante registrado y presiona la opción **Evaluar Compatibilidad**.
2. El sistema verifica que los datos obligatorios del adoptante estén completos y que no posea bloqueos institucionales vigentes (`RD-06`).
3. El sistema consulta el catálogo local recuperando exclusivamente las mascotas en estado *Disponible* con ficha clínica y sanitaria validada (`RF-06`).
4. Para cada mascota elegible, el motor de compatibilidad calcula los factores de vivienda ($V$), tiempo ($T$), presupuesto ($P$) y entorno familiar ($F$), acotando $T$ y $P$ mediante $\min(1, \text{disponible}/\text{requerido})$ con requisitos $> 0$, y aplica la ecuación $C = 30V + 30T + 20P + 20F$ redondeada a dos decimales (`RF-08`, `RD-04`).
5. El sistema filtra los resultados descartando cualquier mascota con incompatibilidad crítica familiar o con puntaje $C < 70\%$ (`RF-09`, `RD-05`).
6. El sistema despliega en pantalla la lista de mascotas compatibles ($C \ge 70\%$) ordenada de mayor a menor porcentaje de afinidad (desempatando por identificador de mascota ascendente) en un tiempo máximo de 2 segundos (`RNF-01`).
7. El Encargado de Ventanilla socializa los resultados con el ciudadano y avanza hacia la exploración física de caniles (`CU-07`).

#### Flujos alternativos
* **A1 — Ninguna mascota alcanza el umbral mínimo ($C < 70\%$) o existe incompatibilidad crítica (Paso 5):**  
  El sistema descarta todas las candidatas no aptas y despliega en pantalla el mensaje normalizado: *"No se encontraron mascotas compatibles con el perfil registrado"*. El caso de uso finaliza sin habilitar opciones de adopción.
* **A2 — Corrección de datos de entrevista (Paso 6):**  
  Si el ciudadano aclara un dato real omitido durante la entrevista (por ejemplo, disponibilidad de patio o presupuesto mensual), el Encargado regresa a `CU-05`, actualiza el perfil y ejecuta nuevamente el cálculo desde el Paso 2.
* **A3 — Desistimiento del ciudadano tras revisar porcentajes (Paso 7):**  
  El ciudadano decide no continuar con el trámite. El Encargado cierra la consulta sin generar acta ni modificar el estado *Disponible* de ninguna mascota (`RF-11`).

#### Postcondiciones
* **En éxito:** Se dispone en memoria de la lista ordenada de mascotas compatibles ($C \ge 70\%$) vinculada al adoptante en atención, lista para habilitar el mapa interactivo de caniles (`CU-07`).
* **En ausencia de coincidencias o desistimiento:** No se modifica el estado de ninguna mascota en la base de datos local.

---

### 5.2. Escenario Detallado: `CU-08` — Confirmar adopción y emitir Acta de Adopción

* **Identificador y Nombre:** `CU-08` — Confirmar adopción y emitir Acta de Adopción.
* **Actor principal:** Encargado de Ventanilla.
* **Actores de apoyo:** Ciudadano Adoptante (presencial) e Impresora local (sistema externo).
* **Requerimientos asociados:** `RF-12`, `RD-06`, `RD-07`, `RD-08`, `RNF-07`, `RNF-08` (incluye `CU-06`).

#### Precondiciones
1. El Encargado de Ventanilla posee sesión activa y autorizada (`CU-01`).
2. El adoptante ha seleccionado una mascota compatible ($C \ge 70\%$) en el mapa interactivo de caniles (`CU-07`) y ha revisado su ficha clínica y gasto mensual estimado.
3. Consta la aceptación expresa del ciudadano adoptante y la verificación de aprobación institucional del refugio.

#### Flujo principal
1. Desde la vista de detalle de la mascota compatible en `CU-07`, el Encargado presiona el botón **Confirmar Adopción**.
2. El sistema ejecuta una revalidación de compatibilidad (`<<include>> CU-06`) y verifica nuevamente que el adoptante no presente restricciones institucionales vigentes (`RD-06`).
3. El sistema inicia una transacción atómica ACID (`RNF-08`) y revalida en tiempo real en la base de datos local que la mascota seleccionada continúe en estado *Disponible* (`RF-12`).
4. El sistema registra la adopción, genera el **Acta de Adopción** con identificador único, datos del adoptante, datos de la mascota, porcentaje de compatibilidad obtenido y metadatos de auditoría (identificador y nombre del encargado en sesión, rol operativo, fecha y hora exacta) (`RD-08`).
5. Dentro de la misma transacción, el sistema actualiza el estado operativo de la mascota de *Disponible* a *Adoptado* y libera el canil o gatera correspondiente (`RF-12`).
6. El sistema confirma la transacción (*commit*), protege el acta como registro histórico inmutable (`RD-07`) y despliega en pantalla la vista previa formateada en hoja A4 (`RNF-07`).
7. El Encargado envía el documento a la **Impresora local** para su firma física en ventanilla.

#### Flujos alternativos
* **A1 — Desistimiento informado antes de confirmar (Paso 1):**  
  Antes de ejecutar la confirmación, el ciudadano manifiesta su deseo de no adoptar. El Encargado registra el desistimiento informado (`RF-11`), el sistema no genera ningún acta y la mascota conserva su estado *Disponible*.
* **A2 — Conflicto de concurrencia: mascota adoptada simultáneamente en otra ventanilla (Paso 3):**  
  Al revalidar el estado en la base de datos, el sistema detecta que otra estación ya cambió el estado de la mascota a *Adoptado*. El sistema revierte la operación (*rollback*, `RNF-08`), impide generar un acta duplicada y notifica al Encargado que la mascota ya no se encuentra disponible.
* **A3 — Fallo de comunicación con la impresora local (Paso 7):**  
  Si la impresora física presenta falta de papel o desconexión, el sistema muestra una alerta informativa permitiendo reimprimir el acta desde el historial sin duplicar el registro de adopción ni alterar el estado *Adoptado* ya confirmado.

#### Postcondiciones
* **En éxito:** Existe un Acta de Adopción inmutable y auditada en la base de datos local (`RD-07`, `RD-08`), la mascota queda en estado *Adoptado* y deja de aparecer en futuras evaluaciones de compatibilidad.
* **En concurrencia o fallo de guardado:** Se garantiza reversión total sin registros parciales ni inconsistencias en el inventario (`RNF-08`).