using prjShanLiang.Models;

namespace prjShanLiang.ViewModels
{
    public class CCreateAdminAccountViewModel
    {
        public Admin Admin { get; set; }

        public int AdminId { get; set; }

        public string? AdminName { get; set; }

        public string? Password { get; set; }

        public string? Password2 { get; set; }

        public int? IdentificationId { get; set; }
    }
}
