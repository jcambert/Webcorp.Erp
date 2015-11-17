using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.erp.quotation
{
    public class FormElement
    {
        public string  Name { get; set; }

        public object Value { get; set; }
    }

    public class FormElement<T> : FormElement
    {

    }

    public class DateElement : FormElement<DateTime>
    {

    }

    public class StringElement : FormElement<string>
    {

    }
}
