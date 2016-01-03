using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Business
{
    public static class Helper
    {


        public static IKernel CreateKernel(params NinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
            return _kernel;
        }

        private static IKernel _kernel;
        public static IKernel Kernel { get { if (_kernel.IsNull()) CreateKernel(); return _kernel; } }


        public static async Task CreateStandardPParametres(bool clearAll = false)
        {
            IRepository<PParametre> repo = Kernel.Get<IRepository<PParametre>>();
            var ps = new PParametresInitializer().StandardPParametres();
            Task t = Task.Factory.StartNew(async () =>
            {
                if (clearAll) await repo.DeleteAll();

                await ps.ForEachAsync(p => repo.Upsert(p));

            });

            await t;

        }

        public static async Task CreateStandardParametres(bool clearAll = false)
        {
            var ph = Kernel.Get<IParametreBusinessHelper<Parametre>>();
            var pps = new PParametresInitializer().StandardPParametres();
            var ps = new ParametresInitializer().StandardParametres();
            if (clearAll)
                await ph.DeleteAll();
            await ps.ForEachAsync(async p =>
            {
                await ph.Create(pps.Where(x => x.Id == p.id).FirstOrDefault(), p.value);

            });

            await ph.Save();
        }

        public static string CurrentSociete
        {
            get
            {
                var auth = Kernel.Get<IAuthenticationService>();
                return auth.Utilisateur.Societe;

            }
        }

        public static string CurrentUser
        {
            get
            {
                var auth = Kernel.Get<IAuthenticationService>();
                return string.Format("{0} {1}", auth.Utilisateur.Nom ,auth.Utilisateur.Prenom);

            }
        }
    }
}
