using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    public class PropertyView 
    {
        String modelCode;
        String value;

        public PropertyView(ModelCode modelCode , string value)
        {
            this.modelCode = modelCode.ToString();
            this.value = value;

        }

        public PropertyView()
        {
            this.modelCode = String.Empty;
            this.value = String.Empty;
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public String ModelCode
        {
            get
            {
                return modelCode;
            }

            set
            {
                modelCode = value;
            }
        }
    }
}
