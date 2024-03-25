namespace DapperNetCore8_Api.Interfaces
{
    public interface IUnitOfWork
    {
        IOgrencilerRepository Ogrenciler { get; }
        INotlarRepository Notlar { get; }
        IDerslerRepository Dersler{ get; }
    }
}
