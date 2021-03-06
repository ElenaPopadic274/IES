using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

        ASSET = 0x0001,
        ASSETFUNCTION = 0x0002,
        ASSETOWNER = 0x0003,
        ASSETINFO = 0x0004,
        SEAL = 0x0005,
        MANUFACTURER = 0x0006,
        PRODUCTASSETMODEL = 0x0007,

        /* moj
         ASSETFUNCTION = 0x0001,
		ASSET = 0x0002,
		ASSETINFO = 0x0003,
		PRODUCTASSETMODEL = 0x0004,
		MANUFACTURER = 0x0005,
		ASSETOWNER = 0x0006,
		SEAL = 0x0007,
         */
    }

    [Flags]
	public enum ModelCode : long
	{
        //prvih osam brojeva je nasledjivanje
        //sledecih 4 je opis da li je klasa apstraktna ili ne, ako jeste ide 0000, ako je konkretna ide broj kojim je opisana iznad
        //poslednja 4 opisuju atribute: za klasu ide 0000
        IDOBJ = 0x1000000000000000,
		IDOBJ_GID							= 0x1000000000000104,
		IDOBJ_ALIASNAME 					= 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
		IDOBJ_NAME							= 0x1000000000000407,

        ASSETFUNCTION                       = 0x1100000000020000,
        ASSETFUNCTION_CONFIGID              = 0x1100000000020107,
        ASSETFUNCTION_FIRMWAREID            = 0x1100000000020207,
        ASSETFUNCTION_HARDWAREID            = 0x1100000000020307,
        ASSETFUNCTION_PASSWORD              = 0x1100000000020407,
        ASSETFUNCTION_PROGRAMID             = 0x1100000000020507,

        ORGANISATIONROLE                    = 0x1200000000000000,

        ASSET                               = 0x1300000000010000,
        ASSET_CRITICAL                      = 0x1300000000010101,
        ASSET_INITIALCONDITION              = 0x1300000000010207,
        ASSET_INITIALLOSSOFLIFE             = 0x1300000000010305,
        ASSET_LOTNUMBER                     = 0x1300000000010407,
        ASSET_PURCHASEPRICE                 = 0x1300000000010505,
        ASSET_SERIALNUMBER                  = 0x1300000000010607,
        ASSET_TYPE                          = 0x1300000000010707,
        ASSET_UTCNUMBER                     = 0x1300000000010807,
        ASSET_ASSETORGANISATIONROLE         = 0x1300000000010919,
        ASSET_ASSETINFO                     = 0x1300000000011009,

        ASSETINFO                           = 0x1400000000040000,
        ASSETINFO_ASSETMODEL                = 0x1400000000040109,
        ASSETINFO_ASSETS                    = 0x1400000000040219,

        ASSETMODEL                          = 0x1500000000000000,
        ASSETMODEL_ASSETINFO                = 0x1500000000000109,

        SEAL                                = 0x1600000000050000,
        SEAL_APPLIEDTIME                    = 0x1600000000050108,
        SEAL_CONDITION                      = 0x160000000005020A,
        SEAL_KIND                           = 0x160000000005030A,
        SEAL_SEALNUMBER                     = 0x1600000000050407,

        ASSETORGANISATIONROLE               = 0x1210000000000000,
        ASSETORGANISATIONROLE_ASSET         = 0x1210000000000119,

        ASSETOWNER                          = 0x1211000000030000,

        MANUFACTURER                        = 0x1220000000060000,
        MANUFACTURER_PRODUCTASSETMODEL      = 0x1220000000060119,

        PRODUCTASSETMODEL                   = 0x1510000000070000,
        PRODUCTASSETMODEL_CSTANDARDKIND     = 0x151000000007010A,
        PRODUCTASSETMODEL_MODELNUMBER       = 0x1510000000070207,
        PRODUCTASSETMODEL_MODELVERSION      = 0x1510000000070307,
        PRODUCTASSETMODEL_USAGEKIND         = 0x151000000007040A,
        PRODUCTASSETMODEL_WEIGHTTOTAL       = 0x1510000000070505,
        PRODUCTASSETMODEL_MANUFACTURER      = 0x1510000000070609,

        //      PSR									= 0x1100000000000000,
        //PSR_CUSTOMTYPE						= 0x1100000000000107,
        //PSR_LOCATION						= 0x1100000000000209,

        //BASEVOLTAGE							= 0x1200000000010000,
        //BASEVOLTAGE_NOMINALVOLTAGE			= 0x1200000000010105,
        //BASEVOLTAGE_CONDEQS					= 0x1200000000010219,

        //LOCATION							= 0x1300000000020000,
        //LOCATION_CORPORATECODE				= 0x1300000000020107,
        //LOCATION_CATEGORY					= 0x1300000000020207,
        //LOCATION_PSRS						= 0x1300000000020319,

        //WINDINGTEST							= 0x1400000000050000,
        //WINDINGTEST_LEAKIMPDN				= 0x1400000000050105,
        //WINDINGTEST_LOADLOSS				= 0x1400000000050205,
        //WINDINGTEST_NOLOADLOSS				= 0x1400000000050305,
        //WINDINGTEST_PHASESHIFT				= 0x1400000000050405,
        //WINDINGTEST_LEAKIMPDN0PERCENT		= 0x1400000000050505,
        //WINDINGTEST_LEAKIMPDNMINPERCENT		= 0x1400000000050605,
        //WINDINGTEST_LEAKIMPDNMAXPERCENT		= 0x1400000000050705,
        //WINDINGTEST_POWERTRWINDING			= 0x1400000000050809,

        //EQUIPMENT							= 0x1110000000000000,
        //EQUIPMENT_ISUNDERGROUND			= 0x1110000000000101,
        //EQUIPMENT_ISPRIVATE				= 0x1110000000000201,		

        //CONDEQ							= 0x1111000000000000,
        //CONDEQ_PHASES						= 0x111100000000010a,
        //CONDEQ_RATEDVOLTAGE				= 0x1111000000000205,
        //CONDEQ_BASVOLTAGE					= 0x1111000000000309,

        //POWERTR							= 0x1112000000030000,
        //POWERTR_FUNC						= 0x111200000003010a,
        //POWERTR_AUTO						= 0x1112000000030201,
        //POWERTR_WINDINGS					= 0x1112000000030319,

        //TRWINDING						    = 0x1111100000040000,
        //TRWINDING_CONNTYPE				    = 0x111110000004010a,
        //TRWINDING_GROUNDED				    = 0x1111100000040201,
        //TRWINDING_RATEDS				    = 0x1111100000040305,
        //TRWINDING_RATEDU				    = 0x1111100000040405,
        //TRWINDING_WINDTYPE				    = 0x111110000004050a,
        //TRWINDING_PHASETOGRNDVOLTAGE	    = 0x1111100000040605,
        //TRWINDING_PHASETOPHASEVOLTAGE	    = 0x1111100000040705,
        //TRWINDING_POWERTRW				    = 0x1111100000040809,
        //TRWINDING_TESTS				        = 0x1111100000040919,
    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


