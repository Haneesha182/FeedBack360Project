using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedBack360.ViewModel
{
    public class UserDetailViewModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Retype_Password { get; set; }
        public int EmpId { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Country { get; set; }
        public string Language { get; set; }
        public string Role { get; set; }
        public List<CountryViewModel> CountryDetail { get; set; }
        public List<LanguageViewModel> LanguageDetail { get; set; }
        public List<RoleDetailViewModel> RoleDetail { get; set; }

    }
}
