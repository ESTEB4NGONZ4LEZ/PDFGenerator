
using API.Dto.Compra;
using Dominio.Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace API.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public EmailService(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        } 

        public void SendEmail(CompraDto data)
        {
            var cliente = _unitOfWork.Clientes.Find(x => x.Id == data.IdCliente).FirstOrDefault();
            var producto = _unitOfWork.Productos.Find(x => x.Id == data.IdProducto).FirstOrDefault();

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:Username").Value));
            email.To.Add(MailboxAddress.Parse(cliente.Email));
            email.Subject = $"{cliente.Nombres} esta es tu facutra de compra {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";

            BodyBuilder builder = new();

            MemoryStream memoryStream = new();

            PdfWriter writer = new(memoryStream);
            PdfDocument pdf = new(writer);
            pdf.AddNewPage(PageSize.A5);
            Document document = new(pdf);

            string imagePath = "C:/Users/Esteban/Documents/JS/YeimyLu/img/logo.png";
            ImageData imageData = ImageDataFactory.Create(imagePath);
            Image image = new(imageData);
            image.SetWidth(50);
    
            Paragraph header = new();
            header.Add(image).SetTextAlignment(TextAlignment.CENTER);

            Paragraph marginButton = new("");
            
            document.Add(header);
            document.Add(marginButton);

            Paragraph titulo = new Paragraph("BABALU - FACTURA DE VENTA")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18);

            document.Add(titulo);

            Table table = new(2);
            table.SetWidth(UnitValue.CreatePercentValue(100));

            table.AddCell("Nombre:").SetFontSize(8);
            table.AddCell(cliente.Nombres).SetFontSize(8);

            table.AddCell("Apellidos:").SetFontSize(8);
            table.AddCell(cliente.Apellidos).SetFontSize(8);

            table.AddCell("Fecha").SetFontSize(8);
            table.AddCell(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")).SetFontSize(8);

            table.AddCell("Referencia Producto:").SetFontSize(8);
            table.AddCell(producto.Referencia).SetFontSize(8);

            table.AddCell("Producto:").SetFontSize(8);
            table.AddCell(producto.Nombre).SetFontSize(8);

            table.AddCell("Precio:").SetFontSize(8);
            table.AddCell(producto.PrecioVenta.ToString() + "$").SetFontSize(8); // Formatear como moneda

            table.AddCell("Cantidad:").SetFontSize(8).SetFontSize(8);
            table.AddCell(data.Cantidad.ToString()).SetFontSize(8);

            table.AddCell("Total:").SetFontSize(8);
            table.AddCell((producto.PrecioVenta * data.Cantidad).ToString() + "$").SetFontSize(8);

            document.Add(table);

            document.Close();

            MimePart pdfAttachment = new("application", "pdf")
            {
                Content = new MimeContent(new MemoryStream(memoryStream.ToArray()), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                FileName = $"FACTUA {cliente.Nombres.ToUpper()} {data.Fecha}"
            };

            builder.Attachments.Add(pdfAttachment);

            email.Body = builder.ToMessageBody();

            var smtp = new SmtpClient();

            smtp.Connect
            (
                _config.GetSection("Email:Host").Value,
                Convert.ToInt32(_config.GetSection("Email:Puerto").Value),
                SecureSocketOptions.StartTls
            );

            smtp.Authenticate
            (
                _config.GetSection("Email:Username").Value,
                _config.GetSection("Email:Password").Value
            );

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}