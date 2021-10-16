using ReclameAquiDoTrader.Business.Enums;

namespace ReclameAquiDoTrader.UI.Helpers
{
    public static class RedeSocialHtmlHelper
    {
        public static string ClasseRedeSocial(ERedeSocialTipo tipo)
        {
            return tipo switch
            {
                ERedeSocialTipo.FACEBOOK => "fa fa-facebook-square",
                ERedeSocialTipo.INSTAGRAM => "fa fa-instagram",
                _ => "fa fa-question-circle",
            };
        }

        public static string ClasseBtnRedeSocial(ERedeSocialTipo tipo)
        {
            return tipo switch
            {
                ERedeSocialTipo.FACEBOOK => "btn btn-social-icon btn-facebook",
                ERedeSocialTipo.INSTAGRAM => "btn btn-social-icon btn-instagram",
                _ => string.Empty,
            };
        }
    }
}
