using DAL.Models;

namespace DAL.Repository
{
    public interface IRepository
    {
        Calculo GetCalculo(int id);
        void AddCalculo(string name, long value);
        void DeleteCalculo(int id);
        IEnumerable<Calculo> GetCalculos();
        IEnumerable<Calculo> GetCalculos(string name, long max, long min);
    }

    public class Repository : IRepository
    {
        private readonly CalculosDbContext _context;
        public Repository(CalculosDbContext context)
        {
            _context = context;
        }
        public void AddCalculo(string name, long value)
        {
            try
            {
                _context.InsertarCalculo(name, value);
            }catch (Exception ex)
            {
                throw new RepositoryException("Error al guardar el cálculo en la DB", ex);
            }
        }

        public void DeleteCalculo(int id)
        {
            try
            {
                var calculo = GetCalculo(id);
                _context.Remove(calculo);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new RepositoryException("Error al borrar el cálculo de la DB", ex);
            }
        }

        public IEnumerable<Calculo> GetCalculos(string name, long max, long min)
        {
           return _context.BuscarCalculos(name, max, min);
        }

        public Calculo GetCalculo(int id)
        {
            return _context.Calculos.FirstOrDefault(c=>c.Id==id);
        }

        public IEnumerable<Calculo> GetCalculos()
        {
            return _context.Calculos.ToList();
        }
    }
}
