using MedOrders.Web.Domain;
using MedOrders.Web.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedOrders.Web.Services;

/// <summary>Чтение каталога из базы: списки для фильтров, поиск товаров и карточка товара.</summary>
public sealed class CatalogService(AppDbContext db)
{
    /// <summary>Возвращает категории по алфавиту.</summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task<IReadOnlyList<SelectItem>> GetCategoriesAsync(CancellationToken cancellationToken = default) =>
        await db.Categories.AsNoTracking()
            .OrderBy(category => category.Name)
            .Select(category => new SelectItem(category.Id, category.Name))
            .ToListAsync(cancellationToken);

    /// <summary>Возвращает производителей по алфавиту.</summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task<IReadOnlyList<SelectItem>> GetManufacturersAsync(CancellationToken cancellationToken = default) =>
        await db.Manufacturers.AsNoTracking()
            .OrderBy(manufacturer => manufacturer.Name)
            .Select(manufacturer => new SelectItem(manufacturer.Id, manufacturer.Name))
            .ToListAsync(cancellationToken);

    /// <summary>Ищет товары по условиям фильтра и возвращает одну страницу результатов.</summary>
    /// <param name="filter">Условия поиска и страница.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task<CatalogResult> SearchProductsAsync(CatalogFilter filter, CancellationToken cancellationToken = default)
    {
        var filteredQuery = ApplyFilter(db.Products.AsNoTracking().Where(product => product.IsActive), filter);
        var totalCount = await filteredQuery.CountAsync(cancellationToken);

        var products = await ApplySorting(filteredQuery, filter.SortOrder)
            .Include(product => product.Certificates)
            .Include(product => product.Stocks)
            .AsSplitQuery()
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        var cards = products.Select(ToProductCard).ToList();
        return new CatalogResult(cards, totalCount, filter.PageNumber, filter.PageSize);
    }

    /// <summary>Возвращает карточку активного товара.</summary>
    /// <param name="productId">Идентификатор товара.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карточка или null, если товар не найден или снят с продажи.</returns>
    public async Task<ProductDetails?> GetProductDetailsAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await db.Products.AsNoTracking()
            .Include(item => item.Category)
            .Include(item => item.Manufacturer)
            .Include(item => item.Stocks)
            .AsSplitQuery()
            .FirstOrDefaultAsync(item => item.Id == productId && item.IsActive, cancellationToken);

        if (product is null)
        {
            return null;
        }

        return new ProductDetails(
            product.Id,
            product.Article,
            product.Name,
            product.Category?.Name ?? string.Empty,
            product.Manufacturer?.Name ?? string.Empty,
            product.Price,
            product.WarrantyMonths,
            product.ShelfLifeMonths,
            product.StorageConditions,
            product.RegistrationNumber,
            product.Stocks.Sum(stock => stock.Available),
            product.Specifications);
    }

    private static IQueryable<Product> ApplyFilter(IQueryable<Product> query, CatalogFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SearchText))
        {
            var searchText = filter.SearchText.Trim();
            query = query.Where(product => product.Name.Contains(searchText) || product.Article.Contains(searchText));
        }

        if (filter.CategoryId is { } categoryId)
        {
            query = query.Where(product => product.CategoryId == categoryId);
        }

        if (filter.ManufacturerId is { } manufacturerId)
        {
            query = query.Where(product => product.ManufacturerId == manufacturerId);
        }

        if (filter.MinPrice is { } minPrice)
        {
            query = query.Where(product => product.Price >= minPrice);
        }

        if (filter.MaxPrice is { } maxPrice)
        {
            query = query.Where(product => product.Price <= maxPrice);
        }

        if (filter.OnlyInStock)
        {
            query = query.Where(product => product.Stocks.Sum(stock => stock.Quantity - stock.ReservedQuantity) > 0);
        }

        if (filter.OnlyWithRegistration)
        {
            query = query.Where(product =>
                product.Certificates.Any(certificate => certificate.DocType == CertificateType.RegistrationCertificate));
        }

        return query;
    }

    private static IQueryable<Product> ApplySorting(IQueryable<Product> query, CatalogSortOrder sortOrder) =>
        sortOrder switch
        {
            CatalogSortOrder.PriceAscending => query.OrderBy(product => product.Price),
            CatalogSortOrder.PriceDescending => query.OrderByDescending(product => product.Price),
            _ => query.OrderBy(product => product.Name),
        };

    private static ProductCard ToProductCard(Product product) => new(
        product.Id,
        product.Article,
        product.Name,
        product.Price,
        product.Stocks.Sum(stock => stock.Available),
        product.Certificates.Any(certificate => certificate.DocType == CertificateType.RegistrationCertificate),
        product.Certificates.Any(certificate => certificate.DocType == CertificateType.ConformityCertificate));
}
