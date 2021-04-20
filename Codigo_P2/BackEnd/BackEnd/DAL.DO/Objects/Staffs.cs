using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DO.Objects
{
    public  class Staffs
    {
        public Staffs()
        {
            Orders = new HashSet<Orders>();
        }

        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Stores Store { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}