# Matriz de Interesados (Stakeholders) — Sistema AffiniPet

**Proyecto:** AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad  
**Entregable:** PPE03 — Configuración del Proyecto, Visión y Matriz de Interesados  
**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Institución:** Escuela Politécnica Nacional (EPN)  
**Fecha:** 22 de noviembre de 2026  

---

## 1. Criterio de Clasificación de Interesados

Para identificar adecuadamente a los involucrados en **AffiniPet**, se distingue entre el concepto general de interesado (*stakeholder*) y usuario directo del software:
* **Interno / Externo:** Un interesado **interno** pertenece a la estructura operativa del refugio de animales, mientras que uno **externo** se relaciona con sus procesos o resultados desde fuera de la institución.
* **Directo / Indirecto:** La participación **directa** corresponde a quienes operan físicamente la interfaz del sistema en las estaciones de cómputo del refugio; la **indirecta** corresponde a quienes aportan información clínica, participan en el trámite presencial o establecen regulaciones sin operar directamente la aplicación.

---

## 2. Tabla de Matriz de Interesados

| Interesado | Clasificación | Interés | Influencia | Necesidad principal | Posibles conflictos |
| :--- | :--- | :---: | :---: | :--- | :--- |
| **Administrador del Refugio** | Interno / Directo | Alto | Alta | Controlar el inventario, ubicación y disponibilidad de las mascotas; crear, habilitar y revocar credenciales de los encargados; supervisar la trazabilidad de las actas emitidas. | Prioriza el control estricto de accesos, la completitud de los registros y las restricciones institucionales frente a la rapidez operativa solicitada por ventanilla. |
| **Encargado de Ventanilla** | Interno / Directo | Alto | Media | Registrar de forma ágil el perfil del adoptante, visualizar el porcentaje de compatibilidad, explorar los caniles habilitados y formalizar el Acta de Adopción mediante formularios claros. | Prefiere formularios cortos y trámites rápidos en horas de alta afluencia, lo que choca con la cantidad de validaciones sanitarias y socioeconómicas exigidas por Administración y Clínica. |
| **Médico Veterinario** | Interno / Indirecto | Alto | Media | Garantizar que ninguna mascota sea evaluada ni entregada sin vacunación vigente, esterilización validada y perfil etológico completo, evitando errores por manipulación no clínica. | Exige bloquear cualquier mascota con ficha médica incompleta y calibrar estrictamente los parámetros de compatibilidad, mientras ventanilla y adoptante buscan agilizar la entrega. |
| **Ciudadano Adoptante** | Externo / Indirecto | Alto | Baja | Conocer opciones de mascotas acordes a su vivienda, tiempo y presupuesto, revisar su estado clínico real y completar la adopción presencial sin esperas innecesarias. | Puede insistir en adoptar una mascota específica por preferencia estética aunque su puntaje de compatibilidad sea inferior al $70\%$, o desistir al conocer los costos y cuidados clínicos. |
| **Entidad Municipal / Regulatoria** | Externo / Indirecto | Medio | Alta | Contar con evidencia institucional y registros trazables de las adopciones responsables y el cumplimiento de normas de bienestar animal cuando se realicen inspecciones. | Demanda conservación histórica inmutable y respaldo documental detallado de cada entrega, lo que incrementa la carga de validación administrativa del refugio. |

---

## 3. Resolución de Conflictos

Las diferencias de intereses identificadas se gestionarán mediante sesiones de revisión y negociación conjunta entre el **Administrador del Refugio**, el **Encargado de Ventanilla** y el **Médico Veterinario**, incorporando las observaciones recogidas en la atención al **Ciudadano Adoptante** y los lineamientos de la **Entidad Municipal / Regulatoria**. Durante estas mesas de trabajo, el equipo de desarrollo expondrá cada conflicto junto con su impacto sobre el bienestar animal, la calidad de los datos y los tiempos de atención presencial.

Como acuerdo marco, se priorizará la integridad clínica y el cumplimiento del umbral de compatibilidad ($C \ge 70\%$) por encima de la rapidez operativa, pero automatizando en segundo plano los filtros sanitarios y el cálculo ponderado para que el Encargado de Ventanilla no pierda tiempo revisando carpetas físicas. Asimismo, dado que el Médico Veterinario y el Ciudadano Adoptante no poseen credenciales directas en esta versión, se establecerá un circuito formal de transcripción validada por el Administrador para las fichas clínicas y pantallas claras de consulta en ventanilla para informar al adoptante. Todos los acuerdos y ajustes derivados de estas negociaciones quedarán documentados en los resultados de validación y en el control de versiones del SRS.