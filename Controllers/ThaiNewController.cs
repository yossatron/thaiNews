using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using thaiNews.Models;

namespace thaiNews.Controllers {
    [ApiController]
    [Route("thainew")]
    public class ThaiNewController : ControllerBase {

        private HttpClient httpClient = new HttpClient();

        [HttpGet]
        public async Task<ActionResult<object>> Get() {
            var response = await httpClient.GetAsync("https://www.posttoday.com/rss/src/breakingnews.xml");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response.Content.ReadAsStringAsync().Result);
            string jsonText = JsonConvert.SerializeXmlNode(doc["rss"]["channel"]);
            var result = JsonConvert.DeserializeObject<Rss>(jsonText);
            return result;
        }
    }
}