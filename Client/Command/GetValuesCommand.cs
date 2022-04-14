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
    public class GetValuesCommand : Command
    {
        private GetValuesViewModel viewModel;

        public GetValuesCommand(GetValuesViewModel vm)
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

            if(parameters == null || parameters[0] == null || parameters[1] == null)
            {
                return;
            }

            long gid = (long)parameters[0];
            List<ModelCode> properties = new List<ModelCode>();
            System.Collections.IList i = (System.Collections.IList)parameters[1];
            var propertyModels = i.Cast<PropertyModel>();

            foreach (var propertyModel in propertyModels)
            {
                properties.Add(propertyModel.ModelCode);
            }
            ResourceDescription rd =  viewModel.Proxy.GetValues(gid, properties);
            List<PropertyView> propertyViews = new List<PropertyView>();
            foreach(Property p in rd.Properties)
            {
                propertyViews.Add(new PropertyView(p.Id ,p.ToString()));
            }
            viewModel.ObjectValue = new ObservableCollection<PropertyView>(propertyViews);
        }
    }
}
