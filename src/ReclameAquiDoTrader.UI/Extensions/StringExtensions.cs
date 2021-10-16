using System;
using System.Globalization;
using System.Text;

namespace ReclameAquiDoTrader.UI.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string FormataTelefone(this string telefone)
        {
            var ddd = telefone.Substring(0, 2);
            var numeroAux = telefone.Substring(ddd.Length);
            var indiceTraco = numeroAux.Length == 9 ? 5 : 4;

            return string.Concat("(", ddd, ") ",
                                    numeroAux.Substring(0, indiceTraco),
                                    "-",
                                    numeroAux.Substring(indiceTraco, 4));
        }

        public static string Truncar(this string texto, int qtdeCaracteres)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;
            if (texto.Length > qtdeCaracteres)
                return texto.Substring(0, qtdeCaracteres);
            return texto;
        }

        public static string SomenteNumeros(this string texto)
        {
            return String.Join("", System.Text.RegularExpressions.Regex.Split(texto, @"[^\d]"));
        }

        #region StyleDinamicoNomeMentorPublicacao
        public static string StyleDinamicoNomeMentorPublicacao(this string nomeUsuario, bool positivo)

        {
            var corTexto = positivo ? "#19d443" : "#dc3545";
            string left = string.Empty, fontSize = string.Empty, top = string.Empty;

            string retorno;
            switch (nomeUsuario.Trim().Length)
            {
                case 1:
                case 2:
                case 3:
                    left = "34%";
                    fontSize = "6.5em";
                    top = "42%";
                    break;
                case 4:
                    left = "29%";
                    fontSize = "6.5em";
                    top = "42%";
                    break;
                case 5:
                    left = "27%";
                    fontSize = "6.5em";
                    top = "42%";
                    break;
                case 6:
                    left = "26%";
                    fontSize = "6.5em";
                    top = "42%";
                    break;
                case 7:
                    left = "22%";
                    fontSize = "6.5em";
                    top = "42%";
                    break;
                case 8:
                    left = "20%";
                    fontSize = "6.5em";
                    top = "42%";
                    break;
                case 9:
                    left = "19%";
                    fontSize = "6.5em";
                    top = "42%";
                    break;
                case 10:
                    left = "10%";
                    fontSize = "6.5em";
                    top = "41%";
                    break;
                case 11:
                    left = "9%";
                    fontSize = "6.5em";
                    top = "41%";
                    break;
                case 12:
                    left = "6%";
                    fontSize = "6em";
                    top = "43%";
                    break;
                case 13:
                    left = "6%";
                    fontSize = "6em";
                    top = "42%";
                    break;
                case 14:
                    left = "5%";
                    fontSize = "6em";
                    top = "42%";
                    break;
                case 15:
                    left = "2%";
                    fontSize = "5.5em";
                    top = "42%";
                    break;
                case 16:
                    left = "5%";
                    fontSize = "4.5em";
                    top = "44%";
                    break;
                case 17:
                    left = "4%";
                    fontSize = "5em";
                    top = "44%";
                    break;
                case 18:
                    left = "4%";
                    fontSize = "4.5em";
                    top = "45%";
                    break;
                case 19:
                    left = "6%";
                    fontSize = "4.5em";
                    top = "45%";
                    break;
                case 20:
                    left = "4%";
                    fontSize = "4em";
                    top = "45%";
                    break;
                case 21:
                    left = "8%";
                    fontSize = "3.5em";
                    top = "45%";
                    break;
                case 22:
                    left = "8%";
                    fontSize = "3em";
                    top = "45%";
                    break;
                case 23:
                    left = "5%";
                    fontSize = "3.5em";
                    top = "45%";
                    break;
                case 24:
                    left = "4%";
                    fontSize = "3.5em";
                    top = "45%";
                    break;
                case 25:
                    left = "4%";
                    fontSize = "3.5em";
                    top = "45%";
                    break;
                case 26:
                    left = "1%";
                    fontSize = "3.5em";
                    top = "45%";
                    break;
                case 27:
                    left = "2%";
                    fontSize = "3.25em";
                    top = "45%";
                    break;
                case 28:
                    left = "2%";
                    fontSize = "3em";
                    top = "45%";
                    break;
                case 29:
                    left = "2%";
                    fontSize = "3em";
                    top = "45%";
                    break;
                case 30:
                    left = "2%";
                    fontSize = "3em";
                    top = "46%";
                    break;
                default:
                    break;
            }

            retorno = string.Concat("position: absolute;",
                                    "font-family: inherit; ",
                                    "font-weight: bold;",
                                    $"left:{left};",
                                    $"font-size:{fontSize};",
                                    $"top:{top};",
                                    $"color:{corTexto};");

            return retorno;
        }
        #endregion

    }
}
