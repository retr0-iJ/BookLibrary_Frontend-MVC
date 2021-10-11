using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binus.SampleWebAPI.Model.AppModel
{
    public class BorrowBookModel
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string BookDesc { get; set; }
        public string BorrowDate { get; set; }
    }
}
