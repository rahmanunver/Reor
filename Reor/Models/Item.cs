using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reor.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Expense { get; set; }
        public string UserId { get; set; }
        public bool IsTime { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
