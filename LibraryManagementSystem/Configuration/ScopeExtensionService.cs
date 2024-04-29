using LibraryManagementSystem.Repository.IRepository;
using LibraryManagementSystem.Repository.Repository;

namespace LibraryManagementSystem.Configuration
{
    public static class ScopeExtensionService
    {
        public static void ConfigureScopeExtension(this IServiceCollection services)
        {
            services.AddScoped<IAuthors, AuthorsRepository>();
            services.AddScoped<IBooks, BooksRepository>();
            services.AddScoped<IMembers, MembersRepository>();
        }
    }
}
