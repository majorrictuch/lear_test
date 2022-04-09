using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model.Dto
{
    public class ADUserProfileOutput
    {


        public string EmpId { get; set; }
        public string Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string DepartmentId { get; set; }
        public string Location { get; set; }
        public string OUCode { get; set; }
        public int Active { get; set; }
    }
}
