
using API.Dto.Producto;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductoController : BaseApiController
    {
        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<ProductoDto>> Get()
        {
            var registros = await _unitOfWork.Productos.GetAllAsync();
            return _mapper.Map<List<ProductoDto>>(registros);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ProductoDto> GetById(int id)
        {
            var registros = await _unitOfWork.Productos.GetByIdAsync(id);
            return _mapper.Map<ProductoDto>(registros);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Post(ProductoDto data)
        {
            if(data == null) return BadRequest();
            var registro = _mapper.Map<Producto>(data);
            _unitOfWork.Productos.Add(registro);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Post), new {id = registro.Id}, registro);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto data)
        {
            if(data == null) return NotFound();
            var registro = _mapper.Map<Producto>(data);
            registro.Id = id;
            _unitOfWork.Productos.Update(registro);
            await _unitOfWork.SaveAsync();
            return data;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var registro = await _unitOfWork.Productos.GetByIdAsync(id);
            if(registro == null) return NotFound();
            _unitOfWork.Productos.Remove(registro);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet("reporteProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetReporteProductosxMes()
        {
            int mesActual = DateTime.Now.Month;

            var registros = await _unitOfWork.Productos.GetReporteProductos(mesActual);

            using MemoryStream memoryStream = new();

            PdfWriter writer = new(memoryStream);
            PdfDocument pdf = new(writer);
            Document document = new(pdf);

            string imagePath = "C:/Users/Esteban/Documents/JS/YeimyLu/img/logo.png";
            ImageData imageData = ImageDataFactory.Create(imagePath);
            Image image = new(imageData);
            image.SetWidth(100);

            Paragraph header = new();
            header.Add(image).SetTextAlignment(TextAlignment.CENTER);

            Paragraph marginButton = new("");
            document.Add(header);
            document.Add(marginButton);

            string[] encabezados = { "ID", "REFERENCIA", "NOMBRE", "FECHA", "STOCK", "PRECIO COMPRA", "PRECIO VENTA" };
            string[] meses = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"};

            Table table = new(UnitValue.CreatePercentArray(encabezados.Length));

            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            Paragraph titulo = new Paragraph($"REPORTE PRODUCTOS MES DE  {meses[mesActual - 1]}").SetFont(font).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12);
            document.Add(titulo);

            foreach (string encabezado in encabezados)
            {
                Cell headerCell = new Cell().Add(new Paragraph(encabezado).SetFont(font));
                headerCell.SetTextAlignment(TextAlignment.CENTER);
                headerCell.SetFontSize(8);
                table.AddHeaderCell(headerCell);
            }

            foreach (var registro in registros)
            {
                table.AddCell(new Cell().Add(new Paragraph(registro.Id.ToString()))).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7);
                table.AddCell(new Cell().Add(new Paragraph(registro.Referencia)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.Nombre)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.Stock.ToString())).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.Fecha.ToString())).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.PrecioCompra.ToString())).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.PrecioVenta.ToString("N2"))).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
            }

            document.Add(table);

            document.Close();

            Response.Headers.Add("Content-Disposition", $"attachment; filename=reporte.pdf");
            Response.Headers.Add("Content-Type", "application/pdf");

            return File(memoryStream.ToArray(), "application/pdf");
        }
    }
}