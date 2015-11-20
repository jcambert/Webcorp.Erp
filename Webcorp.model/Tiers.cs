using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    public class Address
    {

    }

    public abstract class Tiers:Entity
    {
        [KeyProvider]
        public string Nom { get; set; }

        public Address Adresse { get; set; }
    }

    public class Company : Tiers
    {

    }

    public class Person : Tiers
    {

    }
}
