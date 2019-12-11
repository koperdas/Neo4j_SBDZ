using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;
using Neo4j.Driver.V1;
using Neo4jClient.Transactions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Neo4j_SBDZ
{
    public class Travel
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("price")]
        public Int32 Price { get; set; }
    }
}
