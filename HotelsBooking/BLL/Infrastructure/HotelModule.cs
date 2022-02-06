using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    public class HotelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("MyHotel");
        }
    }
}
