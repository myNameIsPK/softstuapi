using System.ComponentModel;

namespace SoftStuApi.Models
{
    public class UserData
    {
        public int Id { get; set; }
        [DefaultValue(false)]
        public bool Blocked { get; set; }
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
    }
}