using System.ComponentModel;

namespace SoftStuApi.Models
{
    public class BookingData
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public int BookerId { get; set; }
        public string ItemName { get; set; }
        [DefaultValue(1)]
        public int DateSlot { get; set; }
        [DefaultValue("08")]
        public string Time { get; set; }
    }
}