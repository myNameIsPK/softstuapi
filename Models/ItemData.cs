using System.ComponentModel;

namespace SoftStuApi.Models
{
    public class ItemData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(0)]
        public int BookTotal { get; set; }
        [DefaultValue(5)]
        public int Total { get; set; }
    }
}