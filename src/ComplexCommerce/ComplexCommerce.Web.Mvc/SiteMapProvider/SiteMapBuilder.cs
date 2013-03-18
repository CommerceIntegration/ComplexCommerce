﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Builder;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web.Mvc.SiteMapProvider
{
    public class SiteMapBuilder
        : ISiteMapBuilder
    {
        public SiteMapBuilder(
            ISiteMapPageTreeFactory siteMapPageTreeFactory,
            IApplicationContext appContext,
            ISiteMapNodeFactory siteMapNodeFactory
            )
        {
            if (siteMapPageTreeFactory == null)
                throw new ArgumentNullException("siteMapPageTreeFactory");
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (siteMapNodeFactory == null)
                throw new ArgumentNullException("siteMapNodeFactory");
            this.siteMapPageTreeFactory = siteMapPageTreeFactory;
            this.appContext = appContext;
            this.siteMapNodeFactory = siteMapNodeFactory;
        }

        private readonly ISiteMapPageTreeFactory siteMapPageTreeFactory;
        private readonly IApplicationContext appContext;
        private readonly ISiteMapNodeFactory siteMapNodeFactory;

        #region ISiteMapBuilder Members

        public ISiteMapNode BuildSiteMap(ISiteMap siteMap, ISiteMapNode rootNode)
        {
            // Set Properties
            siteMap.EnableLocalization = false;
            siteMap.SecurityTrimmingEnabled = true;
            
            var tenant = appContext.CurrentTenant;
            var localeId = appContext.CurrentLocaleId;

            var tree = siteMapPageTreeFactory.GetSiteMapPageTree(tenant.Id, localeId);

            // Get the root node
            var root = GetRootNode(siteMap, tree);

            ProcessTreeNodes(siteMap, root, tree);

            return root;
        }

        #endregion

        private void ProcessTreeNodes(ISiteMap siteMap, ISiteMapNode rootNode, SiteMapPageTree tree)
        {
            var parentNode = rootNode;
            foreach (var page in tree.ChildPages)
            {
                var childNode = GetSiteMapNodeFromTreeNode(siteMap, page);
                childNode.ParentNode = parentNode;

                siteMap.AddNode(childNode, parentNode);

                // Process next level
                ProcessTreeNodes(siteMap, childNode, page);
            }

            foreach (var product in tree.Products)
            {
                var productNode = GetSiteMapNodeFromProductInfo(siteMap, product);
                siteMap.AddNode(productNode, parentNode);
            }
        }

        private ISiteMapNode GetRootNode(ISiteMap siteMap, SiteMapPageTree tree)
        {
            // The root of the tree is the passed in SiteMapPageTree object
            return GetSiteMapNodeFromTreeNode(siteMap, tree);
        }

        private ISiteMapNode GetSiteMapNodeFromTreeNode(ISiteMap siteMap, SiteMapPageTree treeNode)
        {
            var key = treeNode.Id.ToString();
            var node = siteMapNodeFactory.Create(siteMap, key, "");

            // Assign values
            node.Title = treeNode.Title;
            node.Url = treeNode.RouteUrl;

            //// These aren't strictly necessary...
            //node.Controller = treeNode.ContentType.ToString();
            //node.Area = "";
            //node.Action = "Details";
            //if (!treeNode.ContentId.Equals(Guid.Empty))
            //{
            //    node.RouteValues.Add("id", treeNode.ContentId);
            //}
            
            // TODO: Figure out how to do canonical URLs for sitemapPageTree

            AcquireMetaRobotsValuesFrom(treeNode.MetaRobots, node.MetaRobotsValues);

            return node;
        }

        private ISiteMapNode GetSiteMapNodeFromProductInfo(ISiteMap siteMap, SiteMapProductInfo productInfo)
        {
            var key = productInfo.ProductXTenantLocaleId.ToString();
            var node = siteMapNodeFactory.Create(siteMap, key, "");

            // Assign values
            node.Title = productInfo.Name;
            node.Url = productInfo.RouteUrl;

            //// These aren't strictly necessary...
            //node.Controller = "Product";
            //node.Area = "";
            //node.Action = "Details";
            //node.RouteValues.Add("id", productInfo.ProductXTenantLocaleId);

            node.CanonicalUrl = productInfo.CanonicalRouteUrl;

            AcquireMetaRobotsValuesFrom(productInfo.MetaRobots, node.MetaRobotsValues);

            return node;
        }


        // TODO: Move this logic into CSLA object
        private void AcquireMetaRobotsValuesFrom(string metaRobots, IList<string> metaRobotsValues)
        {
            if (!string.IsNullOrEmpty(metaRobots))
            {
                var values = metaRobots.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var value in values)
                {
                    metaRobotsValues.Add(value.Trim());
                }
            }
        }
    }
}