using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReversiRestApi.Interfaces;

namespace ReversiRestApi.Controllers
{
    [Route("api/Spel")]
    [ApiController]
    public class SpelController : ControllerBase
    {
        private readonly ISpelRepository iRepository;
        public SpelController(ISpelRepository repository) 
        { 
            iRepository = repository; 
        }

        // GET api/spel        
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            //iRepository.GetSpellen().Find(spel => spel.Speler2Token == null).Omschrijving;
            List<string> omschrijving = new List<string>();

            foreach (var item in iRepository.GetSpellen())
            {
                if (item.Speler2Token == null)
                {
                    omschrijving.Add(item.Omschrijving);
                }
            }
            return omschrijving;

        }
        // ...    
    }
}
