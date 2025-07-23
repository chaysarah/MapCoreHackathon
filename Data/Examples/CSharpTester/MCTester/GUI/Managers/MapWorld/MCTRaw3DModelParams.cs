using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapCore;

namespace MCTester.Managers.MapWorld
{
    public class MCTRaw3DModelParams
    {
        public bool IsUseIndexing;
        public bool OrthometricHeights;
        public bool IsUseNonDefaultIndexDirectory;
        public String NonDefaultIndexingDataDirectory;
        public DNSMcBox? pClipRect;
        public IDNMcGridCoordinateSystem pTargetCoordinateSystem;
        public float fTargetHighestResolution;
        public DNSMcKeyStringValue[] aRequestParams;
        public DNSMcVector3D PositionOffset;

        public MCTRaw3DModelParams()
        {
            OrthometricHeights = new DNS3DModelConvertParams().bOrthometricHeights;
            fTargetHighestResolution = new DNS3DModelConvertParams().fTargetHighestResolution;
            PositionOffset = DNSMcVector3D.v3Zero;
        }
    }

    public class MCTBuildIndexingParams : MCTRaw3DModelParams
    {
        public bool IsUseExisting;
        public String RawDataSourceDirectory;
        public IDNMcGridCoordinateSystem pSourceCoordinateSystem;
        public bool IsSelectTilingScheme;
        public DNSTilingScheme pTilingScheme;

        public MCTBuildIndexingParams() : base()
        {
        }
    }
}
