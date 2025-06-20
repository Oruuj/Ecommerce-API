using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductFeatureService, ProductFeatureService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IProductSliderService, ProductSliderService>();
            services.AddScoped<ISettingService, SettingService>();
            return services;
        }
    }
}
