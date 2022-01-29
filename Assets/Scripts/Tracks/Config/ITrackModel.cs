using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackModel 
{
    List<int> Tracks01 { get;  }
    List<int> Tracks02 { get;  }
    List<int> Rewards01 { get;  }
    List<int> Rewards02 { get;  }
    TracksConfig Config { get; }
}
