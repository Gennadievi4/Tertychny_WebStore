﻿using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entitys;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        private readonly DbInMemory _Db;
        public InMemoryProductData(IEmployeesData db) => _Db = (DbInMemory)db;
        public IEnumerable<Brand> GetBrands() => _Db.Brands;
        public IEnumerable<Section> GetSections() => _Db.Sections;
        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = _Db.Products;

            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.BrandId);

            return query;
        }
    }
}
