# ProyectoProgAvanzadaWebGrupo2
ProyectoProgAvanzadaWebGrupo2

1. Integrantes finales del grupo. A los que se les asignará la nota del proyecto
   
Madrigal Pozo Lester Javier, 
Acuña Perez Sebastian, 
Alpizar Alpizar Mauricio, 
Jimenez Mora Moises Enrique, 

3. Enlace del repositorio si lo subió en GitHub o en algún otro. 
https://github.com/JavierMadrigal22/ProyectoProgAvanzadaWebGrupo2.git

4. Especificación básica del proyecto: 
a. Arquitectura del proyecto (tipos de proyectos que utilizo y contiene el 
programa) 
Arquitectura en capas Model-View-Controller (MVC),  servicios BLL, repositorios DAL con Inyección de Dependencias y separación de responsabilidades. 

b. Libraries o paquetes de nuget utilizados
AutoMapper
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

c. Principios de SOLID y patrones de diseño utilizados 
S – Single Responsibility Principle: cada clase debe tener una única razón para cambiar.
O – Open/Closed Principle: las entidades (clases, módulos) deben estar abiertas a extensión pero cerradas a modificación.
L – Liskov Substitution Principle: las subclases deben poder sustituir a sus superclases sin alterar el comportamiento esperado.
I – Interface Segregation Principle: es mejor tener interfaces específicas y pequeñas que una interfaz “gorda”.
D – Dependency Inversion Principle: los módulos de alto nivel no deben depender de módulos de bajo nivel; ambos deben depender de abstracciones.

Se utilizan patrones como Repository, Service, MVC, Dependency Injection y Adapter (AutoMapper).
