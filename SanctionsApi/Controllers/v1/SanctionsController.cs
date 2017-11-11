using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace SanctionsApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class SanctionsController : Controller
    {
        private SanctionsApi.Data.WHOLE _dataSource;
        public SanctionsApi.Data.WHOLE DataSource
        {
            get
            {
                if (null == _dataSource)
                {
                    var xml = new XmlSerializer(typeof(SanctionsApi.Data.WHOLE));
                    var xmlStream = new FileStream("Data/eu-sanctions-global.xml", FileMode.Open);
                    _dataSource = xml.Deserialize(xmlStream) as SanctionsApi.Data.WHOLE;
                }
                return _dataSource;
            }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            var sampleList = DataSource.ENTITY.Where(a => a.Type == "E")
                .SelectMany(a =>
                {
                    var list1 = a.Items.Select(b => b as Data.WHOLEENTITYNAME).Where(b=> null != b).ToList();
                    var list3 = list1.Where(b => null != b && !string.IsNullOrEmpty(b.WHOLENAME)).Select(b => b.WHOLENAME);
                    return list3;
                }).ToList();
            
            return sampleList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
