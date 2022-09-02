using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.Models
{
    public class AdsM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
    }
}
