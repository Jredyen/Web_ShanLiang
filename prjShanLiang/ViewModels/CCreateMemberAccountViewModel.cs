using System.ComponentModel;

namespace prjShanLiang.ViewModels
{
    public class CCreateMemberAccountViewModel
    {
        [DisplayName("帳號")]
        public string AccountName { get; set; }
        [DisplayName("手機號碼")]
        public string Memberphone { get; set; }
        [DisplayName("姓名")]
        public string MemberName { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("生日")]
        public DateTime? BrithDate { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
        [DisplayName("密碼")]
        public string AccountPassword { get; set; }
        public string AccountPassword2 { get; set; }
    }
}
