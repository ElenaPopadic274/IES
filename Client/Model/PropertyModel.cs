using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    public class PropertyModel
    {
        private bool isSelected = false;

        private string name;

        private ModelCode modelCode;
        private ModelCode m;

        public PropertyModel(ModelCode m)
        {
            this.modelCode = m;
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public ModelCode ModelCode
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
