using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Employee
    {
        public int HourIncome { get; set; }
        public int MonthlySalery { get; set; }

        public int MontlyIncome
        {
            get
            {
                return HourIncome * 8 * 5 * 4;
            }
        }
    }
}
