using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI
    {
        public static DataAPI CreateDataAPI()
        {
            return new Data();
        }
    }
    internal class Data : DataAPI
    {

    }
}
