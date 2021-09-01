using System;
using System.Collections.Generic;

namespace Avaliacao.Dominio.Model
{
    public class Cliente : IModel
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime? Nascimento { get; set; }

        public virtual ISet<ClienteTelefone> Telefones { get; set; }
    }
}