## GENERADOR DE PDF

En este proyecto estare explicando el proceso a seguir para generar archivos PDF con datos de nuestra base de datos. Todo el proceso para crear el generador de PDF se esta desarrollando en un proyecto webapi de 4 capas, puede ser, sin nungun problema aplicado en un proyecto de 3 capas o en un unico preyecto webapi.

### REQUERIMIENTOS 

Para poder hacer nuestro generador de PDF estaremos usando dos librerias:

itext7 : Es utilizada para manipular y crear ducumentos PDF 

itext7.bouncy-castle-adapter : Hace parte ed IText7, proporciona una adaptacion para la integracion de la libreria Bouncy Castle ( Biblioteca que ofrece funciones criptograficas y de seguridad para archivos PDF ).

### PROCEDIMIENTO

En donde podemos guardar nuestros PDF?

En este proyecto implementamos dos formas de guardar y enviar los achivos PDF:

1. Carpeta dentro del Proyecto: Esta opcion consiste en crear una carpeta dentro de nuestro proyecto y cuando estemos creando nuestro archivo pdf podemos pasarle la ruta de esa carpeta para que todos los archivos generados se guarden alli.

![StringPath](/Documentacion/stringPath.png)

2. Guardandola en un espacio de Memoria: