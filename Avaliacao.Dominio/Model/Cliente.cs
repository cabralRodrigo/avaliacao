using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Avaliacao.Dominio.Model
{
    public class Cliente : IModel
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public virtual string Nome { get; set; }


        [Required(ErrorMessage = "O email do cliente é obrigatório")]
        [EmailAddress(ErrorMessage = "O email informado é inválido")]
        public virtual string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? Nascimento { get; set; }

        public virtual IList<ClienteTelefone> Telefones { get; set; }
    }
}