using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursBd
{
    class Helper
    {
        public  static int RemID;
        private static ToursEntities ConObj;
        public static ToursEntities GetContext()
        {
            if (ConObj == null)
            {
                ConObj = new ToursEntities();
            }
            return ConObj;
        }

        
    }
}
