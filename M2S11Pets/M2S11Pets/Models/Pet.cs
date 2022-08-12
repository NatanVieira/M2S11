using System;
using System.Collections.Generic;

namespace M2S11Pets.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Pessoas = new HashSet<Pessoa>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public int Idade { get; set; }
        public int RacaId { get; set; }

        public virtual Raca Raca { get; set; } = null!;

        public virtual ICollection<Pessoa> Pessoas { get; set; }
    }
}
