using System;
using System.Configuration;
using ReactiveUI;

namespace Webcorp.lib.onedcut
{
    internal class StandardSolverParameter : ReactiveUI.ReactiveObject, IDisposable, ISolverParameter
    {
        bool isChanged = false;
        public StandardSolverParameter()
        {
            //this.Config = ConfigurationManager.GetSection("AppSettings");// OpenExeConfiguration(ConfigurationUserLevel.None);

            if (System.Web.HttpContext.Current != null && !System.Web.HttpContext.Current.Request.PhysicalPath.Equals(string.Empty))
                this.Config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            else
                this.Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.Changed.Subscribe(_ => { isChanged = true; });
            Config.GetValue<double>("CrossoverProbability", ref _crossover, 0.85);
            Config.GetValue<int>("ElitePercentage", ref _elite, 2);
            Config.GetValue<int>("InitialPopulationCount", ref _popCount, 50);
            Config.GetValue<int>("MaxEvaluation", ref _max, 50);
            Config.GetValue<double>("MutationProbability", ref _mutation, 0.001);
        }

        public Configuration Config { get; private set; }

        double _crossover;
        public double CrossoverProbability
        {
            get { return _crossover; }
            set { this.RaiseAndSetIfChanged(ref _crossover, value); Config.SetValue(_crossover); }
        }


        int _elite;
        public int ElitePercentage
        {
            get { return _elite; }
            set { this.RaiseAndSetIfChanged(ref _elite, value);Config.SetValue(_elite); }
        }

        int _popCount;
        public int InitialPopulationCount {
            get { return _popCount; }
            set { this.RaiseAndSetIfChanged(ref _popCount, value); Config.SetValue(_popCount); }
        }

        int _max;
        public int MaxEvaluation
        {
            get { return _max; }
            set { this.RaiseAndSetIfChanged(ref _max, value);Config.SetValue(_max); }
        }

        double _mutation;
        public double MutationProbability
        {
            get { return _mutation; }
            set { this.RaiseAndSetIfChanged(ref _mutation, value);Config.SetValue(_mutation); }
        }

        public void Save()
        {
            
            Config.Save(ConfigurationSaveMode.Modified);
        }
        public void Dispose()
        {
            if (isChanged)
                Config.Save(ConfigurationSaveMode.Modified);
        }
    }
}