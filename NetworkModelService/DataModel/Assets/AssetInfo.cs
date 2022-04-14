using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class AssetInfo : IdentifiedObject
    {
        public AssetInfo(long globalId) : base(globalId)
        {
        }

        private List<long> assets = new List<long>();
        private long assetModel = 0;

        public long AssetModel
        {
            get
            {
                return assetModel;
            }

            set
            {
                assetModel = value;
            }
        }

        public List<long> Assets
        {
            get
            {
                return assets;
            }

            set
            {
                assets = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                AssetInfo x = (AssetInfo)obj;
                return (CompareHelper.CompareLists(x.assets, this.assets) && (x.assetModel == this.assetModel));
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
                case ModelCode.ASSETINFO_ASSETS:
                    return true;
                case ModelCode.ASSETINFO_ASSETMODEL:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.ASSETINFO_ASSETS:
                    prop.SetValue(assets);
                    break;
                case ModelCode.ASSETINFO_ASSETMODEL:
                    prop.SetValue(assetModel);
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
                case ModelCode.ASSETINFO_ASSETMODEL:
                    assetModel = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return assets.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (assets != null && assets.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETINFO_ASSETS] = assets.GetRange(0, assets.Count);
            }

            if (assetModel != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETINFO_ASSETMODEL] = new List<long>();
                references[ModelCode.ASSETINFO_ASSETMODEL].Add(assetModel);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ASSET_ASSETINFO:
                    assets.Add(globalId);
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
                case ModelCode.ASSET_ASSETINFO:

                    if (assets.Contains(globalId))
                    {
                        assets.Remove(globalId);
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
