namespace Rawaa_Api.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
    }
}
