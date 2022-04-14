using Client.Model;
using FTN.Common;
using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Client.ViewModel
{
    public abstract  class AbstractViewModel : INotifyPropertyChanged
    {


        public AbstractViewModel(INetworkModelGDAContract proxy)
        {
            this.proxy = proxy;
            this.modelResurcesDesc = new ModelResourcesDesc();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private INetworkModelGDAContract proxy;
        protected DMSType type;
        protected ModelResourcesDesc modelResurcesDesc = null;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }



        protected ObservableCollection<long> GetGIDs(DMSType modelCode)
        {
            string message = "Getting extent values method started.";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            int iteratorId = 0;
            List<long> ids = new List<long>();

            try
            {
                int numberOfResources = 2;
                int resourcesLeft = 0;

                List<ModelCode> properties = modelResurcesDesc.GetAllPropertyIds(modelCode);

                iteratorId = Proxy.GetExtentValues(modelResurcesDesc.GetModelCodeFromType(modelCode), properties);
                resourcesLeft = Proxy.IteratorResourcesLeft(iteratorId);

                while (resourcesLeft > 0)
                {
                    List<ResourceDescription> rds = Proxy.IteratorNext(numberOfResources, iteratorId);

                    for (int i = 0; i < rds.Count; i++)
                    {
                        ids.Add(rds[i].Id);
                    }

                    resourcesLeft = Proxy.IteratorResourcesLeft(iteratorId);
                }

                Proxy.IteratorClose(iteratorId);

                message = "Getting extent values method successfully finished.";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            }
            catch (Exception e)
            {
                message = string.Format("Getting extent values method failed for {0}.\n\t{1}", modelCode, e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }



           ObservableCollection<long> gids = new ObservableCollection<long>(ids);       
            
           return gids;

        }
       protected ObservableCollection<PropertyModel> CreatePropertyModel(List<ModelCode> mc)
        {
            ObservableCollection<PropertyModel> retVal = new ObservableCollection<PropertyModel>();
            foreach (ModelCode m in mc)
            {
                retVal.Add(new PropertyModel(m));
            }
            return retVal;
        }

        public ObservableCollection<PropertyModel> Properties
        {
            get { return type == 0 ? new ObservableCollection<PropertyModel>() : CreatePropertyModel(modelResurcesDesc.GetAllPropertyIds(type)); }
            set { this.Properties = value; OnPropertyChanged("Properties"); }
        }

        public INetworkModelGDAContract Proxy
        {
            get
            {
                return proxy;
            }

            private set
            {
                proxy = value;
            }
        }
    }
}
