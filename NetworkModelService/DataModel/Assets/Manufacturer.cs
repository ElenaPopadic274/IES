using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class Manufacturer : OrganisationRole
    {
        public Manufacturer(long globalId) : base(globalId)
        {
        }

        private List<long> productAssetModels = new List<long>();

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Manufacturer x = (Manufacturer)obj;
                return (CompareHelper.CompareLists(x.productAssetModels, this.productAssetModels));
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
                case ModelCode.MANUFACTURER_PRODUCTASSETMODEL:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.MANUFACTURER_PRODUCTASSETMODEL:
                    prop.SetValue(productAssetModels);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }


        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return productAssetModels.Count > 0 || base.IsReferenced;
            }
        }

        public List<long> ProductAssetModels
        {
            get
            {
                return productAssetModels;
            }

            set
            {
                productAssetModels = value;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (productAssetModels != null && productAssetModels.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.MANUFACTURER_PRODUCTASSETMODEL] = productAssetModels.GetRange(0, productAssetModels.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.PRODUCTASSETMODEL_MANUFACTURER:
                    productAssetModels.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.PRODUCTASSETMODEL_MANUFACTURER:

                    if (productAssetModels.Contains(globalId))
                    {
                        productAssetModels.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation	
    }
}
