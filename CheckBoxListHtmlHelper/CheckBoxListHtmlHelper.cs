using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace CheckBoxListHtmlHelper
{
    public static class CheckBoxListHtmlHelper
    {
        public static MvcHtmlString CheckBoxList<TModel>(this HtmlHelper<TModel> htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxList(htmlHelper, name, selectList, null);
        }

        public static MvcHtmlString CheckBoxList<TModel>(this HtmlHelper<TModel> htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Invalid argument", "name");
            }

            if (selectList == null)
                selectList = new List<SelectListItem>();

            var dictionaryHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            StringBuilder builder = new StringBuilder(CreateCheckBoxListWrapper(dictionaryHtmlAttributes));

            foreach (var el in selectList)
            {
                builder.Append(CreateCheckBoxListElement(el, name));
            }

            builder.Append(GetCheckBoxListWrapperClosingTag());

            return MvcHtmlString.Create(builder.ToString());
        }
        
        #region helper methods

        private static string CreateCheckBoxListWrapper(IDictionary<string, object> htmlAttributes)
        {
            TagBuilder wrapper = new TagBuilder("div");

            if(htmlAttributes != null)
                wrapper.MergeAttributes(htmlAttributes);

            wrapper.AddCssClass("check-box-list-wrapper");


            return wrapper.ToString(TagRenderMode.StartTag);
        }

        private static string GetCheckBoxListWrapperClosingTag()
        {
            return new TagBuilder("div").ToString(TagRenderMode.EndTag);
        }

        private static string CreateCheckBoxListElement(SelectListItem item, string name)
        {
            StringBuilder element = new StringBuilder(CreateCheckBoxListElementWrapper());

            element.Append(CreateCheckBox(item, name));
            element.Append(" ");
            element.Append(item.Text);
            element.Append(GetCheckBoxListElementWrapperClosingTag());

            return element.ToString();
        }

        private static string CreateCheckBoxListElementWrapper()
        {
            TagBuilder wrapper = new TagBuilder("div");
            wrapper.AddCssClass("check-box-list-span-wrapper");

            return wrapper.ToString(TagRenderMode.StartTag);
        }

        private static string CreateCheckBox(SelectListItem item, string name)
        {
            TagBuilder checkbox = new TagBuilder("input");

            checkbox.MergeAttribute("type", "checkbox");
            checkbox.MergeAttribute("value", item.Value);
            checkbox.MergeAttribute("name", name);

            if (item.Selected)
                checkbox.MergeAttribute("checked", "checked");

            checkbox.AddCssClass("check-box-list-checkbox");

            return checkbox.ToString(TagRenderMode.SelfClosing);
        }

        private static string GetCheckBoxListElementWrapperClosingTag()
        {
            return new TagBuilder("div").ToString(TagRenderMode.EndTag);
        }

        #endregion
    }
}
