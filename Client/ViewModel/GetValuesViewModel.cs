using FTN.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Xml;
using Client.Model;
using Client.Command;
using FTN.ServiceContracts;

namespace Client.ViewModel
{

    public class GetValuesViewModel : AbstractViewModel
    {

        
        public GetValuesCommand  LoadValues {get;set;}
        public ObservableCollection<PropertyView> objectValue;

        public GetValuesViewModel(INetworkModelGDAContract proxy) : base(proxy)
        {
            this.LoadValues = new GetValuesCommand(this);
        }

        public DMSType Type
        {
            get { return type; }
            set { this.type = value; OnPropertyChanged("Type"); OnPropertyChanged("Properties"); ; OnPropertyChanged("GIDs"); }
        }
        

        public ObservableCollection<long> GIDs
        {
            get { return type == 0 ? new ObservableCollection<long>() : GetGIDs(type); }
            set { this.GIDs = value; OnPropertyChanged("GIDs"); }
        }

        public ObservableCollection<PropertyView> ObjectValue
        {
            get { return objectValue; }
            set { this.objectValue = value; OnPropertyChanged("ObjectValue"); }
        }    

      
    }
}
