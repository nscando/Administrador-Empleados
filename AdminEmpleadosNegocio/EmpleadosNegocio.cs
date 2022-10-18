using AdminEmpleadosDatos;
using AdminEmpleadosEntidades;

namespace AdminEmpleadosNegocio
    {
    public class EmpleadosNegocio
        {

        public static List<Empleado> Get ( Empleado e )
            {
            return EmpleadosDatos.Get(e);
            }


        public static bool Update ( Empleado e )
            {
            if ( String.IsNullOrEmpty(e.Nombre) )
                {
                return false;
                }

            if ( String.IsNullOrEmpty(e.Dni) )
                {
                return false;
                }

            if ( e.FechaIngreso == null )
                {
                e.FechaIngreso = DateTime.Now;
                }

            try
                {
                return EmpleadosDatos.Update(e);
                }
            catch ( Exception ) { throw; }
            }

        public static int Insert ( Empleado e )
            {
            if ( String.IsNullOrEmpty(e.Nombre) )
                {
                return 0;
                }

            if ( String.IsNullOrEmpty(e.Dni) )
                {
                return 0;
                }



            if ( e.FechaIngreso == null )
                {
                e.FechaIngreso = DateTime.Now;
                }

            try
                {
                return EmpleadosDatos.Insert(e);
                }
            catch ( Exception ) { throw; }
            }
        }

    
    
    }

    
    