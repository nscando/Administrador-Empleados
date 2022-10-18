﻿using AdminEmpleadosEntidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminEmpleadosDatos
    {
    public class DepartamentosDatos
        {
        public static List<Departamento> Get ( Departamento d )
            {
            List<Departamento> list = new List<Departamento>();

            string conString = System.Configuration.ConfigurationManager.
                        ConnectionStrings["conexionDB"].ConnectionString;

            using ( SqlConnection connection = new SqlConnection(conString) )
                {

                SqlCommand command = new SqlCommand("departamentosGet", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if ( d.Nombre != null )
                    command.Parameters.AddWithValue("@nombre", d.Nombre);

                try
                    {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while ( reader.Read() )
                        {
                        Departamento dpto = new Departamento();

                        dpto.id = Convert.ToInt32(reader["id"]);
                        dpto.Nombre = Convert.ToString(reader["nombre_dpto"]);


                        list.Add(dpto);

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
        }
    }
