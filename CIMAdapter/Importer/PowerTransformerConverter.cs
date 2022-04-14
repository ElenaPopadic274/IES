namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    using System;
    using FTN.Common;

    /// <summary>
    /// PowerTransformerConverter has methods for populating
    /// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
    /// </summary>
    public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.Name));
                }
            }
		}

        public static void PopulateAssetFunctionProperties(FTN.AssetFunction cimAssetFunction, ResourceDescription rd)
        {
            if ((cimAssetFunction != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAssetFunction, rd);

                if (cimAssetFunction.ConfigIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSETFUNCTION_CONFIGID, cimAssetFunction.ConfigID));
                }
                if (cimAssetFunction.FirmwareIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSETFUNCTION_FIRMWAREID, cimAssetFunction.FirmwareID));
                }
                if (cimAssetFunction.HardwareIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSETFUNCTION_HARDWAREID, cimAssetFunction.HardwareID));
                }
                if (cimAssetFunction.PasswordHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSETFUNCTION_PASSWORD, cimAssetFunction.Password));
                }
                if (cimAssetFunction.ProgramIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSETFUNCTION_PROGRAMID, cimAssetFunction.ProgramID));
                }
            }

        }

        public static void PopulateSealProperties(Seal cimSeal, ResourceDescription rd)
        {
            if ((cimSeal != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSeal, rd);

                if (cimSeal.AppliedDateTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEAL_APPLIEDTIME, cimSeal.AppliedDateTime));
                }
                if (cimSeal.ConditionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEAL_CONDITION, (short)cimSeal.Condition));
                }
                if (cimSeal.KindHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEAL_KIND, (short)cimSeal.Kind));
                }
                if (cimSeal.SealNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEAL_SEALNUMBER, cimSeal.SealNumber));
                }
            }
        }


        public static void PopulateManufacturerProperties(Manufacturer cimManufacturer, ResourceDescription rd)
        {
            if ((cimManufacturer != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateOrganisationRoleProperties(cimManufacturer, rd);
            }
        }

        public static void PopulateOrganisationRoleProperties(OrganisationRole cimOrganisationRole, ResourceDescription rd)
        {
            if ((cimOrganisationRole != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimOrganisationRole, rd);
            }
        }

        public static void PopulateProductAssetModelProperties(ProductAssetModel cimProductAssetModel, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimProductAssetModel != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimProductAssetModel, rd);

                if (cimProductAssetModel.CorporateStandardKindHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PRODUCTASSETMODEL_CSTANDARDKIND, (short)cimProductAssetModel.CorporateStandardKind));
                }

                if (cimProductAssetModel.ModelNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PRODUCTASSETMODEL_MODELNUMBER, cimProductAssetModel.ModelNumber));
                }

                if (cimProductAssetModel.ModelVersionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PRODUCTASSETMODEL_MODELVERSION , cimProductAssetModel.ModelVersion));
                }
                if (cimProductAssetModel.UsageKindHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PRODUCTASSETMODEL_USAGEKIND, (short)cimProductAssetModel.UsageKind));
                }
                if (cimProductAssetModel.WeightTotalHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PRODUCTASSETMODEL_WEIGHTTOTAL, cimProductAssetModel.WeightTotal));
                }
                if (cimProductAssetModel.ManufacturerHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimProductAssetModel.Manufacturer.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimProductAssetModel.GetType().ToString()).Append(" rdfID = \"").Append(cimProductAssetModel.ID);
                        report.Report.Append("\" - Failed to set reference to AssetModel: rdfID \"").Append(cimProductAssetModel.Manufacturer.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.PRODUCTASSETMODEL_MANUFACTURER, gid));
                }
            }
        }

        public static void PopulateAssetProperties(Asset cimAsset, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimAsset != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAsset, rd);

                if (cimAsset.CriticalHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_CRITICAL, cimAsset.Critical));
                }
                if (cimAsset.InitialConditionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_INITIALCONDITION, cimAsset.InitialCondition));
                }
                if (cimAsset.InitialLossOfLifeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_INITIALLOSSOFLIFE, cimAsset.InitialLossOfLife));
                }
                if (cimAsset.LotNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_LOTNUMBER, cimAsset.LotNumber));
                }
                if (cimAsset.PurchasePriceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_PURCHASEPRICE, cimAsset.PurchasePrice));
                }
                if (cimAsset.SerialNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_SERIALNUMBER, cimAsset.SerialNumber));
                }
                if (cimAsset.TypeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_TYPE, cimAsset.Type));
                }
                if (cimAsset.UtcNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.ASSET_UTCNUMBER, cimAsset.UtcNumber));
                }
                if (cimAsset.AssetInfoHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimAsset.AssetInfo.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimAsset.GetType().ToString()).Append(" rdfID = \"").Append(cimAsset.ID);
                        report.Report.Append("\" - Failed to set reference to AssetModel: rdfID \"").Append(cimAsset.AssetInfo.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.ASSET_ASSETINFO, gid));
                }
                /*
                if (cimAsset.OrganisationRolesHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimAsset.OrganisationRoles.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimAsset.GetType().ToString()).Append(" rdfID = \"").Append(cimAsset.ID);
                        report.Report.Append("\" - Failed to set reference to AssetModel: rdfID \"").Append(cimAsset.OrganisationRoles.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.ASSET_ASSETORGANISATIONROLE, gid));
                }
                */
            }
        }

        public static void PopulateAssetOwnerProperties(AssetOwner cimAssetOwner, ResourceDescription rd)
        {
            if ((cimAssetOwner != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateAssetOrganisationRole(cimAssetOwner, rd);
            }
        }

        private static void PopulateAssetOrganisationRole(AssetOrganisationRole cimAssetOrganisationRole, ResourceDescription rd)
        {
            if ((cimAssetOrganisationRole != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAssetOrganisationRole, rd);
            }
        }

        public static void PopulateAssetInfoProperties(AssetInfo cimAssetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimAssetInfo != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAssetInfo, rd);
                
                if(cimAssetInfo.AssetModelHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimAssetInfo.AssetModel.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimAssetInfo.GetType().ToString()).Append(" rdfID = \"").Append(cimAssetInfo.ID);
                        report.Report.Append("\" - Failed to set reference to AssetModel: rdfID \"").Append(cimAssetInfo.AssetModel.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.ASSETINFO_ASSETMODEL, gid));
                }
            }
        }

        public static void PopulateAssetModelProperties(AssetModel cimAssetModel, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimAssetModel != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimAssetModel, rd);

                if (cimAssetModel.AssetInfoHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimAssetModel.AssetInfo.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimAssetModel.GetType().ToString()).Append(" rdfID = \"").Append(cimAssetModel.ID);
                        report.Report.Append("\" - Failed to set reference to AssetModel: rdfID \"").Append(cimAssetModel.AssetInfo.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.ASSETMODEL_ASSETINFO, gid));
                }

            }
        }

        #endregion Populate ResourceDescription

        #region Enums convert

        public static SealConditionKind GetDMSSealConditionKind(FTN.SealConditionKind conditionKinds)
        {
            switch(conditionKinds)
            {
                case FTN.SealConditionKind.broken:
                    return SealConditionKind.Broken;
                case FTN.SealConditionKind.locked:
                    return SealConditionKind.Locked;
                case FTN.SealConditionKind.missing:
                    return SealConditionKind.Missing;
                case FTN.SealConditionKind.open:
                    return SealConditionKind.Open;
                default:
                    return SealConditionKind.Other;
            }
        }

        public static SealKind GetDMSSealKind(FTN.SealKind sealKinds)
        {
            switch(sealKinds)
            {
                case FTN.SealKind.lead:
                    return SealKind.Lead;
                case FTN.SealKind.@lock:
                    return SealKind.Lock;
                case FTN.SealKind.steel:
                    return SealKind.Steel;
                default:
                    return SealKind.Other;
            }
        }

        public static CorporateStandardKind GetDMSCorporateStandardKind(FTN.CorporateStandardKind standardKinds)
        {
            switch(standardKinds)
            {
                case FTN.CorporateStandardKind.experimental:
                    return CorporateStandardKind.Experimental;
                case FTN.CorporateStandardKind.standard:
                    return CorporateStandardKind.Standard;
                case FTN.CorporateStandardKind.underEvaluation:
                    return CorporateStandardKind.UnderEvaluation;
                default:
                    return CorporateStandardKind.Other;
            }
        }

        public static AssetModelUsageKind GetDMSAssetModelUsageKind(FTN.AssetModelUsageKind usageKinds)
        {
            switch(usageKinds)
            {
                case FTN.AssetModelUsageKind.customerSubstation:
                    return AssetModelUsageKind.CustomerSubstation;
                case FTN.AssetModelUsageKind.distributionOverhead:
                    return AssetModelUsageKind.DistributionOverhead;
                case FTN.AssetModelUsageKind.distributionUnderground:
                    return AssetModelUsageKind.DistributionUnderground;
                case FTN.AssetModelUsageKind.other:
                    return AssetModelUsageKind.Other;
                case FTN.AssetModelUsageKind.streetlight:
                    return AssetModelUsageKind.Streetlight;
                case FTN.AssetModelUsageKind.substation:
                    return AssetModelUsageKind.Substation;
                case FTN.AssetModelUsageKind.transmission:
                    return AssetModelUsageKind.Transmission;
                default:
                    return AssetModelUsageKind.Unknown;
            }
        }
		#endregion Enums convert
	}
}
