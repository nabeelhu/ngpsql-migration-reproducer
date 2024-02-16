using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migration.Reproducer.Models
{
    public enum StatusType
    {
        Active,
        Inactive
    }

    public abstract class MyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StatusType status { get; set; }
    }

    public class ActiveEntity : MyEntity
    {


    }
    public class InactiveEntity : MyEntity
    {


    }
}
