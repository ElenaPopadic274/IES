using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class ProductAssetModel : AssetModel
    {
        public ProductAssetModel(long globalId) : base(globalId)
        {
        }

        private CorporateStandardKind corporateStandardKind;
        private string modelNumber;
        private string modelVersion;
        private AssetModelUsageKind usageKind;
        private float weightTotal;
        private long manufacturer = 0;

        #region Properties

        public CorporateStandardKind CorporateStandardKind
        {
            get
            {
                return corporateStandardKind;
            }

            set
            {
                corporateStandardKind = value;
            }
        }

        public string ModelNumber
        {
            get
            {
                return modelNumber;
            }

            set
            {
                modelNumber = value;
            }
        }

        public string ModelVersion
        {
            get
            {
                return modelVersion;
            }

            set
            {
                modelVersion = value;
            }
        }

        public AssetModelUsageKind UsageKind
        {
            get
            {
                return usageKind;
            }

            set
            {
                usageKind = value;
            }
        }

        public float WeightTotal
        {
            get
            {
                return weightTotal;
            }

            set
            {
                weightTotal = value;
            }
        }

        public long Manufacturer
        {
            get
            {
                return manufacturer;
            }

            set
            {
                manufacturer = value;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ProductAssetModel x = (ProductAssetModel)obj;
                return ((x.corporateStandardKind == this.corporateStandardKind) && (x.modelNumber == this.modelNumber) && (x.modelVersion == this.modelVersion) && (x.weightTotal == this.weightTotal) && (x.manufacturer == this.manufacturer));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();

        }

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.PRODUCTASSETMODEL_CSTANDARDKIND:
                    return true;
                case ModelCode.PRODUCTASSETMODEL_MANUFACTURER:
                    return true;
                case ModelCode.PRODUCTASSETMODEL_MODELNUMBER:
                    return true;
                case ModelCode.PRODUCTASSETMODEL_MODELVERSION:
                    return true;
                case ModelCode.PRODUCTASSETMODEL_USAGEKIND:
                    return true;
                case ModelCode.PRODUCTASSETMODEL_WEIGHTTOTAL:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.PRODUCTASSETMODEL_CSTANDARDKIND:
                    prop.SetValue((short)corporateStandardKind);
                    break;
                case ModelCode.PRODUCTASSETMODEL_MANUFACTURER:
                    prop.SetValue(manufacturer);
                    break;
                case ModelCode.PRODUCTASSETMODEL_MODELNUMBER:
                    prop.SetValue(modelNumber);
                    break;
                case ModelCode.PRODUCTASSETMODEL_MODELVERSION:
                    prop.SetValue(modelVersion);
                    break;
                case ModelCode.PRODUCTASSETMODEL_USAGEKIND:
                    prop.SetValue((short)usageKind);
                    break;
                case ModelCode.PRODUCTASSETMODEL_WEIGHTTOTAL:
                    prop.SetValue(weightTotal);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PRODUCTASSETMODEL_CSTANDARDKIND:
                    corporateStandardKind = (CorporateStandardKind)property.AsEnum();
                    break;
                case ModelCode.PRODUCTASSETMODEL_MANUFACTURER:
                    manufacturer = property.AsReference();
                    break;
                case ModelCode.PRODUCTASSETMODEL_MODELNUMBER:
                    modelNumber = property.AsString();
                    break;
                case ModelCode.PRODUCTASSETMODEL_MODELVERSION:
                    modelVersion = property.AsString();
                    break;
                case ModelCode.PRODUCTASSETMODEL_USAGEKIND:
                    usageKind = (AssetModelUsageKind)property.AsEnum();
                    break;
                case ModelCode.PRODUCTASSETMODEL_WEIGHTTOTAL:
                    weightTotal = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #region IReference implementation


        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (manufacturer != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.PRODUCTASSETMODEL_MANUFACTURER] = new List<long>();
                references[ModelCode.PRODUCTASSETMODEL_MANUFACTURER].Add(manufacturer);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation	
    }
}
