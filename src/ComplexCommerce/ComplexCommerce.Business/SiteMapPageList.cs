using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using CC = ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class SiteMapPageList
        : CC.ReadOnlyListBase<SiteMapPageList, SiteMapPageTree>
    {

        //public static SiteMapPageList EmptySiteMapPageHierarchy()
        //{
        //    return new SiteMapPageList();
        //}

        //public static SiteMapPageList GetSiteMapPageHierarchy()
        //{
        //    return DataPortal.Fetch<SiteMapPageList>();
        //}


        //private Dictionary<Guid, List<SiteMapPageDto>> GroupByParentId(List<SiteMapPageDto> list)
        //{
        //    var result = new Dictionary<Guid, List<SiteMapPageDto>>();

        //    List<SiteMapPageDto> tempList = null;

        //    foreach (var item in list)
        //    {
        //        if (result.TryGetValue(item.ParentId, out tempList))
        //        {
        //            tempList.Add(item);
        //        }
        //        else
        //        {
        //            result.Add(item.ParentId, new List<SiteMapPageDto>(new SiteMapPageDto[] { item }));
        //        }
        //    }

        //    return result;
        //}


        //// Used for root - move to page tree
        //private void DataPortal_Fetch()
        //{
        //    using (var ctx = ContextFactory.GetContext())
        //    {
        //        var rlce = RaiseListChangedEvents;
        //        RaiseListChangedEvents = false;
        //        IsReadOnly = false;

        //        var list = repository.List();


        //        foreach (var item in list)
        //            Add(DataPortal.FetchChild<SiteMapPageTree>(item));

        //        IsReadOnly = true;
        //        RaiseListChangedEvents = rlce;
        //    }
        //}

        //// Used for nested calls
        //private void Child_Fetch(IEnumerable<SiteMapPageDto> list)
        //{
        //    var rlce = RaiseListChangedEvents;
        //    RaiseListChangedEvents = false;
        //    IsReadOnly = false;
        //    foreach (var item in list)
        //        Add(DataPortal.FetchChild<SiteMapPageTree>(item, list));
        //    IsReadOnly = true;
        //    RaiseListChangedEvents = rlce;

        //    //using (var ctx = ContextFactory.GetContext())
        //    //{
        //    //    var data = repository.List(articleId);

        //    //    var rlce = RaiseListChangedEvents;
        //    //    RaiseListChangedEvents = false;
        //    //    foreach (var item in data)
        //    //        Add(DataPortal.FetchChild<ArticleAuthorEdit>(item));
        //    //    RaiseListChangedEvents = rlce;
        //    //}
        //}

        // Used for nested calls
        private void Child_Fetch(Guid parentId, IEnumerable<SiteMapPageDto> list)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            var childList = list.Where(x => x.ParentId == parentId);
            foreach (var item in childList)
            {
                Add(DataPortal.FetchChild<SiteMapPageTree>(item, list));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;

            //using (var ctx = ContextFactory.GetContext())
            //{
            //    var data = repository.List(articleId);

            //    var rlce = RaiseListChangedEvents;
            //    RaiseListChangedEvents = false;
            //    foreach (var item in data)
            //        Add(DataPortal.FetchChild<ArticleAuthorEdit>(item));
            //    RaiseListChangedEvents = rlce;
            //}
        }



        //#region Dependency Injection

        //[NonSerialized]
        //[NotUndoable]
        //private IPageRepository repository;
        //public IPageRepository Repository
        //{
        //    set
        //    {
        //        // Don't allow the value to be set to null
        //        if (value == null)
        //        {
        //            throw new ArgumentNullException("value");
        //        }
        //        // Don't allow the value to be set more than once
        //        if (this.repository != null)
        //        {
        //            throw new InvalidOperationException();
        //        }
        //        this.repository = value;
        //    }
        //}

        //#endregion
    }
}
