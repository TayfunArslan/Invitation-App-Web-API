namespace Invitation_App_Web_API.Data.Entities
{
    public class Guest : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public long OrganizationId { get; set; }
        public bool IsCalled { get; set; }
        public bool IsEmailSent { get; set; }
        public bool WillCome { get; set; }

        public Organization Organization { get; set; }
    }
}
