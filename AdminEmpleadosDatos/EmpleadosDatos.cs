using AdminEmpleadosEntidades;
//dependencia de Nuget
using Microsoft.Data.SqlClient;

namespace AdminEmpleadosDatos
    {
    public class EmpleadosDatos
        {
        //Conexion con la BD para traer los datos de la BD 
        public static List<Empleado> Get ( Empleado e )
            {
            List<Empleado> list = new List<Empleado>();

            //me conecto a la BD utilizando la cadena de conexion del App.config
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;

            //SqlConnection es una clase que sirve para construir objetos de conexión a una base de datos de Sql Server
            using ( SqlConnection connection = new SqlConnection(conString) )
                {
                //Representa un procedimiento almacenado o una instrucción de Transact-SQL
                //que se ejecuta en una base de datos de SQL Server.
                //en este caso utiliza el SP de EmpleadosGet 

                //en este punto la conexion todavia no esta abierta
                //ADO.Net
                //declaramos la conexion de SQL y recibe por parametro la cadena de conexion del App.config
                SqlCommand command = new SqlCommand("empleadosGet", connection);
                //se especifica cual es el tipo de comando que se va a ejecutar
                //En este caso vamos a ejecutar procedimientos de almacenado (SP)
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if ( e.id != null )
                    //cargamos los parametros que vienen del empleado en el SQLCommand
                    //esos parametros los recibe el SP
                    //estos parametros tienen que coincidir con los del SP
                    //si en el SP es "dni", aca tambien tiene que ser "dni"
                    //al parametro @dni le asigno el valor que viene del objeto
                    command.Parameters.AddWithValue("@id", e.id);
                if ( e.Nombre != null )
                    // le pasa al SP el valor del Nombre del empleado
                    command.Parameters.AddWithValue("@nombre_apellido", e.Nombre);

                try
                    {
                    //siempre se abre la conexion a la BD en un TRY/CATCH para poder controlar las excepciones
                    connection.Open();

                    //SQLDataReader es una clase que permite leer un conjunto de registros
                    //esto es el equivalente a hacer un SELECT
                    SqlDataReader reader = command.ExecuteReader();
                    //se utiliza el método ExecuteReader del SQLCommand,
                    //guardo los datos en el READER
                    
                    //con el while recorremos el reader y por cada fila voy guardando en mi lista de empleados los datos
                    while ( reader.Read() )
                        {
                        //creamos un nuevo objeto de tipo empleado
                        Empleado emp = new Empleado();
                        //traemos los datos
                        emp.id = Convert.ToInt32(reader["id"]);
                        emp.Nombre = Convert.ToString(reader["nombre_apellido"]);
                        emp.Dni = Convert.ToString(reader["dni"]);
                        if ( reader["direccion"].GetType() != typeof(DBNull) )
                            emp.Direccion = Convert.ToString(reader["direccion"]);
                        if ( reader["fecha_ingreso"].GetType() != typeof(DBNull) )
                            emp.FechaIngreso = Convert.ToDateTime(reader["fecha_ingreso"]);
                        if ( reader["salario"].GetType() != typeof(DBNull) )
                            emp.Salario = Convert.ToDecimal(reader["salario"]);

                        if ( reader["anulado"].GetType() != typeof(DBNull) )
                            emp.anulado = Convert.ToBoolean(reader["anulado"]);

                        if ( reader["nombre_dpto"].GetType() != typeof(DBNull) )
                            {
                            Departamento dep = new Departamento();
                            dep.id = 0;
                            dep.Nombre = Convert.ToString(reader["nombre_dpto"]);
                            emp.Departamento = dep;
                            }
                        //llenamos la lista con los datos de los empleados
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

        public static bool Update ( Empleado e )
            {
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;

            using ( SqlConnection connection = new SqlConnection(conString) )
                {
                SqlCommand command = new SqlCommand("empleadosModificar", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if ( e.id != null )
                    command.Parameters.AddWithValue("@id", e.id);

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
                    command.Parameters.AddWithValue("@dpto_id", e.Departamento.id);

                if ( e.NombreDepartamento != null && e.NombreDepartamento != null )
                    command.Parameters.AddWithValue("@nombre_dpto", e.NombreDepartamento);


                command.Parameters.AddWithValue("@anulado", e.anulado);

                try
                    {
                    connection.Open();

                    //hacemos el update
                    command.ExecuteNonQuery();
                    }
                catch ( Exception ) { throw; }
                return true;
                }
            }

        public static int Insert ( Empleado e )
            {
            int idEmpleadoCreado = 0;

            //obtengo la cadena de conexion del App.config mediante la clase System.Coinfiguration
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;

            //el USING se utiliza para que una vez ejecutado el codigo se libere el espacio en memoria
            using ( SqlConnection connection = new SqlConnection(conString) )
                //en este punto la conexion todavia no esta abierta
                //ADO.Net
                //declaramos la conexion de SQL y recibe por parametro la cadena de conexion del App.config
                {
                SqlCommand command = new SqlCommand("empleadosInsert", connection);
                //En este caso vamos a ejecutar procedimientos de almacenado (SP)
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if ( e.Dni != null )
                    //cargamos los parametros que vienen del empleado en el SQLCommand
                    //esos parametros los recibe el SP
                    //estos parametros tienen que coincidir con los del SP
                    //si en el SP es "dni", aca tambien tiene que ser "dni"
                    //al parametro @dni le asigno el valor que viene del objeto
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
                    command.Parameters.AddWithValue("@dpto_id", e.Departamento.id);

                if ( e.NombreDepartamento != null && e.NombreDepartamento != null )
                    command.Parameters.AddWithValue("@nombre_dpto", e.NombreDepartamento);

                //esta linea me esta tirando el error 
                //command.Parameters.AddWithValue("@anulado", e.anulado);
                try
                    {
                    //recien en este punto se abre la conexion con la BD
                    connection.Open();
                    //realizo el insert y obtengo el ID generado en la BD
                    idEmpleadoCreado = Convert.ToInt32(command.ExecuteScalar());
                    //aca ejecuto la consulta, va a la BD, hace lo que tiene que hacer y vuelve


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



