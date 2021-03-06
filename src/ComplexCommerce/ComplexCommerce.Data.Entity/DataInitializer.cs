﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using AutoMapper;
using ComplexCommerce.Data.Entity.Model;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.Entity
{
    public class DataInitializer
        : IDataInitializer
    {
        public DataInitializer()
        {
            // Configure all mappings between EF and DTO
            Mapper.CreateMap<Tenant, TenantDto>();
            Mapper.CreateMap<TenantDto, Tenant>();

            Mapper.CreateMap<Error, ErrorDto>();
            Mapper.CreateMap<ErrorDto, Error>();

            //Mapper.CreateMap<Article_Staged, StagedArticleDto>();
            //Mapper.CreateMap<StagedArticleDto, Article_Staged>();
            ////Mapper.CreateMap<Article, StagedArticleDto>();

            //Mapper.CreateMap<Article, ArticleDto>();
            //Mapper.CreateMap<ArticleDto, Article>();

            //Mapper.CreateMap<ArticleAuthor, ArticleAuthorDto>();
            //Mapper.CreateMap<ArticleAuthorDto, ArticleAuthor>();

            //Mapper.CreateMap<ArticleAuthor_Staged, StagedArticleAuthorDto>();
            //Mapper.CreateMap<StagedArticleAuthorDto, ArticleAuthor_Staged>();


            //Mapper.CreateMap<ViewTemplate, ViewTemplateDto>();
            //Mapper.CreateMap<ViewTemplateDto, ViewTemplate>();
        }

        //public static void Initialize()
        //{
        //    // Fires static constructor
        //}
    }
}
