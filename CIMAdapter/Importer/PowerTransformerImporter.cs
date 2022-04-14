using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            ImportAssetFunction();
            ImportSeal();
            ImportManufacturer();
            ImportProductAssetModel();
            ImportAssetInfo();
            ImportAssetOwner();
            ImportAsset();


			//ImportBaseVoltages();
			//ImportLocations();
			//ImportPowerTransformers();
			//ImportTransformerWindings();
			//ImportWindingTests();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        private void ImportAsset()
        {
            SortedDictionary<string, object> cimAssets = concreteModel.GetAllObjectsOfType("FTN.Asset");
            if (cimAssets != null)
            {
                foreach (KeyValuePair<string, object> cimAssetsPairs in cimAssets)
                {
                    FTN.Asset cimAssset = cimAssetsPairs.Value as FTN.Asset;

                    ResourceDescription rd = CreateAssetResourceDescription(cimAssset);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Asset ID = ").Append(cimAssset.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Asset ID = ").Append(cimAssset.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateAssetResourceDescription(Asset cimAssset)
        {
            ResourceDescription rd = null;
            if (cimAssset != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSET, importHelper.CheckOutIndexForDMSType(DMSType.ASSET));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimAssset.ID, gid);

                PowerTransformerConverter.PopulateAssetProperties(cimAssset, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportAssetOwner()
        {
            SortedDictionary<string, object> cimAssetOwners = concreteModel.GetAllObjectsOfType("FTN.AssetOwner");
            if (cimAssetOwners != null)
            {
                foreach (KeyValuePair<string, object> cimAssetOwnerPairs in cimAssetOwners)
                {
                    FTN.AssetOwner cimAssetOwner = cimAssetOwnerPairs.Value as FTN.AssetOwner;

                    ResourceDescription rd = CreateAssetOwnerResourceDescription(cimAssetOwner);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("AssetOwner ID = ").Append(cimAssetOwner.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("AssetOwner ID = ").Append(cimAssetOwner.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateAssetOwnerResourceDescription(AssetOwner cimAssetOwner)
        {
            ResourceDescription rd = null;
            if (cimAssetOwner != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSETOWNER, importHelper.CheckOutIndexForDMSType(DMSType.ASSETOWNER));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimAssetOwner.ID, gid);

                PowerTransformerConverter.PopulateAssetOwnerProperties(cimAssetOwner , rd);
            }
            return rd;
        }

        private void ImportAssetInfo()
        {
            SortedDictionary<string, object> cimAssetInfos = concreteModel.GetAllObjectsOfType("FTN.AssetInfo");
            if (cimAssetInfos != null)
            {
                foreach (KeyValuePair<string, object> cimAssetInfoPairs in cimAssetInfos)
                {
                    FTN.AssetInfo cimAssetInfo = cimAssetInfoPairs.Value as FTN.AssetInfo;

                    ResourceDescription rd = CreateAssetInfoResourceDescription(cimAssetInfo);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("AssetInfo ID = ").Append(cimAssetInfo.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("AssetInfo ID = ").Append(cimAssetInfo.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateAssetInfoResourceDescription(AssetInfo cimAssetInfo)
        {
            ResourceDescription rd = null;
            if (cimAssetInfo != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSETINFO, importHelper.CheckOutIndexForDMSType(DMSType.ASSETINFO));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimAssetInfo.ID, gid);

                PowerTransformerConverter.PopulateAssetInfoProperties(cimAssetInfo, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportProductAssetModel()
        {
            SortedDictionary<string, object> cimProductAssetModels = concreteModel.GetAllObjectsOfType("FTN.ProductAssetModel");
            if (cimProductAssetModels != null)
            {
                foreach (KeyValuePair<string, object> cimProductAssetModelPairs in cimProductAssetModels)
                {
                    FTN.ProductAssetModel cimProductAssetModel = cimProductAssetModelPairs.Value as FTN.ProductAssetModel;

                    ResourceDescription rd = CreateProductAssetModelResourceDescription(cimProductAssetModel);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ProductAssetModel ID = ").Append(cimProductAssetModel.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ProductAssetModel ID = ").Append(cimProductAssetModel.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateProductAssetModelResourceDescription(ProductAssetModel cimProductAssetModel)
        {
            ResourceDescription rd = null;
            if (cimProductAssetModel != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PRODUCTASSETMODEL, importHelper.CheckOutIndexForDMSType(DMSType.PRODUCTASSETMODEL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimProductAssetModel.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateProductAssetModelProperties(cimProductAssetModel, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportManufacturer()
        {
            SortedDictionary<string, object> cimManufacturers = concreteModel.GetAllObjectsOfType("FTN.Manufacturer");
            if (cimManufacturers != null)
            {
                foreach (KeyValuePair<string, object> cimManufacturerPairs in cimManufacturers)
                {
                    FTN.Manufacturer cimManufacturer = cimManufacturerPairs.Value as FTN.Manufacturer;

                    ResourceDescription rd = CreateManufacturerResourceDescription(cimManufacturer);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Manufacturer ID = ").Append(cimManufacturer.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Manufacturer ID = ").Append(cimManufacturer.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateManufacturerResourceDescription(Manufacturer cimManufacturer)
        {
            ResourceDescription rd = null;
            if (cimManufacturer != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.MANUFACTURER, importHelper.CheckOutIndexForDMSType(DMSType.MANUFACTURER));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimManufacturer.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateManufacturerProperties(cimManufacturer, rd);
            }
            return rd;
        }

        private void ImportSeal()
        {
            SortedDictionary<string, object> cimSeals = concreteModel.GetAllObjectsOfType("FTN.Seal");
            if (cimSeals != null)
            {
                foreach (KeyValuePair<string, object> cimSealPairs in cimSeals)
                {
                    FTN.Seal cimSeal = cimSealPairs.Value as FTN.Seal;

                    ResourceDescription rd = CreateSealResourceDescription(cimSeal);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Seal ID = ").Append(cimSeal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Seal ID = ").Append(cimSeal.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateSealResourceDescription(Seal cimSeal)
        {
            ResourceDescription rd = null;
            if (cimSeal != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SEAL, importHelper.CheckOutIndexForDMSType(DMSType.SEAL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSeal.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateSealProperties(cimSeal, rd);
            }
            return rd;
        }

        private void ImportAssetFunction()
        {
            SortedDictionary<string, object> cimAssetFunctions = concreteModel.GetAllObjectsOfType("FTN.AssetFunction");
            if (cimAssetFunctions != null)
            {
                foreach (KeyValuePair<string, object> cimAssetFunctionPair in cimAssetFunctions)
                {
                    FTN.AssetFunction cimAssetFunction = cimAssetFunctionPair.Value as FTN.AssetFunction;

                    ResourceDescription rd = CreateAssetFunctionResourceDescription(cimAssetFunction);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("AssetFunction ID = ").Append(cimAssetFunction.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("AssetFunction ID = ").Append(cimAssetFunction.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateAssetFunctionResourceDescription(AssetFunction cimAssetFunction)
        {
            ResourceDescription rd = null;
            if (cimAssetFunction != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSETFUNCTION, importHelper.CheckOutIndexForDMSType(DMSType.ASSETFUNCTION));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimAssetFunction.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateAssetFunctionProperties(cimAssetFunction, rd);
            }
            return rd;
        }

	}
}

