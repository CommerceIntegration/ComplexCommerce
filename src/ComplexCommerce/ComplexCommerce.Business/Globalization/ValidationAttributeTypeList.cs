//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.DataAnnotations;
//using System.Threading.Tasks;
//using Csla;
//using ComplexCommerce.Csla;

// TODO: Make this into a list of types that can be selected from and/or joined to on the UI
// administration screen.


//namespace ComplexCommerce.Business.Globalization
//{
//    [Serializable]
//    public class ValidationAttributeTypeList
//        : CslaReadOnlyListBase<ValidationAttributeTypeList, ValidationAttributeTypeInfo>
//    {
//        private void DataPortal_Fetch(Criteria criteria)
//        {

//            var baseAttribute = typeof(ValidationAttribute);

//            //    var prompts = new List<TypePrompt>();

//            //    var baseAttribte = typeof(ValidationAttribute);
//            //    var attributes =
//            //        typeof(RequiredAttribute).Assembly.GetTypes().Where(
//            //            p => baseAttribte.IsAssignableFrom(p) && !p.IsAbstract).ToList();
//            //    foreach (var type in attributes)
//            //    {
//            //        var key = new TypePromptKey(type.FullName, "class");
//            //        var typePrompt = new TypePrompt
//            //        {
//            //            Key = key,
//            //            LocaleId = CultureInfo.CurrentUICulture.LCID,
//            //            TypeFullName = type.FullName,
//            //            TextName = "class",
//            //            UpdatedAt = DateTime.Now,
//            //            UpdatedBy = Thread.CurrentPrincipal.Identity.Name
//            //        };

//            //        var value = GetString(type, culture);
//            //        if (value != null)
//            //        {
//            //            // TODO: Need to work out default culture....?
//            //            //typePrompt.TranslatedText = DefaultUICulture.IsActive ? value : "";
//            //            typePrompt.TranslatedText = value;
//            //        }

//            //        prompts.Add(typePrompt);
//            //    }

//            //    return prompts;


//            //using (var ctx = ContextFactory.GetContext())
//            //{
//            //    var rlce = RaiseListChangedEvents;
//            //    RaiseListChangedEvents = false;
//            //    IsReadOnly = false;

//            //    var list = repository
//            //        .List(criteria.TenantId, criteria.LocaleId, criteria.HashCode, criteria.VirtualPath)
//            //        .OrderBy(x => x.LocaleId == criteria.LocaleId ? 0 : 1); // Requested locale first, default locale second
//            //    var textNamesAdded = new List<string>();

//            //    foreach (var item in list)
//            //    {
//            //        if (item.LocaleId == criteria.LocaleId)
//            //        {
//            //            Add(DataPortal.FetchChild<ViewTextLocaleInfo>(item));
//            //            textNamesAdded.Add(item.TextName);
//            //        }
//            //        else
//            //        {
//            //            if (!textNamesAdded.Contains(item.TextName))
//            //            {
//            //                // Format phrase for non-default locale
//            //                item.Value = string.Format("{1}:[{0}]", item.Value, new CultureInfo(criteria.LocaleId));
//            //                Add(DataPortal.FetchChild<ViewTextLocaleInfo>(item));
//            //            }
//            //        }
//            //    }

//            //    IsReadOnly = true;
//            //    RaiseListChangedEvents = rlce;
//            //}
//        }
//    }
//}
