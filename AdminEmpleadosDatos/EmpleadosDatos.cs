using AdminEmpleadosEntidades;
using Microsoft.Data.SqlClient;

namespace AdminEmpleadosDatos
    {
    public class EmpleadosDatos
        {
        public static List<Empleado> Get ( Empleado e )
            {
            List<Empleado> list = new List<Empleado>();

            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;

            using ( SqlConnection connection = new SqlConnection(conString) )
                {
                SqlCommand command = new SqlCommand("empleadosGet", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if ( e.id != null )
                    command.Parameters.AddWithValue("@id", e.id);
                if ( e.Nombre != null )
                    command.Parameters.AddWithValue("@nombre_apellido", e.Nombre);

                try
                    {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while ( reader.Read() )
                        {
                        Empleado emp = new Empleado();
                        emp.id = Convert.ToInt32(reader["id"]);
                        emp.Nombre = Convert.ToString(reader["nombre_apellido"]);
                        emp.Dni = Convert.ToString(reader["dni"]);
                        if ( reader["direccion"].GetType() != typeof(DBNull) )
                            emp.Direccion = Convert.ToString(reader["direccion"]);
                        if ( reader["fecha_ingreso"].GetType() != typeof(DBNull) )
                            emp.FechaIngreso = Convert.ToDateTime(reader["fecha_ingreso"]);
                        if ( reader["salario"].GetType() != typeof(DBNull) )
                            emp.Salario = Convert.ToDecimal(reader["salario"]);

                        if ( reader["nombre_depto"].GetType() != typeof(DBNull) )
                            {
                            Departamento dep = new Departamento();
                            dep.id = 0;
                            dep.Nombre = Convert.ToString(reader["nombre_depto"]);
                            emp.Departamento = dep;
                            }

                        list.Add(emp);
                        }
                    reader.Close();
                    }
                catch ( Exception )
                    {
                    throw;
                    }
                }
            return list;
            }

        public static int Insert ( Empleado e )
            {
            int idEmpleadoCreado = 0;

            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;

            using ( SqlConnection connection = new SqlConnection(conString) )
                {
                SqlCommand command = new SqlCommand("empleadosInsert", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if ( e.Dni != null )
                    command.Parameters.AddWithValue("@dni", e.Dni);


                if ( e.Nombre != null )
                    command.Parameters.AddWithValue("@nombre_apellido", e.Nombre);


                if ( e.Direccion != null )
                    command.Parameters.AddWithValue("@direccion", e.Direccion);


                if ( e.FechaIngreso != null )
                    command.Parameters.AddWithValue("@fecha_ingreso", e.FechaIngreso);


                if ( e.Salario != null )
                    command.Parameters.AddWithValue("@salario", e.Salario);


                if ( e.Departamento != null && e.Departamento.id != null )
                    command.Parameters.AddWithValue("@depto_id", e.Departamento.id);


                try
                    {
                    connection.Open();
                    //realizo el insert
                    idEmpleadoCreado = Convert.ToInt32(command.ExecuteScalar());

                    }
                catch ( Exception )
                    {
                    throw;
                    }

                return idEmpleadoCreado;

                }
            }
        }
    }