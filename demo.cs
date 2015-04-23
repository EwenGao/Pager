public ActionResult Pdf(int pageIndex = 1)
        {
            int total;
            var first = HrClient.GetArticleDataByType(out total, 2, 1, 1, 2).FirstOrDefault();
            ViewData["first"] = first;
            var model = GetArticles(2, pageIndex, 8, 10, out total, 1);
            var page = new Pager<Article>(model)
            {
                RecordCount = total,
                PageName = "pageIndex",
                Current = pageIndex,
                PageSize = 8,
                ShowNumber = 8
            };
            return View(page);
        }
