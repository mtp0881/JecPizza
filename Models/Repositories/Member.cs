using System;

namespace JecPizza.Models.Repositories
{
    public class Member
    {
        public string MemberId { get; set; }

        public string UserName { get; set; }

        public string MemberName { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public string PostCode { get; set; }
        public  string Address { get; set; }

        public DateTime Dob { get; set; }

        public bool Sex { get; set; }
        public string Img { get; set; }

        public  string Password { get; set; }
    }
}