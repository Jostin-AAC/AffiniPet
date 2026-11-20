# AffiniPet — Sistema de Gestión de Adopciones Basada en Compatibilidad

Aplicación de escritorio desarrollada en **C# con Windows Forms (.NET 8)** para la gestión integral y presencial de adopciones en refugios de animales, enfocada en evaluar la compatibilidad entre el perfil socioeconómico del adoptante y las necesidades de cada mascota.

---

## 1. Integrantes del Equipo y Roles

* **Aldas Jostin:** Líder Técnico, Arquitectura de Software e Integración de Repositorio.
* **Olmos Paul:** Analista de Negocio, Elicitación y Priorización de Requerimientos.
* **Castillo Galo:** Modelado UML, Escenarios y Aseguramiento de Calidad (QA).

**Asignatura:** Diseño y Programación de Software (ITID 543)  
**Docente:** Ing. José Naranjo  
**Institución:** Escuela Politécnica Nacional (EPN) — Facultad de Ingeniería de Sistemas  

---

## 2. Problemática Actual del Negocio

Actualmente, los procesos de registro y evaluación de adopción en el refugio se realizan de manera manual mediante fichas físicas en papel y criterio subjetivo en ventanilla. Esta forma de trabajo ocasiona tres problemas operativos críticos:
1. **Demoras en la atención presencial:** La búsqueda manual de animales disponibles y la revisión física de historiales médicos ralentizan el trámite cuando acuden ciudadanos interesados.
2. **Desconocimiento de condiciones clínicas:** Al no contar con una consulta centralizada e inmediata de vacunas, esterilización y cuidados especiales, los adoptantes pueden elegir mascotas sin conocer sus requerimientos reales.
3. **Alta tasa de devoluciones (adopciones fallidas):** Asignar mascotas sin verificar objetivamente si el espacio de vivienda, presupuesto mensual, tiempo libre y entorno familiar del adoptante se ajustan al nivel de energía y tamaño del animal provoca incompatibilidades posteriores y el retorno de las mascotas al refugio.

---

## 3. Solución Propuesta

**AffiniPet** es un sistema de escritorio operado directamente por el personal del refugio en las estaciones de cómputo de ventanilla presencial. La aplicación digitaliza el inventario de caniles/gateras y las fichas clínicas veterinarias, captura el perfil socioeconómico del ciudadano durante la entrevista y ejecuta un **motor algorítmico de compatibilidad** que calcula un puntaje porcentual ($0\%$ a $100\%$). 

El sistema filtra y habilita visualmente en un mapa interactivo únicamente las mascotas que alcanzan un umbral mínimo de afinidad ($\ge 70\%$), bloqueando aquellas que no son compatibles y generando de forma automática el **Acta de Adopción** formal al confirmar la entrega.

---

## 4. Tecnologías Utilizadas

* **Lenguaje de Programación:** C# 12
* **Framework y Plataforma:** .NET 8.0 (Windows Forms App)
* **Entorno de Desarrollo (IDE):** Microsoft Visual Studio Community 2022
* **Persistencia de Datos:** Base de datos relacional local (SQLite / SQL Server LocalDB)
* **Control de Versiones:** Git y GitHub

---

## 5. Estructura del Repositorio

El repositorio está organizado separando el código fuente de la solución de Visual Studio y toda la documentación técnica de la fase de Ingeniería de Requerimientos:

```text
/AffiniPet
│── .gitignore                              # Exclusión de carpetas compiladas (bin/, obj/, .vs/)
│── README.md                               # Descripción general, integrantes, arquitectura y estructura
│── AffiniPet.sln                           # Archivo de solución de Visual Studio (.NET 8)
│── /AffiniPet                              # Código fuente del proyecto Windows Forms en C#
│    ├── AffiniPet.csproj                   # Configuración y dependencias del proyecto .NET 8
│    ├── Program.cs                         # Punto de entrada principal de la aplicación
│    └── Form1.cs / Form1.Designer.cs       # Formulario base de la interfaz de escritorio
└── /Documentacion                          # Documentación de Ingeniería de Requerimientos (SRS)
     ├── /01_Gestion_y_Alcance              # Visión, alcance, restricciones y matriz de interesados
     ├── /02_Adquisicion_e_Interesados      # Plan de elicitación, registros, aclaraciones y conflictos
     ├── /03_Catalogo_y_Refinamiento        # Catálogo maestro (RF, RNF, RD), análisis y viabilidad
     ├── /04_Requerimientos_Verificables    # Fichas individuales refinadas y verificables
     ├── /05_Modelado_y_Priorizacion        # Casos de uso, diagrama UML, historias de usuario y MoSCoW
     ├── /06_Trazabilidad_y_Validacion      # Matriz de trazabilidad, validación y control de versiones
     └── SRS_Final.md                       # Especificación de Requerimientos de Software consolidada