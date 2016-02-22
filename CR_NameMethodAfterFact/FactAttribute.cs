using System;
using System.Linq;
using System.Collections.Generic;

namespace CR_NameMethodAfterFact
{
    public class MyClass
    {
        [Fact(DisplayName = "When do something, then Condition should be true")]
        public void When_do_something_then_Condition_should_be_true()
        {

        }
    }
    
    public class FactAttribute : System.Attribute
    {
        // Fields...
        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
            }
        }

        public FactAttribute()
        {

        }
    }
}
