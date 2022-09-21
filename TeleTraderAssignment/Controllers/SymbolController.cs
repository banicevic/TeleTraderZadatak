using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeleTraderAssignment.Controllers
{
    //[Route("api/[controller]")]
    [Route("symbols")]
    [ApiController]
    public class SymbolController : ControllerBase
    {
        static List<Symbol> symbolList;
        static Data data;
        static string xmlFileLocation = "cryptos.xml";
        static SymbolController()
        {
            XElement xmlDoc = XElement.Load(xmlFileLocation);
            symbolList = xmlDoc.Elements("Symbol").Select(x =>
              new Symbol
              {
                  id = x.Attribute("id").Value,
                  name = x.Attribute("name").Value,
                  ticker = x.Attribute("ticker").Value
              }).ToList();
            data = new Data(symbolList);
        }
        [HttpGet("all")]
        public IActionResult getAll() {
            var values= data.getValues().Result;
            string[] temp = values.Remove(values.Length-2).Substring(2).Split("],[");
            foreach (string s in temp)
            {
                data.fillValues(s);
            }
            return Ok(symbolList);
        }
        
        [HttpGet("quotes")]
        public IActionResult getFiltered([FromQuery] List<string> ids)
        {
            if (ids.Count==0)
            {
                return BadRequest();
            }
            var values = data.getValues(ids).Result;
            string[] temp = values.Remove(values.Length - 2).Substring(2).Split("],[");
            List<Symbol> tempSymbolList = new List<Symbol>();
            foreach (string s in temp)
            {
                tempSymbolList.Add(data.fillValues(s));
            }
            return Ok(tempSymbolList);
        }        
    }
}
