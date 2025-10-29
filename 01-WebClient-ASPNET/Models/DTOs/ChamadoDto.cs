namespace WebClient.Models.DTOs
{
    public class ChamadoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Prioridade { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int ClienteId { get; set; }
        public int? TecnicoId { get; set; }
        public string? SugestaoIA { get; set; }
        public int? Avaliacao { get; set; }
    }
}
