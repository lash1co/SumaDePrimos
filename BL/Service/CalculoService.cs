using DAL.Models;
using DAL.Repository;

namespace BL.Service
{
    public interface ICalculo
    {
       IEnumerable<Calculo> GetCalculos();
        long AddCalculo(string name, int limit);
        Calculo GetCalculo(int id);
        void DeleteCalculo(int id);
        IEnumerable<Calculo> GetCalculos(string name, long max, long min);

    }
    public class CalculoService: ICalculo
    {
        private readonly IRepository _repository;
        public CalculoService(IRepository repository)
        {
            _repository = repository;
        }

        public long AddCalculo(string name, int limit)
        {
            long suma = 0;
            var i = 0;
            while (i < limit)
            {
                i++;
                if (i % 3 == 0 || i % 5 == 0)
                {
                    suma += i;
                }
            }
            try
            {
                _repository.AddCalculo(name, suma);
            }
            catch(RepositoryException ex) 
            {
                throw new ServiceException("Error en el servicio al agregar el cálculo.", ex);
            }
            return suma;
        }

        public void DeleteCalculo(int id)
        {
            try
            {
                _repository.DeleteCalculo(id);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException("Error en el servicio al borrar el cálculo.", ex);
            }
        }

        public Calculo GetCalculo(int id)
        {
            return _repository.GetCalculo(id);
        }

        public IEnumerable<Calculo> GetCalculos()
        {
            return _repository.GetCalculos();
        }

        public IEnumerable<Calculo> GetCalculos(string name, long max, long min)
        {
            return _repository.GetCalculos(name, max, min);
        }
    }
}
