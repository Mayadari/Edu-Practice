namespace WPF_LoginForm.Models
{
    public class UserAccountModel
    {
        public string Username { get; set; }

        public int AccessLevel { get; set; }
        public string DisplayName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
