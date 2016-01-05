using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Dal
{
    public class DalIoc:NinjectModule
    {
        public override void Load()
        {

            Bind<IDbContext>().To<DbContext>().InSingletonScope();
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind(typeof(IRepository<,>)).To(typeof(Repository<,>));

            Bind(typeof(IDbSet<>)).To(typeof(DbSet<>)).InSingletonScope();
        }
    }
}
