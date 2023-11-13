using Microsoft.AspNetCore.Mvc;
using RestAPI.Extensions;
using RestAPI.Models.Enum;

namespace RestAPI.Controllers
{
    public class EnumController : ControllerBase
    {
        [HttpGet("cardtypes")]
        public IActionResult GetCardTypes()
        {
            var cardTypes = Enum.GetValues(typeof(CardType))
                                .Cast<CardType>()
                                .Select(e => new
                                {
                                    Value = (int)e,
                                    Description = e.GetDescription()
                                });

            return Ok(cardTypes);
        }

        [HttpGet("bandeiras")]
        public IActionResult GetBandeiras()
        {
            var bandeiras = Enum.GetValues(typeof(Bandeira))
                                .Cast<Bandeira>()
                                .Select(e => new
                                {
                                    Value = (int)e,
                                    Description = e.GetDescription()  // Implemente o método GetDescription para obter a descrição do atributo
                                });

            return Ok(bandeiras);
        }
    }
}
