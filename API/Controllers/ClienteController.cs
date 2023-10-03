
using API.Dto.Cliente;
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
    public class ClienteController : BaseApiController
    {
        public ClienteController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<ClienteDto>> Get()
        {
            var registros = await _unitOfWork.Clientes.GetAllAsync();
            return _mapper.Map<List<ClienteDto>>(registros);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ClienteDto> GetById(int id)
        {
            var registros = await _unitOfWork.Clientes.GetByIdAsync(id);
            return _mapper.Map<ClienteDto>(registros);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Post(ClienteDto data)
        {
            if(data == null) return BadRequest();
            var registro = _mapper.Map<Cliente>(data);
            _unitOfWork.Clientes.Add(registro);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Post), new {id = registro.Id}, registro);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto data)
        {
            if(data == null) return NotFound();
            var registro = _mapper.Map<Cliente>(data);
            registro.Id = id;
            _unitOfWork.Clientes.Update(registro);
            await _unitOfWork.SaveAsync();
            return data;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var registro = await _unitOfWork.Clientes.GetByIdAsync(id);
            if(registro == null) return NotFound();
            _unitOfWork.Clientes.Remove(registro);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}