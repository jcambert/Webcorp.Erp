using MahApps.Metro.Controls;
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
    internal static class FrameworkExtensions
    {
        static internal bool Is<T>(this FrameworkElement element)=>typeof(T).IsAssignableFrom(element.GetType());
        
    }

    public abstract class RegionAdapter<T> : RegionAdapterBase<T> where T : class{
        public RegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected abstract void Add(T regionTarget, FrameworkElement element);

        protected abstract void Remove(T regionTarget, UIElement element);

        protected override void Adapt(IRegion region, T regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                       
                        foreach (FrameworkElement element in e.NewItems)
                        {
                            Add(regionTarget, element);
                            /*if (element.Is<T>())
                            {
                                List<object> items = new List<object>();
                                foreach (var item in ((RibbonApplicationMenu)element).Items)
                                {
                                    items.Add(item);

                                }
                                foreach (var item in items)
                                {

                                    ((RibbonApplicationMenu)element).Items.Remove(item);
                                    //regionTarget.ApplicationMenu.Items.Add(item);
                                }
                                items.ForEach(item => regionTarget.ApplicationMenu.Items.Add(item));
                            }
                            else
                                regionTarget.Items.Add(element);*/
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (UIElement elementLoopVariable in e.OldItems)
                        {
                            var element = elementLoopVariable;
                            /*if (regionTarget.Items.Contains(element))
                            {
                                regionTarget.Items.Remove(element);
                            }*/
                            Remove(regionTarget, element);
                        }
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()=> new SingleActiveRegion();
    }

    public class RibbonRegionAdapter : RegionAdapter<Ribbon>
    {
        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }
        protected override void Add(Ribbon regionTarget, FrameworkElement element)
        {
            if (element.Is<RibbonApplicationMenu>())
            {
                List<object> items = new List<object>();
                foreach (var item in ((RibbonApplicationMenu)element).Items)
                {
                    items.Add(item);

                }
                foreach (var item in items)
                {

                    ((RibbonApplicationMenu)element).Items.Remove(item);
                    //regionTarget.ApplicationMenu.Items.Add(item);
                }
                items.ForEach(item => regionTarget.ApplicationMenu.Items.Add(item));
            }
            else
                regionTarget.Items.Add(element);
        }

        protected override void Remove(Ribbon regionTarget, UIElement element)
        {
            if (regionTarget.Items.Contains(element))
            {
                regionTarget.Items.Remove(element);
            }
        }
    }

    public class MetroAnimatedSingleRowTabControlAdapter : RegionAdapter<MetroAnimatedSingleRowTabControl>
    {
        public MetroAnimatedSingleRowTabControlAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }
        protected override void Add(MetroAnimatedSingleRowTabControl regionTarget, FrameworkElement element)
        {
            if (element.Is<MetroAnimatedSingleRowTabControl>())
            {
                List<object> items = new List<object>();
                foreach (var item in ((MetroAnimatedSingleRowTabControl)element).Items)
                {
                    items.Add(item);

                }
                foreach (var item in items)
                {

                    ((MetroAnimatedSingleRowTabControl)element).Items.Remove(item);
                    //regionTarget.ApplicationMenu.Items.Add(item);
                }
                items.ForEach(item => regionTarget.Items.Add(item));
            }
            else
                regionTarget.Items.Add(element);
        }

        protected override void Remove(MetroAnimatedSingleRowTabControl regionTarget, UIElement element)
        {
            if (regionTarget.Items.Contains(element))
            {
                regionTarget.Items.Remove(element);
            }
        }
    }
    /* public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
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

                             if(element is RibbonApplicationMenu)
                             {
                                 List<object> items = new List<object>();
                                 foreach (var item in ((RibbonApplicationMenu)element).Items)
                                 {
                                     items.Add(item);

                                 }
                                 foreach (var item in items)
                                 {

                                     ((RibbonApplicationMenu)element).Items. Remove(item);
                                     //regionTarget.ApplicationMenu.Items.Add(item);
                                 }
                                 items.ForEach(item => regionTarget.ApplicationMenu.Items.Add(item));
                             }
                             else
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
     }*/
}
