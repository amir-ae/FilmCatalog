using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FilmCatalog.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public int ShownButtons = 5;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }
        public PagingInfo? PageModel { get; set; }
        public string? PageController { get; set; }
        public string? PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }
            = new Dictionary<string, object>();

        public bool PageClassesEnabled { get; set; } = false;
        public string? PageClass { get; set; }
        public string? PageClassNormal { get; set; }
        public string? PageClassSelected { get; set; }
        public string? PageClassPadding { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext == null || PageModel == null)
            {
                return;
            }
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            int currentPage = PageModel.CurrentPage;
            int totalPages = PageModel.TotalPages;
            int halfShownPages = (int)Math.Floor((double)ShownButtons / 2);

            if (currentPage > 1 + halfShownPages)
            {
                TagBuilder firstTag = CreateTag("1", 1, urlHelper);
                result.InnerHtml.AppendHtml(firstTag);
            }
            if (currentPage > 1 + ShownButtons)
            {
                int num = closestNumber(currentPage - ShownButtons, ShownButtons);
                TagBuilder fastPrevTag = CreateTag("<<<", num, urlHelper);
                result.InnerHtml.AppendHtml(fastPrevTag);
            }
            if (currentPage > 1)
            {
                TagBuilder prevTag = CreateTag("Prev page", currentPage - 1, urlHelper);
                result.InnerHtml.AppendHtml(prevTag); 
            }
            TagBuilder currentPageTag = CreateTag(currentPage.ToString(), currentPage, urlHelper);
            result.InnerHtml.AppendHtml(currentPageTag);
            if (currentPage < totalPages)
            {
                TagBuilder nextTag = CreateTag("Next page", currentPage + 1, urlHelper);
                result.InnerHtml.AppendHtml(nextTag);
            }
            if (currentPage < totalPages - ShownButtons)
            {
                int num = closestNumber(currentPage + ShownButtons, ShownButtons);
                TagBuilder fastNextTag = CreateTag(">>>", num, urlHelper);
                result.InnerHtml.AppendHtml(fastNextTag);
            }
            if (currentPage < totalPages - halfShownPages
                && totalPages > ShownButtons)
            {
                TagBuilder lastTag = CreateTag(totalPages.ToString(), totalPages, urlHelper);
                result.InnerHtml.AppendHtml(lastTag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }

        private TagBuilder CreateTag(string name, int pageNumber, IUrlHelper urlHelper, bool style = false)
        {
            TagBuilder tag = new TagBuilder("a");
            PageUrlValues[$"page"] = pageNumber;
            tag.Attributes["href"] = urlHelper.Action(PageAction, PageController, PageUrlValues);
            if (PageClassesEnabled)
            {
                if (PageClass != null)
                {
                    tag.AddCssClass(PageClass);
                }
                if (PageClassSelected != null && PageClassNormal != null && PageModel?.CurrentPage != null)
                {
                    tag.AddCssClass(pageNumber == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                if (PageClassPadding != null)
                {
                    tag.AddCssClass(PageClassPadding);
                }
            }
            tag.InnerHtml.Append(name);
            return tag;
        }

        private int closestNumber(int n, int m)
        {
            int q = n / m;

            int n1 = m * q;

            int n2 = (n * m) > 0 ? (m * (q + 1)) : (m * (q - 1));

            if (Math.Abs(n - n1) < Math.Abs(n - n2))
                return n1;

            return n2;
        }
    }
}
