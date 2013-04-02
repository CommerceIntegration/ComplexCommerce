using System;

namespace ComplexCommerce.Business.Text
{
    public interface IUrlBuilder
    {
        string BuildPath(string url, bool isUrlAbsolute, Guid parentPageId, ITenantLocale tenantLocale);
        string BuildPath(string url, bool isUrlAbsolute, Guid parentPageId, int tenantId, int localeId, int defaultLocaleId);
    }
}
