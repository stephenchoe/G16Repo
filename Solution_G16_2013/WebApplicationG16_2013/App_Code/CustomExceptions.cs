using G16_2013.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;



namespace WebApplicationG16_2013.UserIdentity
{
    public class DuplicateRoleException : Exception
    {
        public DuplicateRoleException(string message)
            : base(message)
        {

        }
    }//end class
    public class EmptyRoleNameException : Exception
    {
        public EmptyRoleNameException(string message)
            : base(message)
        {

        }
    }//end class
    public class WrongRoleNameException : Exception
    {
        public WrongRoleNameException(string message)
            : base(message)
        {

        }
    }//end class
    public class AddRoleFailedException : Exception
    {
        public AddRoleFailedException(string message)
            : base(message)
        {

        }
    }//end class
    public class AddUserFailedException : Exception
    {
        public AddUserFailedException(string message)
            : base(message)
        {

        }
    }//end class
    public class AddUserToRoleFailedException : Exception
    {
        public AddUserToRoleFailedException(string message)
            : base(message)
        {

        }
    }//end class
}

//namespace WebApplicationG16_2013.MvcHtmlHelpers
//{
//    public static class HtmlHelperExtensions
//    {
//        public static MvcHtmlString EditorForContactInfo<TModel>(
//    this HtmlHelper<TModel> helper, Expression<Func<TModel, AccountContactInfoInputModel>> getter)
//        {
//            return GetPartial(helper, "_contactInfo", getter);
//        }

     
//    //    private static MvcHtmlString GetPartial<TRootModel, TModelForPartial>(
//    //HtmlHelper<TRootModel> helper, string partialName, Expression<Func<TRootModel, TModelForPartial>> getter)
//    //    {
//    //        var prefix = ExpressionHelper.GetExpressionText(getter);
           
//    //        return helper.Partial(partialName, getter.Compile().Invoke(helper.ViewData.Model),
//    //            new ViewDataDictionary { TemplateInfo = new TemplateInfo { HtmlFieldPrefix = prefix } });
//    //    }

//        private static MvcHtmlString GetPartial<TRootModel, TModelForPartial>(HtmlHelper<TRootModel> helper, string partialName, Expression<Func<TRootModel, TModelForPartial>> getter)
//        {
//            var prefix = ExpressionHelper.GetExpressionText(getter);
//            return helper.Partial(partialName, getter.Compile().Invoke(helper.ViewData.Model), new ViewDataDictionary { TemplateInfo = new TemplateInfo { HtmlFieldPrefix = prefix } });
//        }
//        public static MvcHtmlString EditorForNameModel<TModel>(
//    this HtmlHelper<TModel> helper, Expression<Func<TModel, AccountContactInfoInputModel>> getter)
//        {
//            return GetPartial(helper, "_contactInfo", getter);
//        }

     


//    }
//}
