using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaAccesoADatosDAL.Repositorios.ListaEvento
{
    public interface IListaEventoRepositorio
    {
        Task<List<CapaObjetos.Modelos.ListaEvento>> ObtenerListaEventoAsync();
        Task<CapaObjetos.Modelos.ListaEvento> ObtenerListaEventoPorIdAsync(int EventoId);
        Task<CapaObjetos.Modelos.ListaEvento> CrearEventoAsync(CapaObjetos.Modelos.ListaEvento listaEvento);
        Task<CapaObjetos.Modelos.ListaEvento> ActualizarEventoAsync(CapaObjetos.Modelos.ListaEvento listaEvento);

        Task<bool> EliminarEventoAsync(int EventoId);
    }
}
