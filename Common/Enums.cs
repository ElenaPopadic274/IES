using System;

namespace FTN.Common
{	


	public enum PhaseCode : short
	{
		Unknown = 0x0,
		N = 0x1,
		C = 0x2,
		CN = 0x3,
		B = 0x4,
		BN = 0x5,
		BC = 0x6,
		BCN = 0x7,
		A = 0x8,
		AN = 0x9,
		AC = 0xA,
		ACN = 0xB,
		AB = 0xC,
		ABN = 0xD,
		ABC = 0xE,
		ABCN = 0xF
	}
	
	public enum TransformerFunction : short
	{
		Supply = 1,				// Supply transformer
		Consumer = 2,			// Transformer supplying a consumer
		Grounding = 3,			// Transformer used only for grounding of network neutral
		Voltreg = 4,			// Feeder voltage regulator
		Step = 5,				// Step
		Generator = 6,			// Step-up transformer next to a generator.
		Transmission = 7,		// HV/HV transformer within transmission network.
		Interconnection = 8		// HV/HV transformer linking transmission network with other transmission networks.
	}
	
	public enum WindingConnection : short
	{
		Y = 1,		// Wye
		D = 2,		// Delta
		Z = 3,		// ZigZag
		I = 4,		// Single-phase connection. Phase-to-phase or phase-to-ground is determined by elements' phase attribute.
		Scott = 5,   // Scott T-connection. The primary winding is 2-phase, split in 8.66:1 ratio
		OY = 6,		// 2-phase open wye. Not used in Network Model, only as result of Topology Analysis.
		OD = 7		// 2-phase open delta. Not used in Network Model, only as result of Topology Analysis.
	}

	public enum WindingType : short
	{
		None = 0,
		Primary = 1,
		Secondary = 2,
		Tertiary = 3
	}

    public enum SealConditionKind
    {
        /// Seal is broken.
        Broken = 0,
        Locked = 1,
        Missing = 2,
        Open = 3,
        Other = 4,
    }

    public enum SealKind
    {
        Lead = 0,
        Lock = 1,
        Steel = 2,
        Other = 3,
    }

    public enum CorporateStandardKind
    {

        Experimental = 0,
        Standard = 1,
        UnderEvaluation = 2,
        Other = 3,
    }

    public enum AssetModelUsageKind
    {
        /// Asset model is intended for use in customer substation.
        CustomerSubstation = 0,
        /// Asset model is intended for use in distribution overhead network.
        DistributionOverhead = 1,
        /// Asset model is intended for use in underground distribution network.
        DistributionUnderground = 2,
        /// Other kind of asset model usage.
        Other = 3,
        /// Asset model is intended for use as streetlight.
        Streetlight = 4,
        /// Asset model is intended for use in substation.
        Substation = 5,
        /// Asset model is intended for use in transmission network.
        Transmission = 6,
        /// Usage of the asset model is unknown.
        Unknown = 7,
    }
}
