namespace IdentityCoreFist.Model
{
    public class RoleEditModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public string [] IdToAdd { get; set; }
        public string [] IdToDelete { get; set; }
    }
}
