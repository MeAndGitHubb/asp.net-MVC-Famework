using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string Note { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double Amount { get; set; }
        public int? Sale { get; set; }
    }
}
