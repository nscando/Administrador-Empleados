using AdminEmpleadosEntidades;
using AdminEmpleadosNegocio;

namespace AdminEmpleadosFront
    {
    public partial class FrmEditEmpleados : Form
        {
        public EnumModoForm modo = EnumModoForm.Alta;
        public Empleado _empleado = new Empleado();
        public FrmEditEmpleados ()
            {
            InitializeComponent();
            }

        private void btnAceptar_Click ( object sender, EventArgs e )
            {
            Guardar();
            }

        private bool ValidarEmpleado ( ref string mensaje, Empleado e )
            {
            mensaje = "";
            if ( String.IsNullOrEmpty(e.Dni.Trim()) )
                {
                mensaje += "\nError en DNI";
                }

            if ( String.IsNullOrEmpty(e.Nombre.Trim()) )
                {
                mensaje += "\nError en Nombre";
                }

            if ( !String.IsNullOrEmpty(mensaje) )
                {
                return false;
                }
            return true;
            }

        private void LimpiarControles ()
            {
            txtId.Text = "";
            txtSalario.Value = 0;
            txtDireccion.Text = "";
            txtDni.Text = "";
            txtIngreso.Value = DateTime.Now;
            txtNombre.Text = "";
            }

        private void Guardar ()
            {
            try
                {
                //cargamos datos ingresados en un objeto empleado
                Empleado emp = new Empleado();

                emp.Salario = txtSalario.Value;
                emp.Direccion = txtDireccion.Text.Trim();
                emp.Dni = txtDni.Text.Trim();
                emp.FechaIngreso = txtIngreso.Value;
                emp.Departamento = null;
                emp.Nombre = txtNombre.Text.Trim();

                string mensajeErrores = "";

                if ( !ValidarEmpleado(ref mensajeErrores, emp) )
                    {
                    MessageBox.Show("Atencion: Se en contraron los siguientes errores \n" +
                        mensajeErrores, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    }
                //si las validacion estan ok
                //se pregunta si quiere guardar los datos
                DialogResult res = MessageBox.Show("¿Confirma guardar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if ( res == DialogResult.No )
                    {
                    return;
                    }

                //guardamos los datos
                if ( modo == EnumModoForm.Alta )
                    {
                    int idEmp = EmpleadosNegocio.Insert(emp);
                    txtId.Text = idEmp.ToString();
                    MessageBox.Show("Se genero el empleado nro " + idEmp.ToString(), "Empleado creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                if ( modo == EnumModoForm.Modificacion )
                    {
                    emp.id = Convert.ToInt32(txtId.Text);
                    EmpleadosNegocio.Update(emp);

                    MessageBox.Show("Los datos se actualizaron correctamente", "Empleado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }




                LimpiarControles();

                }
            catch ( Exception ex ) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        private void FrmInsertEmpleados_Load ( object sender, EventArgs e )
            {
            if ( modo == EnumModoForm.Alta )
                {
                LimpiarControles();
                HabilitarControles(true);
                }
            if ( modo == EnumModoForm.Modificacion )
                {
                HabilitarControles(true);
                CargarDatos();

                }
            if ( modo == EnumModoForm.Consulta )
                {
                HabilitarControles(false);
                CargarDatos();
                btnAceptar.Enabled = false;
                }
            }

        private void btnCancelar_Click ( object sender, EventArgs e )
            {
            Close();
            }

        private void HabilitarControles ( bool habilitar )
            {
            txtSalario.Enabled = habilitar;
            txtDni.Enabled = habilitar;
            txtDireccion.Enabled = habilitar;
            txtIngreso.Enabled = habilitar;
            txtNombre.Enabled = habilitar;
            cbmDepartamento.Enabled = habilitar;
            }

        private void CargarDatos ()
            {
            txtId.Text = _empleado.id.ToString();
            txtSalario.Value = Convert.ToDecimal(_empleado.Salario);
            txtDireccion.Text = _empleado.Direccion;
            txtDni.Text = _empleado.Dni;
            if ( _empleado.FechaIngreso != null )
                {
                txtIngreso.Value = Convert.ToDateTime(_empleado.FechaIngreso);
                txtNombre.Text = _empleado.Nombre;
                }
            }


        }




    }
