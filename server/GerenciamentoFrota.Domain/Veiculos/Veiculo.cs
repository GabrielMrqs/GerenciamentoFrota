using MongoDB.Bson.Serialization.Attributes;

namespace GerenciamentoFrota.Domain.Veiculos
{
    [BsonIgnoreExtraElements]
    public class Veiculo
    {
        public virtual string Chassi { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
        public byte NumeroPassageiros { get; set; }
        public string Cor { get; set; }
    }
}