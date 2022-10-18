using AdminEmpleadosDatos;
using AdminEmpleadosEntidades;

namespace AdminEmpleadosNegocio
    {
    public class DepartamentosNegocio
        {
        public static List<Departamento> Get ( Departamento d )
            {
            try
                {
                return DepartamentosDatos.Get(d);
                }
            catch ( Exception )
                {
                throw;
                }
            }
        }
    }
