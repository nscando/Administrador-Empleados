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

        //el metodo recibe un empleado por parametro
        public static int Insert ( Empleado e )
            {
            //hacemos las validaciones correspondientes
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
                //si esta todo ok hacemos vamos a la capa de datos
                return EmpleadosDatos.Insert(e);
                }
            catch ( Exception ) { throw; }
            }
        }

    
    
    }

    
    