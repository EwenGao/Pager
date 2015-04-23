public static class PagerExtensions
    {
        public static MvcHtmlString Pager<T>(this HtmlHelper html, Pager<T> source) where T : class
        {
            var container = new TagBuilder(source.Container);

            if (source.RecordCount > 0)
            {

                container.AddCssClass(source.ClassName);
                var count = source.RecordCount / source.PageSize + (source.RecordCount % source.PageSize > 0 ? 1 : 0);
                if (source.AlwaysShow)
                {
                    container.InnerHtml = CreatePage(source, count);
                }
                else
                {
                    if (source.RecordCount / source.PageSize > 0)
                    {
                        container.InnerHtml = CreatePage(source, count);
                    }
                }
            }
            return MvcHtmlString.Create(container.ToString());
        }

        private static string CreatePage<T>(Pager<T> source, int count)
        {
            var page = new StringBuilder();
            #region [计算循环值]
            int last = source.Current;
            while (last % source.ShowNumber > 0)
                last += 1;
            if (last > count) last = count;
            int begin = last - (source.ShowNumber - 1);
            if (begin < 1) begin = 1;
            #endregion
            var more = new TagBuilder("a") { InnerHtml = "..." };
            var a = new TagBuilder("a");
            var url = HttpContext.Current.Request.Url;
            var location = url.ToString();
            if (!string.IsNullOrWhiteSpace(url.Query))
            {
                location = url.ToString().Replace(HttpContext.Current.Server.UrlDecode(url.Query), "");
            }
            var query = url.Query.Replace("&" + source.PageName + "=" + source.Current, "").Replace("?" + source.PageName + "=" + source.Current, ""); ;
            location += !string.IsNullOrWhiteSpace(query) ? query + "&" : "?";
            if (source.Current > 1)
            {
                a.InnerHtml = "上一页";
                a.Attributes["href"] = location + source.PageName + "=" + (source.Current - 1);
                page.Append(string.Format(source.PageFormat, a));
            }
            if (begin - source.ShowNumber > 0)
            {
                more.Attributes["href"] = location + source.PageName + "=" + (begin - 1);
                page.Append(string.Format(source.PageFormat, more));
            }
            for (var i = begin; i <= last; i++)
            {
                var pageLink = new TagBuilder("a");
                pageLink.Attributes.Add("href", location + source.PageName + "=" + i);
                if (i == source.Current)
                {
                    pageLink.AddCssClass("on");
                }
                pageLink.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                page.Append(i == source.Current
                    ? string.Format(source.CurreFormat, pageLink)
                    : string.Format(source.PageFormat, pageLink));
            }
            if (count - last > 0)
            {
                more.Attributes["href"] = location + source.PageName + "=" + (last + 1);
                page.Append(string.Format(source.PageFormat, more));
            }
            if (source.Current < count)
            {
                a.InnerHtml = "下一页";
                a.Attributes["href"] = location + source.PageName + "=" + (source.Current + 1);
                page.Append(string.Format(source.PageFormat, a));
            }
            return page.ToString();
        }
    }
