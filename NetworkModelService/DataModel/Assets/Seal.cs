using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class Seal : IdentifiedObject
    {
        public Seal(long globalId) : base(globalId)
        {
        }

        private long appliedDateTime;
        private SealConditionKind condition;
        private SealKind kind;
        private string sealNumber;

        public long AppliedDateTime
        {
            get
            {
                return appliedDateTime;
            }

            set
            {
                appliedDateTime = value;
            }
        }

        public SealConditionKind Condition
        {
            get
            {
                return condition;
            }

            set
            {
                condition = value;
            }
        }

        public SealKind Kind
        {
            get
            {
                return kind;
            }

            set
            {
                kind = value;
            }
        }

        public string SealNumber
        {
            get
            {
                return sealNumber;
            }

            set
            {
                sealNumber = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Seal x = (Seal)obj;
                return ((x.appliedDateTime == this.appliedDateTime) && (x.condition == this.condition) && (x.kind == this.kind) && (x.sealNumber == this.sealNumber));
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

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.SEAL_APPLIEDTIME:
                case ModelCode.SEAL_CONDITION:
                case ModelCode.SEAL_KIND:
                case ModelCode.SEAL_SEALNUMBER:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SEAL_APPLIEDTIME:
                    prop.SetValue(appliedDateTime);
                    break;
                case ModelCode.SEAL_CONDITION:
                    prop.SetValue((short)condition);
                    break;
                case ModelCode.SEAL_KIND:
                    prop.SetValue((short)kind);
                    break;
                case ModelCode.SEAL_SEALNUMBER:
                    prop.SetValue(sealNumber);
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
                case ModelCode.SEAL_APPLIEDTIME:
                    appliedDateTime = property.AsLong();
                    break;
                case ModelCode.SEAL_CONDITION:
                    condition = (SealConditionKind)property.AsEnum();
                    break;
                case ModelCode.SEAL_KIND:
                    kind =(SealKind)property.AsEnum();
                    break;
                case ModelCode.SEAL_SEALNUMBER:
                    sealNumber = property.AsString();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
