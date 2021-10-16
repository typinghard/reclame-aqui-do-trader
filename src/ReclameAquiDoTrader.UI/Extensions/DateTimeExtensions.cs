using System;

namespace ReclameAquiDoTrader.UI.Extensions
{
    public static class DateTimeExtensions
    {
        public static string TempoTras(this DateTime? dateTime)
        {
            return TempoTras(dateTime.GetValueOrDefault());
        }
        public static string TempoTras(this DateTime dateTime)
        {
            var now = DateTime.Now.ToUniversalTime();
            var timeSpan = now.Subtract(dateTime);
            if (timeSpan <= TimeSpan.FromSeconds(60))
                return string.Format("{0} segundos atrás", timeSpan.Seconds);

            if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ?
                    string.Format("Cerca de {0} minutos atrás", timeSpan.Minutes) :
                    "Cerca de um minuto atrás";

            if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ?
                    string.Format("Cerca de {0} horas atrás", timeSpan.Hours) :
                    "Cerca de uma hora atrás";

            timeSpan = now.Date.Subtract(dateTime.Date);

            if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ?
                    string.Format("{0} dias atrás", timeSpan.Days) :
                    "Ontem";

            if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ?
                    string.Format("{0} meses atrás", timeSpan.Days / 30) :
                    "Um mês atrás";

            return timeSpan.Days > 365 ?
                string.Format("Cerca de {0} anos atrás", timeSpan.Days / 365) :
                "Cerca de um ano atrás";
        }

        public static string FormataData(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string FormataDataSemHora(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
