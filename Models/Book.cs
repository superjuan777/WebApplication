using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cliente")]
        [JsonProperty("cliente")]
        public object cliente { get; set; }

        [JsonProperty("ciudad")]
        [BsonElement("ciudad")]
        public object ciudad { get; set; }

        [JsonProperty("codigofactura")]
        [BsonElement("codigofactura")]
        public object codigofactura { get; set; }

        [BsonElement("estado")]
        [JsonProperty("estado")]
        public string estado { get; set; }


        [JsonProperty("fechacreacion")]
        [BsonElement("fechacreacion")]
        public object fechacreacion { get; set; }

        [BsonElement("fechapago")]
        [JsonProperty("fechapago")]
        public object fechapago { get; set; }

        [BsonElement("iva")]
        [JsonProperty("iva")]
        public object iva { get; set; }

        [BsonElement("nit")]
        [JsonProperty("nit")]
        public object nit { get; set; }
        
        [BsonElement("por")]
        [JsonProperty("por")]
        public object por { get; set; }

        [BsonElement("pagada")]
        [JsonProperty("pagada")]
        public object pagada { get; set; }

        [BsonElement("retencion")]
        [JsonProperty("retencion")]
        public object retencion { get; set; }

        [BsonElement("subtotal")]
        [JsonProperty("subtotal")]
        public object subtotal { get; set; }

        [BsonElement("totalfactura")]
        [JsonProperty("totalfactura")]
        public int totalfactura { get; set; }
        
        [BsonElement("email")]
        [JsonProperty("email")]
        public object email { get; set; }
        
        [BsonElement("mensaje")]
        [JsonProperty("mensaje")]
        public object mensaje { get; set; }

        public int Exito { get; set; }

        public Book()
        {
            this.Exito = 0;
        }
    }
       
}
