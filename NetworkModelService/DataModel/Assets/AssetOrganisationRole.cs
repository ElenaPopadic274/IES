using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class AssetOrganisationRole : OrganisationRole
    {
        public AssetOrganisationRole(long globalId) : base(globalId)
        {
        }

        private List<long> assets = new List<long>();

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
                AssetOrganisationRole x = (AssetOrganisationRole)obj;
                return (CompareHelper.CompareLists(x.assets, this.assets));
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
                case ModelCode.ASSETORGANISATIONROLE_ASSET:
                    return true;
          
                default:
                    return base.HasProperty(t);

            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.ASSETORGANISATIONROLE_ASSET:
                    prop.SetValue(assets);
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
                return assets.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (assets != null && assets.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.ASSETORGANISATIONROLE_ASSET] = assets.GetRange(0, assets.Count);
            }
          
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ASSET_ASSETORGANISATIONROLE:
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
                case ModelCode.ASSET_ASSETORGANISATIONROLE:

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
