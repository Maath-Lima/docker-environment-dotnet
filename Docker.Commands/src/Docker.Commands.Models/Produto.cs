using System.Text.Json.Serialization;

namespace Docker.Commands.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public int Estoque { get; set; }

        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria Categoria { get; set; }
    }
}