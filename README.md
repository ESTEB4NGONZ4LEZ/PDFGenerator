## GENERADOR DE PDF

En este proyecto estare explicando el proceso a seguir para generar archivos PDF con datos de nuestra base de datos. Todo el proceso para crear el generador de PDF se esta desarrollando en un proyecto webapi de 4 capas, puede ser, sin nungun problema aplicado en un proyecto de 3 capas o en un unico preyecto webapi.

### REQUERIMIENTOS 

Para poder hacer nuestro generador de PDF estaremos usando dos librerias:

**itext7** : Es utilizada para manipular y crear ducumentos PDF 

**itext7.bouncy-castle-adapter** : Hace parte ed IText7, proporciona una adaptacion para la integracion de la libreria Bouncy Castle ( Biblioteca que ofrece funciones criptograficas y de seguridad para archivos PDF ).

### PROCEDIMIENTO

En donde podemos guardar nuestros PDF?

1. **Carpeta dentro del Proyecto:** Esta opcion consiste en crear una carpeta dentro de nuestro proyecto y cuando estemos creando nuestro archivo pdf podemos pasarle la ruta de esa carpeta para que todos los archivos generados se guarden alli.

![StringPath](/Documentacion/stringPath.png)

2. **Guardandola en un espacio de Memoria:** Podemos crear un espacio de memoria en donde guardamos el archivo PDF y luego lo podemos usar por ejemplo para enviar el PDF al front.

![MemoryStream](/Documentacion/memoryStream.png)

En este proyecto estaremos implementando la de guardarlo en un espacio de memoria.

Como crear el PDF?

La estructura para crear un PDF con itext7 es bastante sencilla, siempre debemos crear la instancia de estas 3 clases 

![CreacionPDF](/Documentacion/creacionPDF.png)

**PdfWriter** : Es responsable de escribir contenido en un documento PDF nuevo o existente. Recibe como parametro el memoryStream o la ruta de la carpeta

**PDFDocument** : Se utiliza para representar y controlar el documento PDF en sí. Recibe como parametro la instancia del PdfWriter.

**Document** : Se utilizada para crear y manipular contenido dentro de un documento PDF. Recibe como parametro el PdfDocument.

Despues de estas tres instancias crearemos todo el contenido de nuestro archivo PDF.

Como creamos y estructuramos los datos en nuestro archivo PDF?

Creacion de parrafos: para crear nuestros parrafos debemos instanciar la clase Paragrph esta clase recibe como parametro en contenido del parrafo, podemos asignarle tamanio de letra, alineamiento, fuente y demas propiedades.

![Parrafos](/Documentacion/parrafos.png)

Creacion de tablas: Las tablas las estaremos utilizando mucho para la estructuracion de nuestros datos, para crear una tabla debemos instanciar la clase Table que recibe como parametro un numero que seria la cantidad de columnas de nuestra tabla, a la tabla le podemos asignar propiedades como el que se adapte al ancho de la pagina. etc.

![Tablas](/Documentacion/tablas.png)

Insertar encabezados a nuestra tabla: Para crear encabezados en nuestra tabla debemos instanciar la clase Cell, esta clase nos sirve para definir y formatear celdas en una tabla dentro de un documento PDF. Para agregar nuestro valor como encabezado de la tabla demos agregarlo usando la propiedad AddHeaderCell y pasandole como parametro nuestra instancia de Cell.

![TablasHeader](/Documentacion/tablasHeader.png)

Insercion de contenidos a nuestra tabla : Para ingresar datos a nuestra tabla es demebos llamar a nuestra Table y asignarle la propiedad AddCell en donde como parametro le pasaremos una nueva instancia de la Clase Cell y usaremos la propiedad Add e instanciaremos un parrafo con el contenido. Es importante recalcar que hay que crear tantas celdas como headers a la hora de estructurar nuestros datos, ademas podemos asignarle propiedades como el tamanio de letra, fuente. etc.

![TablasData](/Documentacion/tablasData.png)

Es importante recalcar que cada elemento que creemos debe ser agregado al documento, y lo mismo para con las tablas, cada elemento que pertenezca a nuestra tabla debe agregarse a ella para que se muestre en el documento PDF.

![agregarElementosDocumento](/Documentacion/agregarElementosDocumento.png)

Cuando terminemos de hacer la estructuracion de nuestros datos en nuestro archivo PDF es necesario cerrar el documento:

![documentClose](/Documentacion/documentClose.png)

Si estamos usando la opcion de crear el archivo PDF en una carpeta con esto seria suficiente para crear el archivo, pero si estamos usando la opcion del espacion en memoria para enviarlo a un front es necesario seguir los siguientes pasos: 

Enviaremos dos Headers: El primero sera el que le dira al servidor que enviaremos un archivo descargable y el segundo le dira que enviaremos un archivo PDF.

![Headers](/Documentacion/headers.png)

Y por ultimo devolvemos el archivo PDF como respuesta a una solicitud HTTP, con File crearemos una respuesta http con el tipo de archivo especificado, luego tomaremos el contenido del memoryStream y lo convertiremos en un arreglo de bytes.

![ReturnFile](/Documentacion/returnFile.png)

A continuacion veremos un ejemplo de un controlador en donde creamos un archio PDF para un reporte de ventas en donde aplicamos todo los dicho anteriormente y mas formas de aplicarlo:

![ReporteVentas](/Documentacion/reporteVentas.png)

ES IMPORTANTE ACLARAR QUE HAY MUCHA MAS DOCUMENTACION SOBRE ESTA LIBRERIA Y DISTINTAS FORMAS DE APLICARLO, SE INVITA A REVISAR MAS DOCUMENTACION E IMFORMARSE MUCHOS MAS SOBRE ESTA HERRAMIENTA.



