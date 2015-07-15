using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;
using WFM.Common;

namespace System.Web.Mvc.Html
{
    public static class HtmlExtendClass
    {
        public static MvcHtmlString DisplayDateAndWeek(this HtmlHelper html, DateTime day)
        {
            if (day == null)
            {
                day = DateTime.Now;
                return new MvcHtmlString(day.ToString("MM/dd")+"("+AppConst.Const_Weekday[Convert.ToInt16(day.DayOfWeek)]+")");
            }
            else
            {
                return new MvcHtmlString(day.ToString("MM/dd") + "(" + AppConst.Const_Weekday[Convert.ToInt16(day.DayOfWeek)] + ")");
            }
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, string codeCategory,string vTenantID,string vTenantFlag, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory, vTenantID, vTenantFlag);
            return ListControlUtil.GenerateHtml(name, codes, repeatDirection,"radio",null);
        }
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string nameSuffix,string codeCategory,string vTenantID,string vTenantFlag, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory, vTenantID, vTenantFlag);
          ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
          string name = ExpressionHelper.GetExpressionText(expression);
          string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name) + "_" + nameSuffix;
          return ListControlUtil.GenerateHtml(fullHtmlFieldName, codes, repeatDirection, "radio", metadata.Model);
      }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, string codeCategory, string vTenantID, string vTenantFlag, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
      {
          var codes = CodeManager.GetCodes(codeCategory, vTenantID, vTenantFlag);
          return ListControlUtil.GenerateHtml(name, codes, repeatDirection, "checkbox", null);
      }
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string nameSuffix, string codeCategory, string vTenantID, string vTenantFlag, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
      {
          var codes = CodeManager.GetCodes(codeCategory, vTenantID, vTenantFlag);
          ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
          string name = ExpressionHelper.GetExpressionText(expression);
          string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name) + "_" + nameSuffix;
          return ListControlUtil.GenerateHtml(fullHtmlFieldName, codes, repeatDirection, "checkbox", metadata.Model);
      }
    }
}