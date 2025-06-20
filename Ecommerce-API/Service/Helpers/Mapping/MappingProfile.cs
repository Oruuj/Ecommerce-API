using AutoMapper;
using Domain.Entities;
using Service.DTOs.Account;
using Service.DTOs.CategoryDTOs;
using Service.DTOs.DiscountDTOs;
using Service.DTOs.ProductDTOs;
using Service.DTOs.ProductFeatureDTOs;
using Service.DTOs.ProductSliderDTOs;
using Service.DTOs.SettingDTOs;
using Service.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.DiscountProducts.Select(dp => dp.Discount)));
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();


            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();


            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<ProductFeature, ProductFeatureCreateDTO>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureUpdateDTO>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDTO>().ReverseMap();

            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscountCreateDTO>().ReverseMap();
            CreateMap<Discount, DiscountUpdateDTO>().ReverseMap();

            CreateMap<Slider, SliderDTO>().ReverseMap();
            CreateMap<Slider, SliderCreateDTO>().ReverseMap();
            CreateMap<Slider, SliderUpdateDTO>().ReverseMap();

            CreateMap<DiscountProduct, DiscountDTO>()
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.Discount.DiscountPercentage))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Discount.EndDate))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Discount.StartDate)).ReverseMap();
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();

            CreateMap<ProductSlider, ProductSliderCreateDTO>().ReverseMap();
            CreateMap<ProductSlider, ProductSliderUpdateDTO>().ReverseMap();
            CreateMap<ProductSlider, ProductSliderDTO>().ReverseMap();

            CreateMap<Setting, SettingCreateDTO>().ReverseMap();
            CreateMap<Setting, SettingUpdateDTO>().ReverseMap();
            CreateMap<Setting, SettingDTO>().ReverseMap();

        }
    }
}
