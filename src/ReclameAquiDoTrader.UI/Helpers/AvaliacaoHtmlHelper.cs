namespace ReclameAquiDoTrader.UI.Helpers
{
    public static class AvaliacaoHtmlHelper
    {
        public static string ClasseCheckCircle(bool respondido)
        {
            if (respondido)
                return "texto-sucesso fa fa-check-circle-o";
            return "text-danger fa fa-times-circle-o";
        }

        public static string ClasseJoinha(bool valor)
        {
            if (valor)
                return "texto-sucesso fa fa-thumbs-o-up";
            return "text-danger fa fa-thumbs-o-down";
        }
    }
}
