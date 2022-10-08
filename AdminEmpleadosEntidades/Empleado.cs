using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminEmpleadosEntidades
    {
    public class Empleado
        {
        public Departamento? Departamento
            {
            get; set;
            }

        public string? Direccion
            {
            get; set;
            }

        public string? Dni
            {
            get; set;
            }

        public DateTime? FechaIngreso
            {
            get; set;
            }

        public int? id
            {
            get;set;
            }
        public string? Nombre
            {
            get; set;
            }
        public string? NombreDepartamento
            {
            get
                {
                if ( Departamento != null )

                    return Departamento.Nombre;
                else
                    return null;
                }
            }

        public decimal? Salario
            {
            get; set;
            }
        }

    }
