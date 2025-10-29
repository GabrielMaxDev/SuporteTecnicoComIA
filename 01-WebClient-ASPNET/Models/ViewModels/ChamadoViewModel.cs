using System.ComponentModel.DataAnnotations;

namespace WebClient.Models.ViewModels
{
    public class ChamadoViewModel
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(1000, MinimumLength = 20)]
        [Display(Name = "Descrição detalhada")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione a categoria")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione a prioridade")]
        [Display(Name = "Prioridade")]
        public string Prioridade { get; set; } = string.Empty;

        public string? SugestaoIA { get; set; }
    }
}
