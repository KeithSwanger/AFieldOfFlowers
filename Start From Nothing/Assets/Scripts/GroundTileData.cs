using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType { Ground, Water }

public class GroundTileData
{
    public GroundType groundType = GroundType.Ground;
    public float primaryGrowth = 0f;
    public bool primaryGrowthComplete = false;

    public float secondaryGrowth = 0f;
    public bool secondaryGrowthComplete = false;

    public GroundTileData(GroundType type)
    {
        groundType = type;
    }

    public int AddGrowth(float growth)
    {
        //Primary Growth
        if (!primaryGrowthComplete)
        {
            primaryGrowth += growth;

            if (primaryGrowth >= 1f)
            {
                primaryGrowth = 1f;
                primaryGrowthComplete = true;
                return 1;
            }
        }
        else if(primaryGrowthComplete && !secondaryGrowthComplete)
        {
            secondaryGrowth += growth;
            if (secondaryGrowth >= 1f)
            {
                secondaryGrowth = 1f;
                secondaryGrowthComplete = true;
                return 2;

            }
        }

        return 0;
    }
}
