namespace ReclameAquiDoTrader.UI.Helpers
{
    public static class ArquivoHtmlHelper
    {
        public static string ClasseFaIconContentType(string contentType)
        {
            return contentType switch
            {
                "application/pdf" => "fa fa-file-pdf-o text-danger",
                "application/doc" => "fa fa-file-word-o text-success",
                "application/docx" => "fa fa-file-word-o text-success",
                "application/xls" => "fa fa-file-excel-o text-info",
                "application/xlsx" => "fa fa-file-excel-o text-info",
                _ => "fa fa-file-o",
            };

        }
    }
}
