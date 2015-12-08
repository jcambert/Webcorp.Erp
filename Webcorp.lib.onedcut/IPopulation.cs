using GAF;
using ReactiveUI;
using System.Collections.Generic;

namespace Webcorp.lib.onedcut
{
    public interface IPopulation
    {
        // Population Population { get; }
        ReactiveList<Beam> Beams { get; }
        ReactiveList<BeamStock> CuttingStock { get; }
    }



}