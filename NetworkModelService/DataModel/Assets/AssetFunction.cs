using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Assets
{
    public class AssetFunction : IdentifiedObject
    {

        private string configId;
        private string firmwareId;
        private string hadwareId;
        private string password;
        private string programId;

        public AssetFunction(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                AssetFunction x = (AssetFunction)obj;
                return ((x.configId == this.configId) && x.firmwareId == this.firmwareId && x.hadwareId == this.hadwareId && x.password == this.password && x.programId == this.programId);
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
        public string ConfigId
        {
            get
            {
                return configId;
            }

            set
            {
                configId = value;
            }
        }

        public string FirmwareId
        {
            get
            {
                return firmwareId;
            }

            set
            {
                firmwareId = value;
            }
        }

        public string HadwareId
        {
            get
            {
                return hadwareId;
            }

            set
            {
                hadwareId = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string ProgramId
        {
            get
            {
                return programId;
            }

            set
            {
                programId = value;
            }
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.ASSETFUNCTION_CONFIGID:
                case ModelCode.ASSETFUNCTION_FIRMWAREID:
                case ModelCode.ASSETFUNCTION_HARDWAREID:
                case ModelCode.ASSETFUNCTION_PASSWORD:
                case ModelCode.ASSETFUNCTION_PROGRAMID:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.ASSETFUNCTION_CONFIGID:
                    prop.SetValue(configId);
                    break;
                case ModelCode.ASSETFUNCTION_FIRMWAREID:
                    prop.SetValue(firmwareId);
                    break;
                case ModelCode.ASSETFUNCTION_HARDWAREID:
                    prop.SetValue(hadwareId);
                    break;
                case ModelCode.ASSETFUNCTION_PASSWORD:
                    prop.SetValue(password);
                    break;
                case ModelCode.ASSETFUNCTION_PROGRAMID:
                    prop.SetValue(programId);
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
                case ModelCode.ASSETFUNCTION_CONFIGID:
                    configId = property.AsString();
                    break;
                case ModelCode.ASSETFUNCTION_FIRMWAREID:
                    firmwareId = property.AsString();
                    break;
                case ModelCode.ASSETFUNCTION_HARDWAREID:
                    hadwareId = property.AsString();
                    break;
                case ModelCode.ASSETFUNCTION_PASSWORD:
                    password = property.AsString();
                    break;
                case ModelCode.ASSETFUNCTION_PROGRAMID:
                    programId = property.AsString();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation	
    }
}
