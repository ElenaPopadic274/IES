using Client.Command;
using Client.Model;
using FTN.Common;
using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Client.ViewModel
{
    public class GetExtentValuesViewModel : AbstractViewModel
    {
        private ObservableCollection<PropertyView> objectValue;
        public GetExtentValuesCommand LoadValues{get;set;}


        public GetExtentValuesViewModel(INetworkModelGDAContract proxy) : base(proxy)
        {
            this.LoadValues = new GetExtentValuesCommand(this);
        }

       
        public DMSType Type
        {
            get { return type; }
            set { this.type = value; OnPropertyChanged("Type"); OnPropertyChanged("Properties");}
        }


        public ObservableCollection<PropertyView> ObjectValue
        {
            get { return objectValue; }
            set { this.objectValue = value; OnPropertyChanged("ObjectValue"); }
        }

    }
}
