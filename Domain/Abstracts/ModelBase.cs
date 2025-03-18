namespace WebApiExtendedTemplate.Domain.Abstracts {
    public abstract class ModelBase<TKey> {
        public TKey? Id { get; set; }
    }
}
