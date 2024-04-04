# PersonManagementSystem

# Diseño de la API

### **Arquitectura y Diseño**

El proyecto sigue una arquitectura de múltiples capas que separa claramente la lógica de negocio, el acceso a datos, y la presentación (API REST). Esta separación de preocupaciones facilita la mantenibilidad, la escalabilidad y la posibilidad de realizar pruebas unitarias/integración de manera aislada.

- **Capa de Dominio**: Define las entidades **`Persona`** y **`Factura`**, que son los objetos de negocio principales de la aplicación.
- **Capa de Acceso a Datos**: Implementa el patrón Repository (**`IPersonasRepository`** y **`IFacturasRepository`**) para abstraer y encapsular todo el acceso a la base de datos, permitiendo una mayor flexibilidad y desacoplamiento del almacenamiento de datos.
- **Capa de Servicios o Aplicación**: Contiene la lógica de negocio (no implementada directamente en los controladores de la API) y utiliza los repositorios para interactuar con la base de datos. Es aquí donde se realizarían validaciones, operaciones complejas y transformaciones de datos.
- **API REST (Capa de Presentación)**: Expone la funcionalidad del sistema a través de HTTP, permitiendo que clientes externos interactúen con la aplicación mediante llamadas HTTP. Los controladores de la API delegan operaciones específicas a la capa de servicios y devuelven respuestas formateadas.

### **Interfaces y Clases Abstractas**

- **`IPersonasRepository`**: Define las operaciones disponibles para interactuar con personas en la base de datos, como encontrar, agregar o eliminar personas.
- **`IFacturasRepository`**: Similar a **`IPersonasRepository`**, pero enfocado en las operaciones relacionadas con facturas.

Estas interfaces promueven el principio de inversión de dependencia, permitiendo que la capa de servicios opere sobre abstracciones en lugar de implementaciones concretas. Esto facilita la sustitución de las implementaciones reales, por ejemplo, para pruebas unitarias, donde se podrían usar mocks de los repositorios.

### **Endpoints de la API**

La API expone varios endpoints para operaciones relacionadas con **`Personas`** y **`Facturas`**:

- **Personas**:
    - **`GET /Directorio/personas/{Identification}`**: Busca una persona por su identificación.
    - **`GET /Directorio/personas`**: Obtiene todas las personas.
    - **`POST /Directorio/personas`**: Agrega una nueva persona.
    - **`DELETE /Directorio/personas/{Id}`**: Elimina una persona por su ID.
- **Facturas**:
    - **`GET /Facturas/{PersonaId}`**: Obtiene todas las facturas asociadas a una persona.
    - **`POST /Facturas`**: Agrega una nueva factura.

### **Razones Detrás del Diseño**

El uso de interfaces y clases abstractas para definir repositorios proporciona una capa de abstracción entre la lógica de negocio y el acceso a datos. Esto no solo facilita el cambio de los mecanismos de persistencia en el futuro sin afectar la lógica de negocio, sino que también mejora la testabilidad del código.

La arquitectura elegida permite escalar y mantener la aplicación más fácilmente, ya que cada capa tiene responsabilidades bien definidas. Además, el modelo de respuesta estándar (**`ApiResponse<T>`**) utilizado en los endpoints de la API asegura que todas las respuestas sigan un formato consistente, mejorando la experiencia del desarrollador y facilitando la integración con clientes de la API.