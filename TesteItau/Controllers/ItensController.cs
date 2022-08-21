using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteItau.Model;
using TesteItau.Repositories;

namespace TesteItau.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItensController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        public ItensController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        
        [HttpGet]
        private async Task<IEnumerable<Item>> GetItens()
        {
            return await _itemRepository.Get();
        }
        private int getUltimoId()
        {
            if (GetItens().Result.Count() == 0) return 0;
            else return GetItens().Result.ToList().Last().Id;
        }
        /*
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItens(int id)
        {
            return await _itemRepository.Get(id);
        }
        */
        [HttpGet("[action]")]
        public async Task<ActionResult<object>> GetItemFila()
        {
            var idItemRetornarERemover = getUltimoId();
            var retorno = await _itemRepository.Get(idItemRetornarERemover);
            if (retorno != null)
            {
                await _itemRepository.Delete(idItemRetornarERemover);
                return new JSonItem { moeda = retorno.Moeda, data_inicio = retorno.DataInicio, data_fim = retorno.DataFim };
            }
            else
                return "NAO EXISTE MOEDA";
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<string>> AddItemFila([FromBody] List<JSonItem> jsonItens)
        {
            if (jsonItens.Count() > 0)
            {
                int itensGravados = 0;
                string logErros = string.Empty;
                foreach (var jsonItem in jsonItens)
                {
                    if ((jsonItem.moeda != string.Empty) && (jsonItem.data_inicio<=jsonItem.data_fim))
                    {
                        Item item = new Item() { Id = getUltimoId() + 1, Moeda = jsonItem.moeda, DataInicio = jsonItem.data_inicio, DataFim = jsonItem.data_fim };
                        var newItem = await _itemRepository.Create(item);
                        itensGravados++;
                    }
                    else
                        logErros = " Uma (ou mais) moeda está em branco ou um início está com termino posterior ao fim";
                }
                return itensGravados + " itens gravados com sucesso!"
                    + logErros;
            }
            else
                return "não existem itens para adicionar";

        }
        /*
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemToDelete = await _itemRepository.Get(id);

            if (itemToDelete == null)
                return NotFound();

            await _itemRepository.Delete(itemToDelete.Id);
            return NoContent();


        }

        [HttpPut]
        public async Task<ActionResult> PutItens(int id, [FromBody] Item item)
        {
            if (id != item.Id)
                return BadRequest();

            await _itemRepository.Update(item);

            return NoContent();
        }
        */
    }
}
