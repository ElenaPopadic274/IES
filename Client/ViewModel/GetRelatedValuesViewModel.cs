using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.ServiceContracts;
using FTN.Common;
using System.Collections.ObjectModel;
using Client.Model;
using Client.Command;

namespace Client.ViewModel
{
    public class GetRelatedValuesViewModel : AbstractViewModel
    {

        private ObservableCollection<ModelCode> referenceTypes;
        private ModelCode referenceSelected;
        private ModelCode propertyType;
        private ObservableCollection<PropertyView> objectValue;

        public GetRelatedValuesCommand LoadValues { get; set; }
        public GetRelatedValuesViewModel(INetworkModelGDAContract proxy) : base(proxy)
        {
            this.LoadValues = new GetRelatedValuesCommand(this);
        }

        public DMSType Type
        {
            get { return type; }
            set { this.type = value; OnPropertyChanged("Type"); OnPropertyChanged("Properties"); OnPropertyChanged("GIDs"); OnPropertyChanged("References"); }
        }


        public ObservableCollection<long> GIDs
        {
            get { return type == 0 ? new ObservableCollection<long>() : GetGIDs(type); }
            set { this.GIDs = value; OnPropertyChanged("GIDs"); }
        }

        public ObservableCollection<ModelCode> References
        {
            get
            {
                return type == 0 ? new ObservableCollection<ModelCode>() : GetReferences();
            }

            set { this.References = value; OnPropertyChanged("References"); }
        }

        public ObservableCollection<ModelCode> PropertyTypes
        {
            get
            {
                if(referenceSelected != 0)
                {
                    try
                    {
                        
                        string propertyName = Enum.GetName(typeof(ModelCode), referenceSelected).Split('_')[1];
                        List<ModelCode> allPropeties = new List<ModelCode>();
                        switch (propertyName)
                        {
                            case "ASSETORGANISATIONROLE":
                                allPropeties .Add(ModelCode.ASSETOWNER);
                                break;
                            case "ASSETMODEL":
                                allPropeties.Add(ModelCode.PRODUCTASSETMODEL);
                                break;
                            default:
                                allPropeties.Add((ModelCode)Enum.Parse(typeof(ModelCode), propertyName));
                                break;

                        }
                        return new ObservableCollection<ModelCode>(allPropeties);
                    }
                    catch
                    {
                        return new ObservableCollection<ModelCode>();
                    }
                }
                return  new ObservableCollection<ModelCode>();
            }

            set
            {
                referenceTypes = value; OnPropertyChanged("ReferencedProperties");
            }
        }

        public ModelCode ReferenceSelected
        {
            get
            {
                return referenceSelected;
            }

            set
            {
                referenceSelected = value; OnPropertyChanged("PropertyTypes");
            }
        }

        private ObservableCollection<ModelCode> GetReferences()
        {
            ObservableCollection<ModelCode> references = new ObservableCollection<ModelCode>();
            foreach (var mc in Properties)
            {
                if (((ulong)mc.ModelCode & 0x0000000000000009) == 0x0000000000000009)
                {
                    references.Add(mc.ModelCode);
                }
            }
            return references;
        }


        public ObservableCollection<PropertyModel> ReferencedProperties
        {
            get { return propertyType == 0 ? new ObservableCollection<PropertyModel>() : CreatePropertyModel(modelResurcesDesc.GetAllPropertyIds(propertyType)); }
            set { this.ReferencedProperties = value; OnPropertyChanged("ReferencedProperties"); }
        }

        public ModelCode PropertyType
        {
            get
            {
                return propertyType;
            }

            set
            {
                propertyType = value; OnPropertyChanged("PropertyType"); OnPropertyChanged("ReferencedProperties");
            }
        }

        public ObservableCollection<PropertyView> ObjectValue
        {
            get { return objectValue; }
            set { this.objectValue = value; OnPropertyChanged("ObjectValue"); }
        }
    }
}
