using Client.Model;
using Client.ViewModel;
using FTN.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Client.Command
{
    public class GetExtentValuesCommand : Command
    {

        private GetExtentValuesViewModel viewModel;
        public GetExtentValuesCommand(GetExtentValuesViewModel vm)
        {
            viewModel = vm;
        }
        public override void Execute(object parameter)
        {
            if (parameter == null || !(parameter is Object[]))
            {
                return;
            }

            Object[] parameters = parameter as Object[];

            if (parameters == null || parameters[0] == null || parameters[1] == null)
            {
                return;
            }

            List<ModelCode> properties = new List<ModelCode>();
            System.Collections.IList i = (System.Collections.IList)parameters[1];
            var propertyModels = i.Cast<PropertyModel>();
            ModelCode modelCode;
           if(!ModelCodeHelper.GetModelCodeFromString(parameters[0].ToString(), out modelCode))
            {
                return;
            }

            foreach (var propertyModel in propertyModels)
            {
                properties.Add(propertyModel.ModelCode);
            }

            List<ResourceDescription> resourceDescritions = GetExtentValues(modelCode , properties);

            List<PropertyView> propertyViews = new List<PropertyView>();

            foreach (ResourceDescription rd in resourceDescritions)
            {
                string gidString = String.Format("GID: 0x{0:x16}", rd.Id);
                propertyViews.Add(new PropertyView(modelCode, gidString));
                foreach(Property p  in rd.Properties)
                {
                    propertyViews.Add(new PropertyView(p.Id, p.ToString()));
                }
                propertyViews.Add(new PropertyView());
            }
            viewModel.ObjectValue = new ObservableCollection<PropertyView>(propertyViews); //
        }

        public List<ResourceDescription> GetExtentValues(ModelCode modelCode , List<ModelCode> properties)
        {

            string message = "Getting extent values method started.";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            int iteratorId = 0;
            List<ResourceDescription>  resourceDescriptions = new List<ResourceDescription>();

            try
            {
                int numberOfResources = 2;
                int resourcesLeft = 0;
                

                iteratorId = viewModel.Proxy.GetExtentValues(modelCode, properties);
                resourcesLeft = viewModel.Proxy.IteratorResourcesLeft(iteratorId);
                             
                while (resourcesLeft > 0)
                {
                    List<ResourceDescription> rds = viewModel.Proxy.IteratorNext(numberOfResources, iteratorId);

                    for (int i = 0; i < rds.Count; i++)
                    {
                        resourceDescriptions.Add(rds[i]);
                    }

                    resourcesLeft = viewModel.Proxy.IteratorResourcesLeft(iteratorId);
                }

                viewModel.Proxy.IteratorClose(iteratorId);
                message = "Getting extent values method successfully finished.";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

            }
            catch(Exception e)
            {
                message = string.Format("Getting extent values method failed for {0}.\n\t{1}", modelCode, e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

                return null;
            }
           

            return resourceDescriptions;
        }
    }
}
