using ReclameAquiDoTrader.Business.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReclameAquiDoTrader.Business.ValueObjects
{
    public class ArquivoList
    {
        public List<Arquivo> Arquivos { get; set; }

        public void Adicionar(Arquivo arquivo)
        {
            if (Arquivos == null)
                Arquivos = new List<Arquivo>();

            if (Arquivos.Any(e => e == arquivo))
                return;

            Arquivos.Add(arquivo);
        }
        public void LimparArquivos()
        {
            Arquivos?.Clear();
        }
    }

    public class Arquivo : ValueObject<Arquivo>
    {
        public Arquivo(string nome, string contentType)
        {
            ValidarNomeArquivo(nome);
            Nome = nome;
            ContentType = contentType;
            Cadastro = DateTime.UtcNow;
            _identificador = Guid.NewGuid().ToString();
        }

        public string Identificador { get { return _identificador + Extensao; } }
        private string _identificador { get; set; }
        public string Nome { get; private set; }
        public string ContentType { get; private set; }
        public DateTime Cadastro { get; private set; }
        public string Extensao => Path.GetExtension(Nome);
        public static void ValidarNomeArquivo(string nome)
        {
            if (string.IsNullOrEmpty(nome?.Trim()))
                throw new DomainException("Nome de arquivo inválido");

            var extensao = Path.GetExtension(nome);
            if (string.IsNullOrEmpty(extensao))
                throw new DomainException("Nome de arquivo inválido");

            if (nome.Length == extensao.Length)
                throw new DomainException("Nome de arquivo inválido");
        }
    }
}
