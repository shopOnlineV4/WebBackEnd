using Domain.EF;
using Microsoft.EntityFrameworkCore;
using StructureMap;
using UnitOfWork;

namespace Api.Configuration
{
    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();

                    //This line of code is just directions telling StructureMap
                    //where to look for DAL models.
                    //Typically, my DAL code lives in different class library
                    scan.AssemblyContainingType<WebOnlineDbContext>();

                    //To connect implementations to our open generic type of IRepository,
                    //we use the ConnectImplementationsToTypesClosing method.
                    scan.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                });
            //For<IExample>().Use<Example>();

            //Here we resolve object instances of our DbContext and IRepository
            For<DbContext>().Use(ctx => new WebOnlineDbContext());
            For(typeof(IRepository<>)).Use(typeof(BaseRepository<>));
        }

        #endregion Constructors and Destructors
    }
}