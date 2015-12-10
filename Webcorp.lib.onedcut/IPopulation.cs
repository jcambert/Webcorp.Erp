using GAF;
using ReactiveUI;
using System.Collections.Generic;
using Webcorp.Model;

namespace Webcorp.lib.onedcut
{
    public interface IPopulation
    {
        // Population Population { get; }
        ReactiveList<BeamToCut> Beams { get; }
        ReactiveList<BeamStock> CuttingStock { get; }
        Beam Beam { get; }
    }



}