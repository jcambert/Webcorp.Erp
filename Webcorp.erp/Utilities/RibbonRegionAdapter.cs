using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;

namespace Webcorp.erp.Utilities
{
    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {

        public RibbonRegionAdapter(IRegionBehaviorFactory behaviorFactory):base(behaviorFactory)
        {

        }
        protected override void Adapt(IRegion region, Ribbon regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (FrameworkElement element in e.NewItems)
                        {
                            regionTarget.Items.Add(element);
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (UIElement elementLoopVariable in e.OldItems)
                        {
                            var element = elementLoopVariable;
                            if (regionTarget.Items.Contains(element))
                            {
                                regionTarget.Items.Remove(element);
                            }
                        }
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            //return new SingleActiveRegion();
            return new AllActiveRegion();
        }
    }
}
