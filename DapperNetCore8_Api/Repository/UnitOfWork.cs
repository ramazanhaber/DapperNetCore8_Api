using DapperNetCore8_Api.Interfaces;

namespace DapperNetCore8_Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IOgrencilerRepository ogrenciler, INotlarRepository notlar, IDerslerRepository dersler)
        {
            this.Ogrenciler = ogrenciler;
            Notlar = notlar;
            Dersler = dersler;
        }
        public IOgrencilerRepository Ogrenciler { get; }

        public INotlarRepository Notlar { get; }

        public IDerslerRepository Dersler { get; }
    }
}
