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
    public class GetRelatedValuesCommand : Command
    {


        private GetRelatedValuesViewModel viewModel;

        public GetRelatedValuesCommand(GetRelatedValuesViewModel vm)
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

            if (parameters == null || parameters[0] == null || parameters[1] == null || parameters[2] == null || parameters[3] == null)
            {
                return;
            }

            long gid = (long)parameters[0];
            ModelCode property = (ModelCode)parameters[1];
            ModelCode type = (ModelCode)parameters[2];

            List<ModelCode> properties = new List<ModelCode>();
            System.Collections.IList i = (System.Collections.IList)parameters[3];
            var propertyModels = i.Cast<PropertyModel>();
         
            foreach (var propertyModel in propertyModels)
            {
                properties.Add(propertyModel.ModelCode);
            }

            Association association = new Association(property , type);
            
            List<ResourceDescription> resourceDescritions = GetRelatedValues(gid, association, properties);

            List<PropertyView> propertyViews = new List<PropertyView>();

            foreach (ResourceDescription rd in resourceDescritions)
            {
                string gidString = String.Format("GID: 0x{0:x16}", rd.Id);
                propertyViews.Add(new PropertyView(type, gidString));
                foreach (Property p in rd.Properties)
                {
                    propertyViews.Add(new PropertyView(p.Id, p.ToString()));
                }
                propertyViews.Add(new PropertyView());
            }
            viewModel.ObjectValue = new ObservableCollection<PropertyView>(propertyViews); //
  
        }

        public List<ResourceDescription> GetRelatedValues(long sourceGlobalId, Association association , List<ModelCode> properties)
        {
            string message = "Getting related values method started.";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            
            int numberOfResources = 2;
            List<ResourceDescription> resourceDescriptions = new List<ResourceDescription>();
            try
            {

                int iteratorId = viewModel.Proxy.GetRelatedValues(sourceGlobalId, properties, association);
                int resourcesLeft = viewModel.Proxy.IteratorResourcesLeft(iteratorId);
              

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

                message = "Getting related values method successfully finished.";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
            }
            catch (Exception e)
            {
                message = string.Format("Getting related values method  failed for sourceGlobalId = {0} and association (propertyId = {1}, type = {2}). Reason: {3}", sourceGlobalId, association.PropertyId, association.Type, e.Message);
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                return null;
            }

            return resourceDescriptions;
        }
    }
}
