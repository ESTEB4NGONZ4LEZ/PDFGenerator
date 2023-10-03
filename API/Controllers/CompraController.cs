
using API.Dto.Compra;
using API.Service;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CompraController : BaseApiController
    {
        private readonly IEmailService _emailService;
        public CompraController(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService) : base(unitOfWork, mapper)
        {
            _emailService = emailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<CompraDto>> Get()
        {
            var registros = await _unitOfWork.Compras.GetAllAsync();
            return _mapper.Map<List<CompraDto>>(registros);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CompraDto> GetById(int id)
        {
            var registros = await _unitOfWork.Compras.GetByIdAsync(id);
            return _mapper.Map<CompraDto>(registros);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(CompraDto data)
        {
            if(data == null) return BadRequest();
            var registro = _mapper.Map<Compra>(data);
            _unitOfWork.Compras.Add(registro);
            await _unitOfWork.SaveAsync();
            _emailService.SendEmail(data);
            return Ok();
        }

        [HttpPost("AddRange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> AddRange(IEnumerable<CompraDto> data)
        {
            IEnumerable<Compra> registros = _mapper.Map<IEnumerable<Compra>>(data);
            _unitOfWork.Compras.AddRange(registros);
            await _unitOfWork.SaveAsync();
            foreach(Compra compra in registros)
            {
                CreatedAtAction(nameof(AddRange),new  {id = compra.Id}, compra);
            }

            return Ok("Registros creados con exito");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompraDto>> Put(int id, [FromBody] CompraDto data)
        {
            if(data == null) return NotFound();
            var registro = _mapper.Map<Compra>(data);
            registro.Id = id;
            _unitOfWork.Compras.Update(registro);
            await _unitOfWork.SaveAsync();
            return data;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var registro = await _unitOfWork.Compras.GetByIdAsync(id);
            if(registro == null) return NotFound();
            _unitOfWork.Compras.Remove(registro);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet("reporteVentas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetReporteVentas()
        {
            int mesActual = DateTime.Now.Month;

            var registros = await _unitOfWork.Compras.ReporteVentaPorMes(mesActual);

            MemoryStream memoryStream = new();

            string path = "C:Ruta/de/la/carpeta";

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


            string[] encabezados = { "Nombre", "Cedula", "Referencia Producto", "Producto", "Fecha Venta", "Cantidad", "Total" };
            string[] meses = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"};

            Table table = new(UnitValue.CreatePercentArray(encabezados.Length));

            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            Paragraph titulo = new Paragraph($"REPORTE VENTAS {meses[mesActual - 1]}").SetFont(font).SetTextAlignment(TextAlignment.CENTER).SetFontSize(12);
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
                table.AddCell(new Cell().Add(new Paragraph(registro.Nombre))).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7);
                table.AddCell(new Cell().Add(new Paragraph(registro.Cedula)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.ReferenciaProducto)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.Producto)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.Fecha.ToString())).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.Cantidad.ToString())).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
                table.AddCell(new Cell().Add(new Paragraph(registro.Total.ToString("N2"))).SetTextAlignment(TextAlignment.CENTER).SetFontSize(7));
            }

            document.Add(table);

            document.Close();

            Response.Headers.Add("Content-Disposition", $"attachment; filename=reporte.pdf");
            Response.Headers.Add("Content-Type", "application/pdf");

            return File(memoryStream.ToArray(), "application/pdf");
        }
    }
}